namespace BibleWell.Aquifer.Data;

/// <summary>
/// Main service implementation that uses repositories to access data
/// </summary>
public sealed class SqliteAquiferService : IReadWriteAquiferService
{
    private readonly ResourceRepository _resourceRepository;

    public SqliteAquiferService()
    {
        var dbManager = new SqliteDbManager(Constants.ConnectionString);
        _resourceRepository = new ResourceRepository(dbManager);
    }

    public Task<Resource?> GetResourceAsync(int id)
    {
        return _resourceRepository.GetByIdAsync(id);
    }

    public Task SaveResource(Resource resource)
    {
        return _resourceRepository.SaveAsync(resource);
    }

    // add other repositories here ...
}