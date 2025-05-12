using Avalonia;
using Avalonia.Styling;
using BibleWell.Preferences;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace BibleWell.App.ViewModels.Pages;

/// <summary>
/// View model for use with the <see cref="Views.Pages.HomePageView" />.
/// </summary>
public partial class HomePageViewModel(Router _router, IUserPreferencesService _userPreferencesService) : PageViewModelBase
{
    [RelayCommand]
    public void ChangeTheme()
    {
        var newThemeVariant = Application.Current!.ActualThemeVariant == ThemeVariant.Dark
            ? ThemeVariant.Light
            : ThemeVariant.Dark;

        Application.Current!.RequestedThemeVariant = newThemeVariant;

        _userPreferencesService.Set(PreferenceKeys.ThemeVariant, newThemeVariant.ToString());
    }

    [RelayCommand]
    public void ChangeLanguage()
    {
        _router.GoTo<LanguagesPageViewModel>();
    }

    [RelayCommand]
    public void UseDefaultExperience()
    {
        _userPreferencesService.Set(PreferenceKeys.Experience, AppExperience.Default);
        WeakReferenceMessenger.Default.Send(new ExperienceChangedMessage(AppExperience.Default));
    }

    [RelayCommand]
    public void UseFiaExperience()
    {
        _userPreferencesService.Set(PreferenceKeys.Experience, AppExperience.Fia);
        WeakReferenceMessenger.Default.Send(new ExperienceChangedMessage(AppExperience.Fia));
    }
}