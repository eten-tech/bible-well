using BibleWell.PushNotifications;
using Firebase.Messaging;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace BibleWell.App.Android.Platform;

[Service(Exported = false)]
[IntentFilter(["com.google.firebase.MESSAGING_EVENT"])]
public class PushNotificationFirebaseMessagingService : FirebaseMessagingService
{
    private IPushNotificationActionService? _notificationActionService;
    private INotificationRegistrationService? _notificationRegistrationService;
    private IDeviceInstallationService? _deviceInstallationService;

    // Default parameterless constructor required by Android
    public PushNotificationFirebaseMessagingService()
    {
        // Services will be resolved in OnCreate
    }

    public override void OnCreate()
    {
        base.OnCreate();
        
        // Resolve dependencies using Ioc container
        _notificationActionService = Ioc.Default.GetService<IPushNotificationActionService>();
        _notificationRegistrationService = Ioc.Default.GetService<INotificationRegistrationService>();
        _deviceInstallationService = Ioc.Default.GetService<IDeviceInstallationService>();
    }

    public override void OnNewToken(string token)
    {
        if (_deviceInstallationService != null)
        {
            _deviceInstallationService.Token = token;

            _notificationRegistrationService?.RefreshRegistrationAsync()
                .ContinueWith((task) =>
                {
                    if (task.IsFaulted)
                    {
                        throw task.Exception;
                    }
                });
        }
    }

    public override void OnMessageReceived(RemoteMessage message)
    {
        base.OnMessageReceived(message);

        if (message.Data.TryGetValue("action", out var messageAction) && _notificationActionService != null)
        {
            _notificationActionService.TriggerAction(messageAction);
        }
    }
}