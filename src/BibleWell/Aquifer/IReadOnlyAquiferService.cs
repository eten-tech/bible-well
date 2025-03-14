namespace BibleWell.Aquifer;

public interface IReadOnlyAquiferService
{
    Task<Resource?> GetResourceAsync(int id);
}