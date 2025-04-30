using System.Net.Http.Headers;
using System.Reflection;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;
using BibleWell.App.Configuration;
using BibleWell.App.Telemetry;
using BibleWell.App.ViewModels;
using BibleWell.App.ViewModels.Components;
using BibleWell.App.ViewModels.Pages;
using BibleWell.App.Views;
using BibleWell.App.Views.Components;
using BibleWell.App.Views.Pages;
using BibleWell.Aquifer;
using BibleWell.Aquifer.Api;
using BibleWell.Aquifer.Data;
using BibleWell.Preferences;
using CommunityToolkit.Extensions.DependencyInjection;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Polly;
using Polly.Contrib.WaitAndRetry;
using Polly.Extensions.Http;

namespace BibleWell.App;

public partial class App : Application, IDisposable
{
    private InMemoryChannel _telemetryChannel = new()
    {
#if DEBUG
        DeveloperMode = true,
#endif
        MaxTelemetryBufferCapacity = 10,
        SendingInterval = TimeSpan.FromSeconds(30),
    };
    private bool _isDisposed;

    protected virtual void ConfigurePlatform(ConfigurationBuilder configurationBuilder)
    {
        throw new NotImplementedException("This method must be implemented in platform-specific projects.");
    }

    protected virtual void RegisterPlatformServices(IServiceCollection services)
    {
        throw new NotImplementedException("This method must be implemented in platform-specific projects.");
    }

    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        ConfigureUnhandledExceptionHandling();

        var config = ConfigureConfiguration();

        var serviceProvider = ConfigureServiceProvider(config);
        Ioc.Default.ConfigureServices(serviceProvider);

        var viewLocator = ConfigureViewLocator();
        DataTemplates.Add(viewLocator);

        var userPreferencesService = serviceProvider.GetRequiredService<IUserPreferencesService>();
        var userThemeVariant = userPreferencesService.Get(PreferenceKeys.ThemeVariant, "Default");
        RequestedThemeVariant = new ThemeVariant(userThemeVariant, null);

        // configure Avalonia app main window
        var mainViewModel = Ioc.Default.GetRequiredService<MainViewModel>();

        // The desktop application lifetime is only used for the design view.
        // Don't use DI for the view because there is no directly associated ViewModel.
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            // Line below is needed to remove Avalonia data validation.
            // Without this line you will get duplicate validations from both Avalonia and CT
            BindingPlugins.DataValidators.RemoveAt(0);

            desktop.MainWindow = new MainWindow
            {
                DataContext = mainViewModel,
            };
        }
        // the SingleViewApplicationLifetime is for mobile app configuration (no MainWindow is used)
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            var mainView = viewLocator.Build(mainViewModel);
            mainView.DataContext = mainViewModel;
            singleViewPlatform.MainView = mainView;
        }

        base.OnFrameworkInitializationCompleted();
    }

    private IConfiguration ConfigureConfiguration()
    {
        var configurationBuilder = new ConfigurationBuilder();

        var environment = Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")
#if DEBUG
            ?? "Development";
#else
            ?? "Production";
#endif

        using var globalConfigurationSettingsFileStream = GetAppSettingsFileStream("appsettings.json");
        configurationBuilder.AddJsonStream(globalConfigurationSettingsFileStream);

        using var environmentConfigurationSettingsFileStream = GetAppSettingsFileStream($"appsettings.{environment}.json");
        configurationBuilder.AddJsonStream(environmentConfigurationSettingsFileStream);

        ConfigurePlatform(configurationBuilder);

        return configurationBuilder.Build();

        static Stream GetAppSettingsFileStream(string appSettingsFileName)
        {
            var appSettingsEmbeddedResourceFileName = $"{Assembly.GetExecutingAssembly().GetName().Name}.{appSettingsFileName}";
            var configurationStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(appSettingsEmbeddedResourceFileName);
            return configurationStream ?? throw new InvalidOperationException($"The embedded resource \"{appSettingsEmbeddedResourceFileName}\" was not found.");
        }
    }

    private ServiceProvider ConfigureServiceProvider(IConfiguration configuration)
    {
        var services = new ServiceCollection();

        var configurationOptions = configuration.Get<ConfigurationOptions>() ??
            throw new InvalidOperationException($"Unable to bind {nameof(ConfigurationOptions)}.");

        services.AddOptions<ConfigurationOptions>().Bind(configuration);

        services.AddSingleton(new Router());

        // ApplicationInsights and Logging
        services
            .AddLogging(b =>
            {
#if DEBUG
                b.AddFilter<Microsoft.Extensions.Logging.ApplicationInsights.ApplicationInsightsLoggerProvider>(
                        "Category",
                        LogLevel.Trace)
                    .SetMinimumLevel(LogLevel.Trace)
                    .AddConsole();
#else
                b.AddFilter<Microsoft.Extensions.Logging.ApplicationInsights.ApplicationInsightsLoggerProvider>(
                        "Category",
                        LogLevel.Information)
                    .SetMinimumLevel(LogLevel.Information);
#endif

                if (!string.IsNullOrEmpty(configurationOptions.ApplicationInsights.ConnectionString))
                {
                    b.AddApplicationInsights(o => o.IncludeScopes = true);
                }
            })
            .Configure<TelemetryConfiguration>(c =>
            {
                c.TelemetryChannel = _telemetryChannel;
                c.ConnectionString = configurationOptions.ApplicationInsights.ConnectionString;
                c.TelemetryInitializers.Add(new OperationCorrelationTelemetryInitializer());
                c.TelemetryInitializers.Add(new AppTelemetryInitializer());
            })
            .AddTransient(sp => new TelemetryClient(sp.GetRequiredService<IOptions<TelemetryConfiguration>>().Value));

        ConfigureServices(services);
        ConfigureViewModels(services);
        ConfigureViews(services);

        // HTTP clients
        services
            .AddHttpClient<IReadOnlyAquiferService, AquiferApiService>(
                client =>
                {
                    client.BaseAddress = new Uri(configurationOptions.AquiferApiBaseUri);
                    client.DefaultRequestHeaders.UserAgent.Add(
                        new ProductInfoHeaderValue(
                            new ProductHeaderValue(
                                Assembly.GetExecutingAssembly().GetName().Name ?? "",
                                Assembly.GetExecutingAssembly().GetName().Version?.ToString())));
                    client.DefaultRequestHeaders.Add("api-key", configurationOptions.AquiferApiKey);
                })
            .SetHandlerLifetime(TimeSpan.FromMinutes(5))
            .AddPolicyHandler(GetRetryPolicy());

        RegisterPlatformServices(services);

        return services
            .BuildServiceProvider();
    }

    private void LogUnhandledException(Exception ex)
    {
        try
        {
            var logger = Ioc.Default.GetRequiredService<ILogger<App>>();
            logger.LogCritical(ex, "An unhandled exception resulted in an application crash!");

            _telemetryChannel.Flush();
        }
        catch
        {
            // ignore exceptions thrown when attempting to log unhandled exceptions
        }
    }

    private void ConfigureUnhandledExceptionHandling()
    {
        AppDomain.CurrentDomain.UnhandledException += (_, e) =>
        {
            var ex = (Exception)e.ExceptionObject;
            LogUnhandledException(ex);
        };
    }

    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (!_isDisposed)
        {
            _isDisposed = true;

            if (disposing)
            {
                _telemetryChannel.Dispose();
                _telemetryChannel = null!;
            }
        }
    }

    private static AsyncPolicy<HttpResponseMessage> GetRetryPolicy()
    {
        return HttpPolicyExtensions
            .HandleTransientHttpError()
            .OrResult(msg => msg.StatusCode == System.Net.HttpStatusCode.NotFound)
            .WaitAndRetryAsync(Backoff.DecorrelatedJitterBackoffV2(medianFirstRetryDelay: TimeSpan.FromSeconds(0.5), retryCount: 2));
    }

    [Singleton(typeof(AquiferApiService), typeof(IReadOnlyAquiferService))]
    [Singleton(typeof(CachingAquiferService), typeof(ICachingAquiferService))]
    [Singleton(typeof(ResourceContentRepository), typeof(ResourceContentRepository))]
    [Singleton(typeof(SqliteAquiferService), typeof(IReadWriteAquiferService))]
    [Singleton(typeof(SqliteDbManager), typeof(SqliteDbManager))]
    private static partial void ConfigureServices(IServiceCollection services);

    [Singleton(typeof(MainViewModel))]
    // components
    [Transient(typeof(TiptapRendererViewModel))]
    // pages
    [Transient(typeof(BiblePageViewModel))]
    [Transient(typeof(DevPageViewModel))]
    [Transient(typeof(GuidePageViewModel))]
    [Transient(typeof(HomePageViewModel))]
    [Transient(typeof(LibraryPageViewModel))]
    [Transient(typeof(ResourcesPageViewModel))]
    private static partial void ConfigureViewModels(IServiceCollection services);

    [Singleton(typeof(MainView))]
    // components
    [Transient(typeof(TiptapRendererView))]
    // pages
    [Transient(typeof(BiblePageView))]
    [Transient(typeof(DevPageView))]
    [Transient(typeof(GuidePageView))]
    [Transient(typeof(HomePageView))]
    [Transient(typeof(LibraryPageView))]
    [Transient(typeof(ResourcesPageView))]
    private static partial void ConfigureViews(IServiceCollection services);

    // all views must be mapped from the primary view model in this method
    private static ViewLocator ConfigureViewLocator()
    {
        var viewLocator = new ViewLocator();

        viewLocator.RegisterViewFactory<MainViewModel, MainView>();

        // components
        viewLocator.RegisterViewFactory<TiptapRendererViewModel, TiptapRendererView>();

        // pages
        viewLocator.RegisterViewFactory<BiblePageViewModel, BiblePageView>();
        viewLocator.RegisterViewFactory<DevPageViewModel, DevPageView>();
        viewLocator.RegisterViewFactory<GuidePageViewModel, GuidePageView>();
        viewLocator.RegisterViewFactory<HomePageViewModel, HomePageView>();
        viewLocator.RegisterViewFactory<LibraryPageViewModel, LibraryPageView>();
        viewLocator.RegisterViewFactory<ResourcesPageViewModel, ResourcesPageView>();

        return viewLocator;
    }
}