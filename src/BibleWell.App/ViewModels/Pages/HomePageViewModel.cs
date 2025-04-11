using Avalonia;
using Avalonia.Styling;
using CommunityToolkit.Mvvm.Input;

namespace BibleWell.App.ViewModels.Pages;

public sealed partial class HomePageViewModel : PageViewModelBase
{
    [RelayCommand]
    public void ChangeTheme()
    {
        Application.Current!.RequestedThemeVariant = Application.Current!.ActualThemeVariant == ThemeVariant.Dark
            ? ThemeVariant.Light
            : ThemeVariant.Dark;
    }
}