namespace BibleWell.PushNotifications;

public interface IPushNotificationActionService : INotificationActionService, IDisposable
{
    /// <summary>
    /// Event that is triggered when a push notification action is received.
    /// </summary>
    event EventHandler<ActionEnum> ActionTriggered;
    
    void Initialize();
}

public class PushNotificationActionService : IPushNotificationActionService
{
    private readonly Dictionary<string, ActionEnum> _actionMappings = new()
    {
        { "action_a", ActionEnum.ActionA },
        { "action_b", ActionEnum.ActionB },
    };
    
    private bool _isInitialized;
    private bool _isDisposed;

    public event EventHandler<ActionEnum> ActionTriggered = delegate { };
    
    public void Initialize()
    {
        if (_isInitialized)
        {
            return;
        }

        // Subscribe to our own event to handle navigation
        ActionTriggered += HandleActionNavigation;
        _isInitialized = true;
    }
    
    private void HandleActionNavigation(object? sender, ActionEnum action)
    {
        switch (action)
        {
            // TODO take action here
            case ActionEnum.ActionA:
                // _router.GoTo<HomePageViewModel>();
                Console.WriteLine("ActionA triggered");
                break;
            case ActionEnum.ActionB:
                // _router.GoTo<BiblePageViewModel>();
                Console.WriteLine("ActionB triggered");
                break;
            default:
                // TODO take action here
                break;
        }
    }
    
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
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
    
    protected virtual void Dispose(bool disposing)
    {
        if (_isDisposed)
        {
            return;
        }

        if (disposing)
        {
            // Unsubscribe from our own event
            ActionTriggered -= HandleActionNavigation;
        }

        _isDisposed = true;
    }
}