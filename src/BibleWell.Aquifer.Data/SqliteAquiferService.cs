using BibleWell.Aquifer.Data.DbModels;

namespace BibleWell.Aquifer.Data;

public sealed class SqliteAquiferService(ResourceContentRepository resourceContentRepository) : IReadWriteAquiferService
{
// SQLite is not async, we have a common interface that does interact with an async API.
// This might change as we develop the application further. For now, we are telling the compiler to ignore this.
#pragma warning disable CS1998 // Async method lacks 'await' operators and will run synchronously
    public async Task<IReadOnlyList<Language>> GetLanguagesAsync()
    {
        return [];
    }

    public Task<IReadOnlyList<ParentResource>> GetParentResourcesAsync()
    {
        throw new NotImplementedException();
    }

    public async Task<ResourceContent?> GetResourceContentAsync(int id)
    {
        return MapToResourceContent(resourceContentRepository.GetById(id));
    }

    public async Task SaveLanguagesAsync(IReadOnlyList<Language> languages)
    {
        // TODO save languages
    }

    public async Task SaveResourceContentAsync(ResourceContent resourceContent)
    {
        resourceContentRepository.Save(MapToDbResourceContent(resourceContent));
    }

    private static ResourceContent? MapToResourceContent(DbResourceContent? source)
    {
        // SQLite INTEGER data type is always 64-bit. So, we have to account for potential overflows using `checked`
        return source == null
            ? null
            : new ResourceContent(checked((int)source.Id), source.Name, source.Content);
    }

    private static DbResourceContent MapToDbResourceContent(ResourceContent source)
    {
        return new DbResourceContent(source.Id, source.Name, source.Content);
    }
#pragma warning restore CS1998 // Async method lacks 'await' operators and will run synchronously

    // add other repositories here ...
}