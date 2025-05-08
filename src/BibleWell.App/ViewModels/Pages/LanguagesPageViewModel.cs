using System.Collections.ObjectModel;
using System.Globalization;
using Avalonia;
using BibleWell.Aquifer;
using BibleWell.Preferences;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BibleWell.App.ViewModels.Pages;

/// <summary>
/// View model for use with the <see cref="Views.Pages.LanguagesPageView" />.
/// </summary>
public partial class LanguagesPageViewModel(
    Router _router,
    IUserPreferencesService _userPreferencesService,
    ICachingAquiferService _cachingAquiferService)
    : PageViewModelBase
{
    [ObservableProperty]
    private CultureInfo? _selectedCultureInfo;

    [ObservableProperty]
    private ObservableCollection<Language> _apiLanguages = [];

    public static ObservableCollection<CultureInfo> SupportedCultureInfos { get; } =
    [
        new("en-US"),
        new("es-ES"),
    ];

    [RelayCommand]
    public void Close()
    {
        _router.Back();
    }

    partial void OnSelectedCultureInfoChanged(CultureInfo? value)
    {
        if (value == null)
        {
            return;
        }

        _userPreferencesService.Set(PreferenceKeys.Language, value.Name);
        Thread.CurrentThread.CurrentUICulture = value;
        ((App)Application.Current!).ReloadMainView<HomePageViewModel>();
    }

    [RelayCommand]
    public async Task LoadLanguagesAsync()
    {
        ApiLanguages = [.. await _cachingAquiferService.GetLanguagesAsync()];
    }
}