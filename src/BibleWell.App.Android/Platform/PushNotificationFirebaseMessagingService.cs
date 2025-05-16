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
    }

    public override void OnNewToken(string token)
    {
        // Store the token in the AndroidApp class
        AndroidApp.SetPushNotificationToken(token);

        // Refresh registration with the backend
        _notificationRegistrationService?.RefreshRegistrationAsync()
            .ContinueWith((task) =>
            {
                if (task.IsFaulted)
                {
                    throw task.Exception;
                }
            });
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