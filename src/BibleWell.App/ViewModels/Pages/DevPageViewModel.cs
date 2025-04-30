using System.Collections.ObjectModel;
using System.Reflection;
using BibleWell.App.Configuration;
using BibleWell.Aquifer;
using BibleWell.Devices;
using BibleWell.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BibleWell.App.ViewModels.Pages;

/// <summary>
/// View model for use with the <see cref="Views.Pages.DevPageView"/>.
/// </summary>
public partial class DevPageViewModel(
    IApplicationInfoService _applicationInfoService,
    IDeviceService _deviceService,
    IStorageService _storageService,
    IReadWriteAquiferService _readWriteAquiferService,
    IOptions<ConfigurationOptions> _configurationOptions,
    ILogger<DevPageViewModel> _logger)
    : PageViewModelBase
{
    public ObservableCollection<InfoItem> ApplicationInfoItems { get; } =
        [.. GetInfoItems(_applicationInfoService.GetType(), _applicationInfoService)];

    public ObservableCollection<InfoItem> DeviceInfoItems { get; } = [.. GetInfoItems(_deviceService.GetType(), _deviceService)];

    public ObservableCollection<InfoItem> EnvironmentConfigurationItems { get; } =
        [.. GetInfoItems(_configurationOptions.Value.GetType(), _configurationOptions.Value)];

    public ObservableCollection<InfoItem> StorageInfoItems { get; } = [.. GetInfoItems(_storageService.GetType(), _storageService)];

    private static IEnumerable<InfoItem> GetInfoItems(Type serviceType, object service, string prefix = "")
    {
        return serviceType
            .GetProperties(BindingFlags.Instance | BindingFlags.Public)
            .SelectMany(p =>
            {
                var propertyValue = p.GetValue(service);
                if (propertyValue is null)
                {
                    return [];
                }

                var propertyDisplay = $"{prefix}{(string.IsNullOrEmpty(prefix) ? "" : ".")}{p.Name}";

                if (p.PropertyType.IsClass && p.PropertyType.Assembly.GetName().Name?.StartsWith("BibleWell") == true)
                {
                    return GetInfoItems(p.PropertyType, propertyValue, propertyDisplay);
                }

                return [new InfoItem(propertyDisplay, p.GetValue(service)?.ToString() ?? "")];
            });
    }

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