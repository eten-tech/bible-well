using System.Globalization;
using System.Reflection;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using Avalonia.Styling;
using BibleWell.App.Configuration;
using BibleWell.App.Resources;
using BibleWell.App.Telemetry;
using BibleWell.App.ViewModels;
using BibleWell.App.ViewModels.Components;
using BibleWell.App.ViewModels.Pages;
using BibleWell.App.Views;
using BibleWell.App.Views.Components;
using BibleWell.App.Views.Pages;
using BibleWell.Aquifer;
using BibleWell.Aquifer.Api;
using BibleWell.Aquifer.Api.Helpers;
using BibleWell.Aquifer.Data;
using BibleWell.Preferences;
using BibleWell.PushNotifications;
using CommunityToolkit.Extensions.DependencyInjection;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.ApplicationInsights;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Http.Resilience;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.ApplicationInsights;
using Microsoft.Extensions.Options;
using Polly;

namespace BibleWell.App;

public partial class App : Application, IDisposable
{
    private bool _isDisposed;
    private InMemoryChannel _telemetryChannel = null!;

    public void Dispose()
    {
        Dispose(disposing: true);
        GC.SuppressFinalize(this);
    }
    
    private IPushNotificationActionService? _pushNotificationActionService;

    protected virtual void ConfigurePlatform(ConfigurationBuilder configurationBuilder, string environment)
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

    /// <summary>
    /// Used by Avalonia for the first initialization of our application configuration.
    /// </summary>
    public override void OnFrameworkInitializationCompleted()
    {
        ConfigureApplication();

        base.OnFrameworkInitializationCompleted();
    }

    /// <summary>
    /// Reloads the application services and configuration.
    /// Router history will be erased and then the specified <typeparamref name="TViewModel" /> will be loaded.
    /// This should only be done in development/admin mode; no normal users should ever be able to do this.
    /// </summary>
    /// <param name="environmentOverride">The environment to use (this will override any environment variables).</param>
    /// <typeparam name="TViewModel">The view model to which to navigate after the reload.</typeparam>
    public void ReloadApplication<TViewModel>(AppEnvironment? environmentOverride = null) where TViewModel : ViewModelBase
    {
        ConfigureApplication(environmentOverride, isReload: true);

        var router = Ioc.Default.GetRequiredService<Router>();
        router.EraseHistory();
        router.GoTo<TViewModel>();
    }

    /// <summary>
    /// Reloads the main view but not the application services and configuration
    /// (see <see cref="ReloadApplication{TViewModel}" /> for that operation).
    /// Router history will be erased and then the specified <typeparamref name="TViewModel" /> will be loaded.
    /// </summary>
    /// <typeparam name="TViewModel">The view model to which to navigate after the reload.</typeparam>
    public void ReloadMainView<TViewModel>() where TViewModel : ViewModelBase
    {
        LoadMainView(isReload: true);

        var router = Ioc.Default.GetRequiredService<Router>();
        router.EraseHistory();
        router.GoTo<TViewModel>();
    }

    /// <summary>
    /// This method does not update user preferences.
    /// </summary>
    /// <param name="cultureInfo">The culture to use for the application.</param>
    /// <returns><c>true</c> if the culture is supported, <c>false</c> otherwise.</returns>
    public static bool TrySetApplicationCulture(CultureInfo cultureInfo)
    {
        CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
        CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;
        Thread.CurrentThread.CurrentCulture = cultureInfo;
        Thread.CurrentThread.CurrentUICulture = cultureInfo;
        AppResources.Culture = cultureInfo;

        var isSupportedCulture = ResourceHelper.IsSupportedCulture(cultureInfo);
        if (!isSupportedCulture)
        {
            var logger = Ioc.Default.GetRequiredService<ILogger<App>>();
            logger.LogWarning(
                "The user's culture '{Culture}' is not supported.  Defaulting to 'en' as display language.",
                cultureInfo.Name);
        }
        
        return isSupportedCulture;
    }

    /// <summary>
    /// Gets the current application culture as set by either the OS or our language selection logic.
    /// </summary>
    public static CultureInfo GetApplicationCulture()
    {
        return Thread.CurrentThread.CurrentUICulture;
    }

    private void ConfigureApplication(AppEnvironment? environmentOverride = null, bool isReload = false)
    {
        if (!isReload)
        {
            ConfigureUnhandledExceptionHandling();
        }

        var config = ConfigureConfiguration(environmentOverride);

        var viewLocator = ConfigureViewLocator();
        if (isReload)
        {
            var previousViewLocator = DataTemplates.Single(dt => dt is ViewLocator);
            DataTemplates.Remove(previousViewLocator);
        }

        DataTemplates.Add(viewLocator);

        var serviceProvider = ConfigureServiceProvider(config, viewLocator, isReload);
        if (isReload)
        {
            // If Ioc.Default.ConfigureServices() has already been called previously then we have to hack reset the Ioc
            // because it's not designed for reloading services and throws an exception if you attempt to do so.
            ResetIoc();
        }

        Ioc.Default.ConfigureServices(serviceProvider);

        ConfigureUserPreferences(serviceProvider.GetRequiredService<IUserPreferencesService>());
        
        ConfigurePushNotifications();

        LoadMainView(isReload);
    }

    /// <summary>
    /// Configures the application based upon the user's previously saved preferences.
    /// </summary>
    /// <returns>The view model type to load as the first page.</returns>
    private void ConfigureUserPreferences(IUserPreferencesService userPreferencesService)
    {
        var userThemeVariant = userPreferencesService.Get(PreferenceKeys.ThemeVariant, "Default");
        RequestedThemeVariant = new ThemeVariant(userThemeVariant, inheritVariant: null);

        var userLanguage = userPreferencesService.Get(
            PreferenceKeys.Language,
            GetApplicationCulture().Name);

        try
        {
            var preferredCultureInfo = new CultureInfo(userLanguage);
            TrySetApplicationCulture(preferredCultureInfo);
        }
        catch (CultureNotFoundException)
        {
            userPreferencesService.Remove(PreferenceKeys.Language);
        }
    }
    
    private void ConfigurePushNotifications()
    {
        // Subscribe to push notification actions
        if (_pushNotificationActionService != null)
        {
            _pushNotificationActionService.ActionTriggered -= NotificationActionTriggered;
        }
        
        _pushNotificationActionService = Ioc.Default.GetService<IPushNotificationActionService>();
        if (_pushNotificationActionService != null)
        {
            _pushNotificationActionService.ActionTriggered += NotificationActionTriggered;
        }
    }

    private void LoadMainView(bool isReload = false)
    {
        var viewLocator = Ioc.Default.GetRequiredService<ViewLocator>();

        var mainViewModel = ViewModelFactory.Create<MainViewModel>();

        // The desktop application lifetime is used for the desktop app, testing, and the design view.
        // Don't use DI for the view because there is no directly associated ViewModel.
        if (ApplicationLifetime is IClassicDesktopStyleApplicationLifetime desktop)
        {
            if (!isReload)
            {
                // Line below is needed to remove Avalonia data validation.
                // Without this line you will get duplicate validations from both Avalonia and CommunityToolkit.Mvvm.
                BindingPlugins.DataValidators.RemoveAt(index: 0);

                desktop.MainWindow = new MainWindow
                {
                    DataContext = mainViewModel,
                };
            }
            else
            {
                desktop.MainWindow!.DataContext = mainViewModel;
            }
        }
        // The SingleViewApplicationLifetime is for mobile app configuration (no MainWindow is used).
        else if (ApplicationLifetime is ISingleViewApplicationLifetime singleViewPlatform)
        {
            var mainView = viewLocator.Build(mainViewModel);
            mainView.DataContext = mainViewModel;
            singleViewPlatform.MainView = mainView;
        }
    }

    private void NotificationActionTriggered(object? sender, ActionEnum action)
    {
        // As an example, we navigate to different pages based on the action
        var router = Ioc.Default.GetService<Router>();
        switch (action)
        {
            case ActionEnum.ActionA:
                router?.GoTo<HomePageViewModel>();
                break;
            case ActionEnum.ActionB:
                router?.GoTo<BiblePageViewModel>();
                break;
            default:
                break;
        }
    }
    
    private IConfiguration ConfigureConfiguration(AppEnvironment? environmentOverride = null)
    {
        var configurationBuilder = new ConfigurationBuilder();

        var environment = environmentOverride
                ?.ToString() ??
            Environment.GetEnvironmentVariable("DOTNET_ENVIRONMENT")
#if DEBUG
            ?? nameof(AppEnvironment.Development);
#else
            ?? nameof(AppEnvironment.Production);
#endif

        using var globalConfigurationSettingsFileStream = GetAppSettingsFileStream(
            Assembly.GetExecutingAssembly(), 
            "appsettings.json");
        configurationBuilder.AddJsonStream(globalConfigurationSettingsFileStream);

        using var environmentConfigurationSettingsFileStream = GetAppSettingsFileStream(
            Assembly.GetExecutingAssembly(),
            $"appsettings.{environment}.json");
        configurationBuilder.AddJsonStream(environmentConfigurationSettingsFileStream);

        ConfigurePlatform(configurationBuilder, environment);

        return configurationBuilder.Build();
    }

    protected static Stream GetAppSettingsFileStream(Assembly assembly, string appSettingsFileName)
    {
        var appSettingsEmbeddedResourceFileName = $"{assembly.GetName().Name}.{appSettingsFileName}";
        var configurationStream = assembly.GetManifestResourceStream(appSettingsEmbeddedResourceFileName);
        return configurationStream ??
            throw new InvalidOperationException($"The embedded resource \"{appSettingsEmbeddedResourceFileName}\" was not found.");
    }

    private ServiceProvider ConfigureServiceProvider(IConfiguration configuration, ViewLocator viewLocator, bool isReload)
    {
        var services = new ServiceCollection();

        var configurationOptions = configuration.Get<ConfigurationOptions>() ??
            throw new InvalidOperationException($"Unable to bind {nameof(ConfigurationOptions)}.");

        services.AddOptions<ConfigurationOptions>().Bind(configuration);

        services.AddSingleton(new Router());
        services.AddSingleton(viewLocator);

        // ApplicationInsights and Logging

        if (isReload)
        {
            _telemetryChannel.Flush();
            _telemetryChannel.Dispose();
        }

        _telemetryChannel = new InMemoryChannel
        {
#if DEBUG
            DeveloperMode = true,
#endif
            MaxTelemetryBufferCapacity = 10,
            SendingInterval = TimeSpan.FromSeconds(seconds: 30),
        };

        services
            .AddLogging(b =>
            {
#if DEBUG
                b.AddFilter<ApplicationInsightsLoggerProvider>(
                        "Category",
                        LogLevel.Trace)
                    .SetMinimumLevel(LogLevel.Trace)
                    .AddConsole();
#else
                b.AddFilter<ApplicationInsightsLoggerProvider>(
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
            .ConfigureHttpClientDefaults(builder => builder
                .SetHandlerLifetime(TimeSpan.FromMinutes(minutes: 5))
                .AddStandardResilienceHandler()
                .Configure(o =>
                {
                    o.AttemptTimeout.Timeout = TimeSpan.FromSeconds(value: 30);

                    o.CircuitBreaker.SamplingDuration = TimeSpan.FromSeconds(60);

                    o.Retry.BackoffType = DelayBackoffType.Exponential;
                    o.Retry.Delay = TimeSpan.FromSeconds(value: 0.5);
                    o.Retry.MaxRetryAttempts = 3;
                    o.Retry.UseJitter = true;

                    o.TotalRequestTimeout.Timeout = TimeSpan.FromSeconds(value: 60);
                }))
            .AddAquiferWellApiClient(configurationOptions.AquiferApiBaseUri, configurationOptions.AquiferApiKey);

        services.AddLocalization(options => options.ResourcesPath = "/Resources");

        RegisterPlatformServices(services);

        return services
            .BuildServiceProvider();
    }

    /// <summary>
    /// Shuts down the application.
    /// </summary>
    public void Shutdown()
    {
        if (ApplicationLifetime is IControlledApplicationLifetime controlledApplicationLifetime)
        {
            controlledApplicationLifetime.Shutdown();
        }
        else
        {
            Environment.Exit(exitCode: 0);
        }
    }

    /// <summary>
    /// Hack: Use reflection to reset the Ioc.Default instance.
    /// The CommunityToolkit.Mvvm.DependencyInjection DI service provider does not allow configuring services twice.
    /// Because the static Ioc.Default instance is used, we need to reset that static value's services.
    /// It doesn't have a setter to do this, so we need to use reflection to set the value.
    /// If they ever provide a way to reset the service provider via their library, we should use that instead.
    /// </summary>
    protected void ResetIoc()
    {
        // Get the serviceProvider private field.
        var serviceProviderField = typeof(Ioc).GetField("serviceProvider", BindingFlags.NonPublic | BindingFlags.Instance) ??
            throw new InvalidOperationException(
                $"Could not retrieve the {nameof(Ioc)}.{nameof(Ioc.Default)}.serviceProvider private field.");

        // Set serviceProvider to null on the static Default IOC instance.
        // Note that the serviceProvider field is marked "volatile" but no other thread should be accessing it at this point.
        serviceProviderField.SetValue(Ioc.Default, value: null);
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

    protected virtual void Dispose(bool disposing)
    {
        if (!_isDisposed)
        {
            _isDisposed = true;

            if (disposing)
            {
                // Unsubscribe from events
                if (_pushNotificationActionService != null)
                {
                    _pushNotificationActionService.ActionTriggered -= NotificationActionTriggered;
                }
                
                _telemetryChannel.Dispose();
                _telemetryChannel = null!;
            }
        }
    }

    [Transient(typeof(AquiferApiService), typeof(IReadOnlyAquiferService))]
    [Transient(typeof(CachingAquiferService), typeof(ICachingAquiferService))]
    [Singleton(typeof(ResourceContentRepository), typeof(ResourceContentRepository))]
    [Singleton(typeof(SqliteAquiferService), typeof(IReadWriteAquiferService))]
    [Singleton(typeof(SqliteDbManager), typeof(SqliteDbManager))]
    [Singleton(typeof(PushNotificationActionService), typeof(IPushNotificationActionService))]
    private static partial void ConfigureServices(IServiceCollection services);

    [Singleton(typeof(MainViewModel))]
    // components
    [Transient(typeof(TiptapRendererViewModel))]
    // pages
    [Transient(typeof(BiblePageViewModel))]
    [Transient(typeof(DevPageViewModel))]
    [Transient(typeof(GuidePageViewModel))]
    [Transient(typeof(HomePageViewModel))]
    [Transient(typeof(LanguagesPageViewModel))]
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
    [Transient(typeof(LanguagesPageView))]
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
        viewLocator.RegisterViewFactory<LanguagesPageViewModel, LanguagesPageView>();
        viewLocator.RegisterViewFactory<LibraryPageViewModel, LibraryPageView>();
        viewLocator.RegisterViewFactory<ResourcesPageViewModel, ResourcesPageView>();

        return viewLocator;
    }
}