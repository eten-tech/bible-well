using CommunityToolkit.Mvvm.Input;

namespace BibleWell.App.ViewModels.Pages;

/// <summary>
/// View model for use with the <see cref="Views.Pages.LanguagesPageView"/>.
/// </summary>
public partial class LanguagesPageViewModel(Router _router) : PageViewModelBase
{
    [RelayCommand]
    public void Close()
    {
        _router.Back();
    }
}