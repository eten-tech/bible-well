namespace BibleWell.PushNotifications;

public interface IPushNotificationWellApiService
{
    Task RegisterDeviceAsync(DeviceInstallation deviceInstallation);
    Task DeregisterDeviceAsync(string deviceId);
    // --- demo notification code: can be removed ---
    Task RequestActionAAsync();
    Task RequestActionBAsync();
}