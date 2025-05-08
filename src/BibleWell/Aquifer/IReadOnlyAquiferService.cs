using BibleWell.PushNotifications;

namespace BibleWell.Aquifer;

public interface IReadOnlyAquiferService
{
    Task<ResourceContent?> GetResourceContentAsync(int contentId);

    // todo: rethink this interface and not use default implementations 
    Task<DeviceInstallation?> RegisterDeviceAsync(DeviceInstallation deviceInstallation)
    {
        return Task.FromResult<DeviceInstallation?>(null);
    }

    Task DeRegisterDeviceAsync(string deviceId)
    {
        return Task.CompletedTask;
    }

    // --- demo notification code: can be removed ---
    Task<bool> RequestActionAAsync()
    {
        return Task.FromResult(true);
    }
    Task<bool> RequestActionBAsync()
    {
        return Task.FromResult(true);
    }
}