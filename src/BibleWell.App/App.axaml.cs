using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Controls.Templates;
using Avalonia.Data.Core.Plugins;
using Avalonia.Markup.Xaml;
using BibleWell.App.ViewModels;
using BibleWell.App.ViewModels.Pages;
using BibleWell.App.Views;
using BibleWell.App.Views.Pages;
using BibleWell.Aquifer;
using BibleWell.Aquifer.Api;
using BibleWell.Aquifer.Data;
using CommunityToolkit.Extensions.DependencyInjection;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.Extensions.DependencyInjection;

namespace BibleWell.App;

public partial class App : Application
{
    public override void Initialize()
    {
        AvaloniaXamlLoader.Load(this);
    }

    public override void OnFrameworkInitializationCompleted()
    {
        Ioc.Default.ConfigureServices(ConfigureServiceProvider());
        var viewLocator = ConfigureViewLocator();
        DataTemplates.Add(viewLocator);

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

    private static ServiceProvider ConfigureServiceProvider()
    {
        var services = new ServiceCollection();

        ConfigureServices(services);
        ConfigureViewModels(services);
        ConfigureViews(services);

        return services
            .BuildServiceProvider();
    }

    [Singleton(typeof(AquiferApiService), typeof(IReadOnlyAquiferService))]
    [Singleton(typeof(CachingAquiferService), typeof(ICachingAquiferService))]
    [Singleton(typeof(SqliteAquiferService), typeof(IReadWriteAquiferService))]
    private static partial void ConfigureServices(IServiceCollection services);

    [Singleton(typeof(MainViewModel))]
    [Transient(typeof(BiblePageViewModel))]
    [Transient(typeof(GuidePageViewModel))]
    [Transient(typeof(HomePageViewModel))]
    [Transient(typeof(LibraryPageViewModel))]
    [Transient(typeof(ResourcesPageViewModel))]
    private static partial void ConfigureViewModels(IServiceCollection services);

    [Singleton(typeof(MainView))]
    [Transient(typeof(BiblePageView))]
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
        viewLocator.RegisterViewFactory<BiblePageViewModel, BiblePageView>();
        viewLocator.RegisterViewFactory<GuidePageViewModel, GuidePageView>();
        viewLocator.RegisterViewFactory<HomePageViewModel, HomePageView>();
        viewLocator.RegisterViewFactory<LibraryPageViewModel, LibraryPageView>();
        viewLocator.RegisterViewFactory<ResourcesPageViewModel, ResourcesPageView>();

        return viewLocator;
    }
}