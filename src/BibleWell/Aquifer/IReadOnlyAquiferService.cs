namespace BibleWell.Aquifer;

public interface IReadOnlyAquiferService
{
    public Task<Resource?> GetResourceAsync(int id);
}