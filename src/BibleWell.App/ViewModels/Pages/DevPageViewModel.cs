using System.Collections.ObjectModel;
using System.Reflection;
using BibleWell.Aquifer;
using BibleWell.Devices;
using BibleWell.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;

namespace BibleWell.App.ViewModels.Pages;

/// <summary>
/// View model for use with the <see cref="Views.Pages.DevPageView"/>.
/// </summary>
public partial class DevPageViewModel(
    IApplicationInfoService _applicationInfoService,
    IDeviceService _deviceService,
    IStorageService _storageService,
    IReadWriteAquiferService _readWriteAquiferService,
    ILogger<DevPageViewModel> _logger)
    : PageViewModelBase
{
    public ObservableCollection<InfoItem> ApplicationInfoItems { get; } = [.. _applicationInfoService
        .GetType()
        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
        .Select(p => new InfoItem(p.Name, p.GetValue(_applicationInfoService)?.ToString() ?? ""))];

    public ObservableCollection<InfoItem> DeviceInfoItems { get; } = [.. _deviceService
        .GetType()
        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
        .Select(p => new InfoItem(p.Name, p.GetValue(_deviceService)?.ToString() ?? ""))];

    public ObservableCollection<InfoItem> StorageInfoItems { get; } = [.. _storageService
        .GetType()
        .GetProperties(BindingFlags.Instance | BindingFlags.Public)
        .Select(p => new InfoItem(p.Name, p.GetValue(_storageService)?.ToString() ?? ""))];

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

    [RelayCommand]
    public void LogTrace()
    {
        _logger.LogDebug("Test trace log message.");
    }

    [RelayCommand]
    public void LogDebug()
    {
        _logger.LogDebug("Test debug log message.");
    }

    [RelayCommand]
    public void LogInformation()
    {
        _logger.LogInformation("Test information log message.");
    }

    [RelayCommand]
    public void LogWarning()
    {
        _logger.LogWarning("Test warning log message.");
    }

    [RelayCommand]
    public void LogError()
    {
        _logger.LogError("Test error log message.");
    }

    [RelayCommand]
    public void LogCritical()
    {
        _logger.LogCritical("Test critical log message.");
    }

    [RelayCommand]
    public static void ThrowUnhandledException()
    {
        throw new InvalidOperationException("Test unhandled exception.");
    }

    public record InfoItem(string Name, string Value);
}