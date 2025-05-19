using System.Collections.ObjectModel;
using BibleWell.App.Resources;
using BibleWell.App.ViewModels.Pages;
using BibleWell.Preferences;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using CommunityToolkit.Mvvm.Messaging.Messages;

namespace BibleWell.App.ViewModels;

/// <summary>
/// View model for use with the <see cref="Views.MainView" />.
/// </summary>
public partial class MainViewModel : ViewModelBase
{

    private readonly Router _router;

    private readonly IUserPreferencesService _userPreferencesService;

    [ObservableProperty]
    private ViewModelBase _currentPage = null!;

    [ObservableProperty]
    private AppExperience _experience;

    [ObservableProperty]
    private bool _isMenuPaneOpen;

    [ObservableProperty]
    private ObservableCollection<MenuItemTemplate> _menuItems;

    [ObservableProperty]
    private bool _navMenuVisible;

    [ObservableProperty]
    private MenuItemTemplate? _selectedMenuItem;

    public MainViewModel(Router router, IUserPreferencesService userPreferencesService)
    {
        _router = router;
        _router.CurrentViewModelChanged += OnRouterCurrentViewModelChanged;
        MenuItems = [];
        RegisterExperienceChangedHandler();
        _userPreferencesService = userPreferencesService;
        Experience = (AppExperience)userPreferencesService.Get(PreferenceKeys.Experience, (int)AppExperience.None);
        InitializeMenuItems();
        // TODO: This is where we'd have the router go to the "First Time User Welcome" page (BIB-934), since there are no menu items
        _router.GoTo<PageViewModelBase>(
            MenuItems.Count == 0
                ? new MenuItemTemplate(typeof(HomePageViewModel), "HomeRegular").ViewModelType
                : MenuItems[0].ViewModelType);

        if (!ResourceHelper.IsSupportedCulture(App.GetApplicationCulture()))
        {
            // TODO BIB-934 open a modal instead
            _router.GoTo<LanguagesPageViewModel>();
        }
    }

    private void RegisterExperienceChangedHandler()
    {
        WeakReferenceMessenger.Default.Register<ExperienceChangedMessage>(
            this,
            (_, m) =>
            {
                Experience = m.Value;
                _userPreferencesService.Set(PreferenceKeys.Experience, (int)m.Value);
                InitializeMenuItems();
            });
    }

    private void InitializeMenuItems()
    {
        MenuItems.Clear();
        switch (Experience)
        {
            case AppExperience.None:
                NavMenuVisible = false;
                break;
            case AppExperience.Default:
                NavMenuVisible = true;
                MenuItems.Add(new MenuItemTemplate(typeof(HomePageViewModel), "BibleWellIconBrush", true, "BibleWellIconOutlineBrush"));
                MenuItems.Add(new MenuItemTemplate(typeof(BiblePageViewModel), "BookOpenRegular"));
                MenuItems.Add(new MenuItemTemplate(typeof(GuidePageViewModel), "CompassNorthwestRegular"));
                MenuItems.Add(new MenuItemTemplate(typeof(ResourcesPageViewModel), "ClipboardRegular"));
                MenuItems.Add(new MenuItemTemplate(typeof(LibraryPageViewModel), "LibraryRegular"));
                MenuItems.Add(new MenuItemTemplate(typeof(DevPageViewModel), "WindowDevToolsRegular"));
                break;
            case AppExperience.Fia:
                NavMenuVisible = true;
                MenuItems.Add(new MenuItemTemplate(typeof(HomePageViewModel), "BibleWellIconBrush", true, "BibleWellIconOutlineBrush"));
                MenuItems.Add(new MenuItemTemplate(typeof(BiblePageViewModel), "BookOpenRegular"));
                MenuItems.Add(new MenuItemTemplate(typeof(GuidePageViewModel), "CompassNorthwestRegular"));
                MenuItems.Add(new MenuItemTemplate(typeof(ResourcesPageViewModel), "ClipboardRegular"));
                break;
            // TODO: This should go to the Welcome Page and log an error BIB-934
            default:
                throw new NotImplementedException();
        }
    }

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

        foreach (var item in MenuItems)
        {
            item.IsSelected = item == value;
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

public class ExperienceChangedMessage(AppExperience newExperience) : ValueChangedMessage<AppExperience>(newExperience)
{
    public AppExperience NewExperience { get; init; } = newExperience;

    public void Deconstruct(out AppExperience newExperience)
    {
        newExperience = NewExperience;
    }
}