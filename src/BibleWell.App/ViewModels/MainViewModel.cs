using System.Collections.ObjectModel;
using BibleWell.App.ViewModels.Pages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BibleWell.App.ViewModels;

/// <summary>
/// View model for use with the <see cref="Views.MainView"/>.
/// </summary>
public partial class MainViewModel : ViewModelBase
{
    [ObservableProperty]
    private ViewModelBase _currentPage = null!;

    [ObservableProperty]
    private bool _isMenuPaneOpen;

    [ObservableProperty]
    private MenuItemTemplate? _selectedMenuItem;

    private readonly Router<ViewModelBase> _router;

    public MainViewModel(Router<ViewModelBase> router)
    {
        _router = router;
        _router.CurrentViewModelChanged += OnRouterCurrentViewModelChanged;
        _router.GoTo<PageViewModelBase>(MenuItems[0].ViewModelType);
    }

    private void OnRouterCurrentViewModelChanged(ViewModelBase vm)
    {
        // It's possible for any view model to use the router to navigate to another view model.
        // Therefore, if the view model changes we need to update the selected menu item.
        SelectedMenuItem = MenuItems.FirstOrDefault(mi => mi.ViewModelType == vm.GetType());
        CurrentPage = vm;
    }

    public static ObservableCollection<MenuItemTemplate> MenuItems { get; } =
    [
        new(typeof(HomePageViewModel), "HomeRegular"),
        new(typeof(BiblePageViewModel), "BookOpenRegular"),
        new(typeof(GuidePageViewModel), "CompassNorthwestRegular"),
        new(typeof(ResourcesPageViewModel), "ClipboardRegular"),
        new(typeof(LibraryPageViewModel), "LibraryRegular"),
#if DEBUG
        new(typeof(DevPageViewModel), "WindowDevToolsRegular"),
#endif
    ];

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