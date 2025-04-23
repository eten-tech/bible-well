#if DEBUG
using BibleWell.App.ViewModels.Pages;
using BibleWell.Aquifer;
using BibleWell.Preferences;
using FakeItEasy;

namespace BibleWell.App.ViewModels;

/// <summary>
/// This class is only built in DEBUG mode and is only for providing design-time data.
/// </summary>
public static class DesignData
{
    // pages
    public static BiblePageViewModel DesignBiblePageViewModel { get; } = new();
    public static DevPageViewModel DesignDevPageViewModel { get; } = new(A.Fake<IReadWriteAquiferService>());
    public static GuidePageViewModel DesignGuidePageViewModel { get; } = new();
    public static HomePageViewModel DesignHomePageViewModel { get; } = new(A.Fake<IUserPreferencesService>());
    public static LibraryPageViewModel DesignLibraryPageViewModel { get; } = new();
    public static ResourcesPageViewModel DesignResourcesPageViewModel { get; } = new(A.Fake<ICachingAquiferService>());

    // this must be last because it references the above view models
    public static MainViewModel DesignMainViewModel { get; } = new(new Router<ViewModelBase>());
}
#endif