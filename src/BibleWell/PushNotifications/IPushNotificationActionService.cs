
namespace BibleWell.PushNotifications;

public interface IPushNotificationActionService : INotificationActionService
{
    event EventHandler<ActionEnum> ActionTriggered;
}