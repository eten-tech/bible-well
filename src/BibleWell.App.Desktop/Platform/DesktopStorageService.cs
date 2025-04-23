using BibleWell.Storage;

namespace BibleWell.App.Desktop.Platform;

public sealed class DesktopStorageService : IStorageService
{
    public string AppDataDirectory => Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

    public string ApplicationDirectoryPath => Path.Combine(
        AppDataDirectory,
        Constants.ApplicationDir);
        
    public string DatabasePath => Path.Combine(
        ApplicationDirectoryPath,
        Constants.AquiferDatabaseFilename);

    public string ConnectionString => $"Data Source={DatabasePath}";
}