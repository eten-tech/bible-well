using System.Collections.ObjectModel;
using BibleWell.App.Resources;
using BibleWell.App.ViewModels.Pages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BibleWell.App.ViewModels;

/// <summary>
/// View model for use with the <see cref="Views.MainView" />.
/// </summary>
public partial class MainViewModel : ViewModelBase
{
    private readonly Router _router;

    [ObservableProperty]
    private ViewModelBase _currentPage = null!;

    [ObservableProperty]
    private bool _isMenuPaneOpen;

    [ObservableProperty]
    private MenuItemTemplate? _selectedMenuItem;

    public MainViewModel(Router router)
    {
        _router = router;
        _router.CurrentViewModelChanged += OnRouterCurrentViewModelChanged;
        _router.GoTo<PageViewModelBase>(MenuItems[0].ViewModelType);

        if (!ResourceHelper.IsSupportedCulture(App.GetApplicationCulture()))
        {
            // TODO BIB-934 open a modal instead
            _router.GoTo<LanguagesPageViewModel>();
        }
    }

    public static ObservableCollection<MenuItemTemplate> MenuItems { get; } =
    [
        new(typeof(HomePageViewModel), "HomeRegular"),
        new(typeof(BiblePageViewModel), "BookOpenRegular"),
        new(typeof(GuidePageViewModel), "CompassNorthwestRegular"),
        new(typeof(ResourcesPageViewModel), "ClipboardRegular"),
        new(typeof(LibraryPageViewModel), "LibraryRegular"),
        new(typeof(DevPageViewModel), "WindowDevToolsRegular"),
    ];

    private void OnRouterCurrentViewModelChanged(ViewModelBase vm)
    {
        // It's possible for any view model to use the router to navigate to another view model.
        // Therefore, if the view model changes we need to update the selected menu item.
        // It's also possible to navigate to a page not in the menu in which case the selected menu item should remain unchanged.
        var selectedMenuItem = MenuItems.FirstOrDefault(mi => mi.ViewModelType == vm.GetType());
        if (selectedMenuItem != null)
        {
            SelectedMenuItem = selectedMenuItem;
        }

        CurrentPage = vm;
    }

    partial void OnSelectedMenuItemChanged(MenuItemTemplate? value)
    {
        if (value == null)
        {
            return;
        }

        // Only route to the menu's view model if it's not already loaded.
        if (SelectedMenuItem?.ViewModelType != _router.Current?.GetType())
        {
            _router.GoTo<PageViewModelBase>(value.ViewModelType);
        }
    }

    [RelayCommand]
    private void NavigateBack()
    {
        if (_router.CanGoBack)
        {
            _router.Back();
        }
    }

    [RelayCommand]
    private void TriggerMenuPane()
    {
        IsMenuPaneOpen = !IsMenuPaneOpen;
    }
}