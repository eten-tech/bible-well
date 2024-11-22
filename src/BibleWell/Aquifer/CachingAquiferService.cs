namespace BibleWell.Aquifer;

public class CachingAquiferService(IReadOnlyAquiferService _readOnlyAquiferService, IReadWriteAquiferService _readWriteAquiferService)
    : ICachingAquiferService
{
    public async Task<Resource?> GetResourceAsync(int id)
    {
        var resource = await _readWriteAquiferService.GetResourceAsync(id);
        if (resource == null)
        {
            resource = await _readOnlyAquiferService.GetResourceAsync(id);
            if (resource != null)
            {
                await _readWriteAquiferService.SaveResource(resource);
            }
        }

        return resource;
    }
}