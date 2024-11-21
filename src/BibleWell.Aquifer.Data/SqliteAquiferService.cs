namespace BibleWell.Aquifer.Data;

public sealed class SqliteAquiferService : IReadWriteAquiferService
{
    public Task<Resource?> GetResourceAsync(int id)
    {
        return Task.FromResult<Resource?>(new Resource(42, "Really cool resource text!"));
    }

    public Task SaveResource(Resource resource)
    {
        throw new NotImplementedException();
    }
}