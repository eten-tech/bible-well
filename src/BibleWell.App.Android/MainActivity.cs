using Android.Content;
using Android.Content.PM;
using Avalonia;
using Avalonia.Android;
using BibleWell.PushNotifications;
using Firebase.Messaging;
using Kotlin;
using IOnSuccessListener = Android.Gms.Tasks.IOnSuccessListener;
using CommunityToolkit.Mvvm.DependencyInjection;
using Exception = System.Exception;

namespace BibleWell.App.Android;

[Activity(
    Label = "BibleWell.App.Android",
    Theme = "@style/MyTheme.NoActionBar",
    Icon = "@drawable/icon",
    MainLauncher = true,
    LaunchMode = LaunchMode.SingleTop,
    ConfigurationChanges = ConfigChanges.Orientation | ConfigChanges.ScreenSize | ConfigChanges.UiMode)]
public class MainActivity : AvaloniaMainActivity<AndroidApp>, IOnSuccessListener
{
    private IPushNotificationActionService? _notificationActionService;
    private IDeviceInstallationService? _deviceInstallationService;

    // Default parameterless constructor required by Android
    public MainActivity()
    {
    }

    protected override AppBuilder CustomizeAppBuilder(AppBuilder builder)
    {
        return base.CustomizeAppBuilder(builder)
            .WithInterFont();
    }

    protected override void OnCreate(Bundle? savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        Microsoft.Maui.ApplicationModel.Platform.Init(this, savedInstanceState);

        // Resolve dependencies using Ioc container
        _notificationActionService = Ioc.Default.GetService<IPushNotificationActionService>();
        _deviceInstallationService = Ioc.Default.GetService<IDeviceInstallationService>();

        if (_deviceInstallationService?.NotificationsSupported == true)
        {
            FirebaseMessaging.Instance.GetToken().AddOnSuccessListener(this);
        }

        ProcessNotificationAction(Intent!); // todo null check?
    }

    public void OnSuccess(Java.Lang.Object? result)
    {
        if (_deviceInstallationService != null)
        {
            _deviceInstallationService.Token = result?.ToString() ?? "";
        }
    }

    protected override void OnNewIntent(Intent? intent)
    {
        base.OnNewIntent(intent);
        ProcessNotificationAction(intent!); // todo: null check ?
    }

    public void ProcessNotificationAction(Intent intent)
    {
        try
        {
            if (intent?.HasExtra("action") == true && _notificationActionService != null)
            {
                var action = intent.GetStringExtra("action");

                if (!string.IsNullOrEmpty(action))
                {
                    _notificationActionService.TriggerAction(action);
                }
            }
        }
        catch (Exception e)
        {
            throw new NotImplementedError(e.Message!);
        }
    }
}