namespace BibleWell.Aquifer;

public interface IReadWriteAquiferService : IReadOnlyAquiferService
{
    public Task SaveResource(Resource resource);
}