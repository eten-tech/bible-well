using Android.Gms.Common;
using BibleWell.Devices;
using BibleWell.PushNotifications;
using MonoAndroidApplication = Android.App.Application;

namespace BibleWell.App.Android.Platform;

public class AndroidNotificationDeviceInstallationService(IDeviceService _androidDeviceInfo) : IDeviceInstallationService
{
    public string Token { get; set; } = string.Empty;

    public bool NotificationsSupported => 
        GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(MonoAndroidApplication.Context) == ConnectionResult.Success;

    public string DeviceId => _androidDeviceInfo.DeviceId;

    public DeviceInstallation GetDeviceInstallation(params string[] tags)
    {
        if (!NotificationsSupported)
        {
            throw new Exception(GetPlayServicesError());
        }

        if (string.IsNullOrWhiteSpace(Token))
        {
            throw new Exception("Unable to resolve token for FCMv1.");
        }

        if (DeviceId == string.Empty)
        {
            throw new Exception("Unable to resolve device id.");
        }

        var installation = new DeviceInstallation
        {
            InstallationId = DeviceId,
            Platform = "fcmv1",
            PushChannel = Token,
            Tags = [] // todo custom tags programatically
        };

        installation.Tags.AddRange(tags);

        return installation;
    }

    public string GetPlayServicesError()
    {
        var resultCode = GoogleApiAvailability.Instance.IsGooglePlayServicesAvailable(MonoAndroidApplication.Context);

        if (resultCode != ConnectionResult.Success)
        {
            return GoogleApiAvailability.Instance.IsUserResolvableError(resultCode) ?
                       GoogleApiAvailability.Instance.GetErrorString(resultCode) :
                       "This device isn't supported.";
        }

        return "An error occurred preventing the use of push notifications.";
    }
}