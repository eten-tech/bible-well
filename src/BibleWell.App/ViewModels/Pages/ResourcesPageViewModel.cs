using BibleWell.Aquifer;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BibleWell.App.ViewModels.Pages;

public sealed partial class ResourcesPageViewModel(ICachingAquiferService _cachingAquiferService)
    : PageViewModelBase
{
    [ObservableProperty]
    private string _resourceContent = "Click the button to view resource text...";

    [RelayCommand]
    public async Task PopulateResourceContentAsync()
    {
        try
        {
            ResourceContent = (await _cachingAquiferService.GetResourceAsync(42))
                ?.Content
                ?? "Resource not found.";
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}