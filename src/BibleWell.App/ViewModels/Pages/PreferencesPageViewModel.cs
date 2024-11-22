using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Maui.Storage;

namespace BibleWell.App.ViewModels.Pages;

public sealed partial class PreferencesPageView : PageViewModelBase
{
    [ObservableProperty]
    private List<string> _languageList;
    [ObservableProperty]
    private string? _selectedLanguage;
    
    public PreferencesPageView()
    {
        LanguageList =
        [
            "English",
            "French",
            "Italian",
            "Dutch",
            "Spanish",
            "Estonian",
            "Portuguese",
        ];

        GetPreferredLanguage();
    }

    partial void OnSelectedLanguageChanged(string? value)
    {
        if (value == null)
        {
            return;
        }
        
        Debug.WriteLine(value);
        // set preference value
        Preferences.Default.Set("Language", value);
    }

    private void GetPreferredLanguage()
    {
        var preferredLanguage = Preferences.Default.Get("Language", string.Empty);
        if (string.IsNullOrEmpty(preferredLanguage))
        {
            SelectedLanguage = preferredLanguage;
        }
    }
}

