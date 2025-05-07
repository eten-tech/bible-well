namespace BibleWell.Aquifer;

public interface IReadWriteAquiferService : IReadOnlyAquiferService
{
    Task SaveLanguagesAsync(IReadOnlyList<Language> languages);
    Task SaveResourceContentAsync(ResourceContent resource);
}