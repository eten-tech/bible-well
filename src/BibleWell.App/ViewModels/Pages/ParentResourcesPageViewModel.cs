using System.Collections.ObjectModel;
using BibleWell.Aquifer;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BibleWell.App.ViewModels.Pages;

public partial class ParentResourcesPageViewModel(
    Router _router,
    ICachingAquiferService _cachingAquiferService)
    : PageViewModelBase
{
    [ObservableProperty]
    private ObservableCollection<ParentResource> _apiParentResources = [];
    
    [RelayCommand]
    public void Close()
    {
        _router.Back();
    }
    
    [RelayCommand]
    public async Task LoadParentResourcesAsync()
    {
        ApiParentResources = [.. await _cachingAquiferService.GetParentResourcesAsync()];
    }
}