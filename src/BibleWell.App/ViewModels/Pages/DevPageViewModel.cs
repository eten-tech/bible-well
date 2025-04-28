using System.Collections.ObjectModel;
using System.Reflection;
using BibleWell.Aquifer;
using BibleWell.Devices;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace BibleWell.App.ViewModels.Pages;

/// <summary>
/// View model for use with the <see cref="Views.Pages.DevPageView"/>.
/// </summary>
public partial class DevPageViewModel(
    IApplicationInfoService _applicationInfoService,
    IDeviceService _deviceService,
    IReadWriteAquiferService _readWriteAquiferService)
    : PageViewModelBase
{
    public ObservableCollection<InfoItem> InfoItems { get; } = [.. _applicationInfoService
        .GetType()
        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
        .Select(p => new InfoItem($"Application.{p.Name}", p.GetValue(_applicationInfoService)?.ToString() ?? ""))
        .Concat(
            _deviceService
                .GetType()
                .GetProperties(BindingFlags.Instance | BindingFlags.Public)
                .Select(p => new InfoItem($"Device.{p.Name}", p.GetValue(_deviceService)?.ToString() ?? "")))];

    [ObservableProperty]
    private string _resourceContentHtml = "<p>Click the button to view content.</p>";

    [RelayCommand]
    public async Task LoadResourceContentAsync()
    {
        try
        {
            var resourceContent = await _readWriteAquiferService.GetResourceContentAsync(1);
            ResourceContentHtml = resourceContent?.Content ?? "resource not found";
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    public record InfoItem(string Name, string Value);
}