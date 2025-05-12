using BibleWell.PushNotifications;

namespace BibleWell.Aquifer;

public interface IReadOnlyAquiferService
{
    Task<IReadOnlyList<Language>> GetLanguagesAsync();
    Task<ResourceContent?> GetResourceContentAsync(int contentId);

    // todo: rethink this interface and not use default implementations 
    Task RegisterDeviceAsync(DeviceInstallation deviceInstallation)
    {
        throw new NotImplementedException();
    }

    Task DeRegisterDeviceAsync(string deviceId)
    {
        throw new NotImplementedException();
    }

    // --- demo notification code: can be removed ---
    Task RequestActionAAsync()
    {
        throw new NotImplementedException();
    }
    Task RequestActionBAsync()
    {
        throw new NotImplementedException();
    }
}