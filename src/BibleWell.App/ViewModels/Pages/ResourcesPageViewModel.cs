using BibleWell.Aquifer;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Storage;

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
            var file = await FilePicker.PickAsync();
            ResourceContent = file is null
                ? (await _cachingAquiferService.GetResourceAsync(42))
                    ?.Content
                    ?? "Resource not found."
                : $"Found file: \"{file.FileName}\"";
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}