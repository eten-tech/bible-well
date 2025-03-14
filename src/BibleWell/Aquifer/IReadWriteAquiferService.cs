namespace BibleWell.Aquifer;

public interface IReadWriteAquiferService : IReadOnlyAquiferService
{
    Task SaveResource(Resource resource);
}