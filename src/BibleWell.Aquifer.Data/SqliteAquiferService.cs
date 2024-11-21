namespace BibleWell.Aquifer.Data;

public sealed class SqliteAquiferService : IReadWriteAquiferService
{
    public Task<Resource?> GetResourceAsync(int id)
    {
        throw new NotImplementedException();
    }

    public Task SaveResource(Resource resource)
    {
        throw new NotImplementedException();
    }
}