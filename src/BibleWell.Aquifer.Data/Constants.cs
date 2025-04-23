using BibleWell.Storage;

namespace BibleWell.Aquifer.Data;

internal sealed class Constants(IStorageService _storageService)
{
    private const string AquiferDatabaseFilename = "BibleWellAquifer.db3";
    private const string ApplicationDir = "BibleWell";

    private string ApplicationPath => Path.Combine(
                            _storageService.AppDataDirectory,
                            ApplicationDir);

    public string DatabasePath => Path.Combine(ApplicationPath, AquiferDatabaseFilename);

    public string ConnectionString => $"Data Source={DatabasePath}";
}