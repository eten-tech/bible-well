using Avalonia.Platform.Storage;
using Microsoft.Maui.Storage;

namespace BibleWell.App.Services;

public interface IFilesService
{
    public Task<IStorageFile?> OpenStorageProviderFileAsync();

    public Task<FileResult?> OpenFileAsync();
}