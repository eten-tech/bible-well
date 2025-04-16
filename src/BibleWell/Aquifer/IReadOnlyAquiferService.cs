namespace BibleWell.Aquifer;

public interface IReadOnlyAquiferService
{
    Task<ResourceContent?> GetResourceContentAsync(int contentId);
}