using BibleWell.PushNotifications;

namespace BibleWell.App.iOS.Platform;

public class IosNotificationDeviceInstallationService(iOSDeviceService _iOsDeviceInfo) : IDeviceInstallationService
{
    private const int SupportedVersionMajor = 13;
    private const int SupportedVersionMinor = 0;
    public string Token { get; set; } = string.Empty;

    // TODO: update with iOS support
    public bool NotificationsSupported => 
        UIDevice.CurrentDevice.CheckSystemVersion(SupportedVersionMajor, SupportedVersionMinor);

    public string DeviceId => _iOsDeviceInfo.DeviceId;

    public DeviceInstallation GetDeviceInstallation(params string[] tags)
    {
        throw new NotImplementedException();
    }
}