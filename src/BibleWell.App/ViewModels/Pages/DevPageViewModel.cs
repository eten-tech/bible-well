using System.Collections.ObjectModel;
using System.Reflection;
using Avalonia;
using BibleWell.App.Configuration;
using BibleWell.Aquifer;
using BibleWell.Devices;
using BibleWell.PushNotifications;
using BibleWell.Storage;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace BibleWell.App.ViewModels.Pages;

/// <summary>
/// View model for use with the <see cref="Views.Pages.DevPageView" />.
/// </summary>
public partial class DevPageViewModel(
    IApplicationInfoService _applicationInfoService,
    IDeviceService _deviceService,
    IStorageService _storageService,
    IReadWriteAquiferService _readWriteAquiferService,
    IOptions<ConfigurationOptions> _configurationOptions,
    ILogger<DevPageViewModel> _logger,
    INotificationRegistrationService _notificationRegistrationService)
    : PageViewModelBase
{
    [ObservableProperty]
    private string _resourceContentHtml = "<p>Click the button to view content.</p>";

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

    [RelayCommand]
    public async Task LoadResourceContentAsync()
    {
        try
        {
            var resourceContent = await _readWriteAquiferService.GetResourceContentAsync(contentId: 1);
            ResourceContentHtml = resourceContent?.Content ?? "resource not found";
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    [RelayCommand]
    public void LogTestMessage(LogLevel logLevel)
    {
        _logger.Log(logLevel, "Test {LogLevel} log message.", logLevel);
    }

    [RelayCommand]
    public void ThrowUnhandledException()
    {
        throw new InvalidOperationException("Test unhandled exception.");
    }

    [RelayCommand]
    public void ChangeEnvironment(AppEnvironment environment)
    {
        ((App)Application.Current!).ReloadApplication<DevPageViewModel>(environment);
    }

    public record InfoItem(string Name, string Value);

    // --- push notifications ---
    [ObservableProperty]
    private string _pushText = "";
    [RelayCommand]
    public void RegisterDeviceButtonClicked()
    {
        _notificationRegistrationService.RegisterDeviceAsync()
            .ContinueWith((task) => ShowAlert(task.IsFaulted ? task.Exception.Message : $"Device registered"));
    }

    [RelayCommand]
    public void DeregisterDeviceButtonClicked()
    {
        _notificationRegistrationService.DeregisterDeviceAsync()
            .ContinueWith((task) => ShowAlert(task.IsFaulted ? task.Exception.Message : $"Device deregistered"));
    }

    [RelayCommand]
    public void RequestActionAClicked()
    {
        _notificationRegistrationService.RequestActionAAsync()
            .ContinueWith((task) => ShowAlert(task.IsFaulted ? task.Exception.Message : $"Action A requested"));
    }

    [RelayCommand]
    public void RequestActionBClicked()
    {
        _notificationRegistrationService.RequestActionBAsync()
            .ContinueWith((task) => ShowAlert(task.IsFaulted ? task.Exception.Message : $"Action B requested"));
    }
    
    // status = await Permissions.RequestAsync<Permissions.PostNotifications>(); // using maui essentials.

    private void ShowAlert(string message)
    {
        PushText = message;
    }
}