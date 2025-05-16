namespace BibleWell.PushNotifications;

public interface INotificationActionService
{
    void TriggerAction(string action);
}