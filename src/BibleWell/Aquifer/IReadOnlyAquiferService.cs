namespace BibleWell.Aquifer;

public interface IReadOnlyAquiferService
{
    Task<IReadOnlyList<Language>> GetLanguagesAsync();
    Task<IReadOnlyList<ParentResource>> GetParentResourcesAsync();
    Task<ResourceContent?> GetResourceContentAsync(int contentId);
}