namespace BibleWell.Aquifer;

public class CachingAquiferService(IReadOnlyAquiferService _readOnlyAquiferService, IReadWriteAquiferService _readWriteAquiferService)
    : ICachingAquiferService
{
    public async Task<IReadOnlyList<Language>> GetLanguagesAsync()
    {
        var languages = await _readWriteAquiferService.GetLanguagesAsync();
        if (languages.Count == 0)
        {
            languages = await _readOnlyAquiferService.GetLanguagesAsync();
            if (languages.Count != 0)
            {
                await _readWriteAquiferService.SaveLanguagesAsync(languages);
            }
        }

        return languages;
    }

    public async Task<ResourceContent?> GetResourceContentAsync(int contentId)
    {
        var resource = await _readWriteAquiferService.GetResourceContentAsync(contentId);
        if (resource == null)
        {
            resource = await _readOnlyAquiferService.GetResourceContentAsync(contentId);
            if (resource != null)
            {
                await _readWriteAquiferService.SaveResourceContentAsync(resource);
            }
        }

        return resource;
    }
}