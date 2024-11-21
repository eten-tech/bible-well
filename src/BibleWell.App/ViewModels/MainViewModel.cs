using System.Collections.ObjectModel;
using Avalonia.Controls;
using BibleWell.App.ViewModels.Pages;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.DependencyInjection;
using CommunityToolkit.Mvvm.Input;

namespace BibleWell.App.ViewModels;

public sealed partial class MainViewModel : ViewModelBase
{
    public MainViewModel()
    {
        // TODO figure out how to load this in the constructor
        //CurrentPage = GetPageViewModelFromMenuItemTemplate(MenuItems[0]);
    }

    [ObservableProperty]
    private ViewModelBase _currentPage = new HomePageViewModel(); // TODO figure out how to load this in the constructor

    [ObservableProperty]
    private bool _isMenuPaneOpen = true;

    [ObservableProperty]
    private MenuItemTemplate? _selectedMenuItem;

    public ObservableCollection<MenuItemTemplate> MenuItems { get; } =
    [
        new(typeof(HomePageViewModel)),
        new(typeof(BiblePageViewModel)),
        new(typeof(GuidePageViewModel)),
        new(typeof(ResourcesPageViewModel)),
        new(typeof(LibraryPageViewModel)),
    ];

    partial void OnSelectedMenuItemChanged(MenuItemTemplate? value)
    {
        if (value == null)
        {
            return;
        }

        CurrentPage = GetPageViewModelFromMenuItemTemplate(value);
    }

    private static PageViewModelBase GetPageViewModelFromMenuItemTemplate(MenuItemTemplate value)
    {
        var viewModel = Design.IsDesignMode
            ? Activator.CreateInstance(value.ViewModelType)
            : Ioc.Default.GetService(value.ViewModelType);

        return viewModel as PageViewModelBase
            ?? throw new InvalidOperationException(
                $"{nameof(MenuItemTemplate)}.{nameof(MenuItemTemplate.ViewModelType)} must derive from {nameof(PageViewModelBase)} but was {value.ViewModelType.Name}.");
    }

    [RelayCommand]
    private void TriggerMenuPane()
    {
        IsMenuPaneOpen = !IsMenuPaneOpen;
    }
}