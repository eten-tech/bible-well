using BibleWell.Aquifer;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BibleWell.App.ViewModels.Pages;

/// <summary>
/// Design-time view model for use with the <see cref="Views.Pages.DevPageView"/>.
/// </summary>
public sealed class DesignDevPageViewModel() : DevPageViewModel(null!);

/// <summary>
/// View model for use with the <see cref="Views.Pages.DevPageView"/>.
/// </summary>
public partial class DevPageViewModel(IReadWriteAquiferService _sqliteAquiferService) : PageViewModelBase
{
    [ObservableProperty]
    private string _resourceContentHtml = "<p>Click the button to view content.</p>";

    [RelayCommand]
    public async Task LoadResourceContentAsync()
    {
        try
        {
            var resourceContent = await _sqliteAquiferService.GetResourceContentAsync(1);
            ResourceContentHtml = resourceContent?.Content ?? "resource not found";
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}