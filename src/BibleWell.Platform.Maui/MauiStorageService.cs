using BibleWell.Storage;
using Microsoft.Maui.Storage;

namespace BibleWell.Platform.Maui;

public sealed class MauiStorageService : IStorageService
{
    public string AppDataDirectory => FileSystem.Current.AppDataDirectory;

    public string ApplicationDirectoryPath => Path.Combine(
        AppDataDirectory,
        Constants.ApplicationDir);
    
    public string DatabasePath => Path.Combine(
        ApplicationDirectoryPath,
        Constants.AquiferDatabaseFilename);

    public string ConnectionString => $"Data Source={DatabasePath}";
}