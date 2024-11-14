using Avalonia;
using Avalonia.Controls;
using Avalonia.Platform.Storage;
using Microsoft.Maui.Storage;

namespace BibleWell.App.Services;

public class FilesService(Visual? control) : IFilesService
{
    private readonly IStorageProvider _storageProvider =
        (TopLevel.GetTopLevel(control) ?? throw new ArgumentNullException("No root available.")).StorageProvider;

    public async Task<IStorageFile?> OpenStorageProviderFileAsync()
    {
        var files = await _storageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Open File",
            AllowMultiple = false
        });
        return files.Count >= 1 ? files[0] : null;
    }

    public async Task<FileResult?> OpenFileAsync()
    {
        return await FilePicker.PickAsync();
    }
}