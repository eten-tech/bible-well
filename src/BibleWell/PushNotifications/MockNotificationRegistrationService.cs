namespace BibleWell.PushNotifications;

/// <summary>
/// A mock implementation of INotificationRegistrationService that doesn't throw exceptions
/// for platforms where push notifications are not fully implemented.
/// </summary>
public class MockNotificationRegistrationService : INotificationRegistrationService
{
    public Task DeregisterDeviceAsync()
    {
        return Task.CompletedTask;
    }

    public Task RegisterDeviceAsync(params string[] tags)
    {
        return Task.CompletedTask;
    }

    public Task RefreshRegistrationAsync()
    {
        return Task.CompletedTask;
    }

    public Task RequestActionAAsync()
    {
        return Task.CompletedTask;
    }

    public Task RequestActionBAsync()
    {
        return Task.CompletedTask;
    }
}
