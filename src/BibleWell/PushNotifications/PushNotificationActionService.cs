
namespace BibleWell.PushNotifications;

public class PushNotificationActionService : IPushNotificationActionService
{
    private readonly Dictionary<string, ActionEnum> _actionMappings = new()
    {
        { "action_a", ActionEnum.ActionA },
        { "action_b", ActionEnum.ActionB }
    };

    public event EventHandler<ActionEnum> ActionTriggered = delegate { };

    public void TriggerAction(string action)
    {
        if (!_actionMappings.TryGetValue(action, out var pushDemoAction))
        {
            return;
        }

        var exceptions = new List<Exception>();

        foreach (var handler in ActionTriggered?.GetInvocationList()!)
        {
            try
            {
                handler.DynamicInvoke(this, pushDemoAction);
            }
            catch (Exception ex)
            {
                exceptions.Add(ex);
            }
        }

        if (exceptions.Any())
        {
            throw new AggregateException(exceptions);
        }
    }
}