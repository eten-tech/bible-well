using Avalonia;
using Avalonia.Styling;
using BibleWell.Preferences;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;

namespace BibleWell.App.ViewModels.Pages;


/// <summary>
/// View model for use with the <see cref="Views.Pages.HomePageView" />.
/// </summary>
public partial class HomePageViewModel() : PageViewModelBase
{
    [RelayCommand]
    public void UseExperience(AppExperience experience)
    {
        WeakReferenceMessenger.Default.Send(new ExperienceChangedMessage(experience));
    }
}