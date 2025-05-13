using BibleWell.PushNotifications;

namespace BibleWell.App.Desktop.Platform;

public class DesktopNotificationDeviceInstallationService() : IDeviceInstallationService
{
    public string Token { get; set; } = string.Empty;

    // TODO: update with desktop support
    public bool NotificationsSupported => false;

    public string DeviceId => string.Empty;

    public DeviceInstallation GetDeviceInstallation(params string[] tags)
    {
        throw new NotImplementedException();
    }
}