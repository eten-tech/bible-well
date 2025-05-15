namespace BibleWell.PushNotifications;

public interface IDeviceInstallationService
{
    string? Token { get; }
    bool NotificationsSupported { get; }
    string DeviceId { get; }
    DeviceInstallation GetDeviceInstallation(params string[] tags);
}