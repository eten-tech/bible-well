namespace BibleWell.PushNotifications;

public interface INotificationRegistrationService
{
    Task DeregisterDeviceAsync();
    Task RegisterDeviceAsync(params string[] tags);
    Task RefreshRegistrationAsync();
    // --- demo notification code: can be removed ---
    Task RequestActionAAsync();
    Task RequestActionBAsync();
}