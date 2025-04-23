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
        _router.CurrentViewModelChanged += (vm) => CurrentPage = vm;
        SelectedMenuItem = MenuItems[0];
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

    partial void OnSelectedMenuItemChanged(MenuItemTemplate? value)
    {
        if (value == null)
        {
            return;
        }

        _router.GoTo<PageViewModelBase>(value.ViewModelType);
    }

    [RelayCommand]
    private void TriggerMenuPane()
    {
        IsMenuPaneOpen = !IsMenuPaneOpen;
    }
}