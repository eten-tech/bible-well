using System.Text.Json;
using BibleWell.PushNotifications;
using Microsoft.Maui.Storage;

namespace BibleWell.Platform.Maui;

public class NotificationRegistrationService(
    IPushNotificationWellApiService _wellApiService,
    IDeviceInstallationService _deviceInstallationService) 
    : INotificationRegistrationService
{
    private const string CachedDeviceTokenKey = "cached_device_token";
    private const string CachedTagsKey = "cached_tags";

    public async Task DeregisterDeviceAsync()
    {
        var cachedToken = await SecureStorage.GetAsync(CachedDeviceTokenKey)
            .ConfigureAwait(false);

        if (cachedToken == null)
        {
            return;
        }

        if (string.IsNullOrWhiteSpace(_deviceInstallationService.DeviceId))
        {
            throw new Exception("Unable to resolve an ID for the device.");
        }

        await _wellApiService.DeregisterDeviceAsync(_deviceInstallationService.DeviceId);

        SecureStorage.Remove(CachedDeviceTokenKey);
        SecureStorage.Remove(CachedTagsKey);
    }

    public async Task RegisterDeviceAsync(params string[] tags)
    {
        var deviceInstallation = _deviceInstallationService?.GetDeviceInstallation(tags);

        await _wellApiService.RegisterDeviceAsync(deviceInstallation!);

        await SecureStorage.SetAsync(CachedDeviceTokenKey, deviceInstallation!.PushChannel)
            .ConfigureAwait(false);

        await SecureStorage.SetAsync(CachedTagsKey, JsonSerializer.Serialize(tags));
    }

    public async Task RefreshRegistrationAsync()
    {
        var cachedToken = await SecureStorage.GetAsync(CachedDeviceTokenKey)
            .ConfigureAwait(false);

        var serializedTags = await SecureStorage.GetAsync(CachedTagsKey)
            .ConfigureAwait(false);

        if (string.IsNullOrWhiteSpace(cachedToken) ||
            string.IsNullOrWhiteSpace(serializedTags) ||
            string.IsNullOrWhiteSpace(_deviceInstallationService.Token) ||
            cachedToken == _deviceInstallationService.Token)
        {
            return;
        }

        var tags = JsonSerializer.Deserialize<string[]>(serializedTags);

        await RegisterDeviceAsync(tags!);
    }

    // --- demo code to trigger notifications ---
    public async Task RequestActionAAsync()
    {
        await _wellApiService.RequestActionAAsync();
    }

    public async Task RequestActionBAsync()
    {
        await _wellApiService.RequestActionBAsync();
    }
}