using BibleWell.Storage;

namespace BibleWell.App.Desktop.Platform;

public sealed class DesktopStorageService : IStorageService
{
    public string AppDataDirectory => Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
}