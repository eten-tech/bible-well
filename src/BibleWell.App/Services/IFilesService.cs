using Avalonia.Platform.Storage;

namespace BibleWell.App.Services;

public interface IFilesService
{
    public Task<IStorageFile?> OpenFileAsync();
}