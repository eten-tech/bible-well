using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace BibleWell.App.ViewModels.Pages;

/// <summary>
/// View model for use with the <see cref="Views.Pages.HomePageView" />.
/// </summary>
public partial class HomePageViewModel : PageViewModelBase
{
    [ObservableProperty]
    private AppExperience _currentExperience;
    public HomePageViewModel()
    {
        UseExperience(AppExperience.Default);
    }

    [RelayCommand]
    public void UseExperience(AppExperience experience)
    {
        CurrentExperience = experience;
        WeakReferenceMessenger.Default.Send(new ExperienceChangedMessage(experience));
    }
}