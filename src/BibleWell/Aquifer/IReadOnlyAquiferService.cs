namespace BibleWell.Aquifer;

public interface IReadOnlyAquiferService
{
    Task<IReadOnlyList<Language>> GetLanguagesAsync();
    Task<ResourceContent?> GetResourceContentAsync(int contentId);
}