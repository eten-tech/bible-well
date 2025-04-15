namespace BibleWell.Aquifer;

public interface IReadWriteAquiferService : IReadOnlyAquiferService
{
    Task SaveResourceContent(ResourceContent resource);
}