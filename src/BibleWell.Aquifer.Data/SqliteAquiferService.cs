namespace BibleWell.Aquifer.Data;

public sealed class SqliteAquiferService : IReadWriteAquiferService
{
    public Task<ResourceContent?> GetResourceContentAsync(int contentId)
    {
        return Task.FromResult<ResourceContent?>(null);
    }

    public Task SaveResourceContent(ResourceContent resource)
    {
        return Task.CompletedTask;
    }
}