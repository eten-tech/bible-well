using BibleWell.App.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Extensions.DependencyInjection;

namespace BibleWell.App.ViewModels;

public partial class OpenFileViewModel : ViewModelBase
{
    [ObservableProperty]
    private string? _fileText;

    [RelayCommand]
    private async Task OpenFile(CancellationToken ct)
    {
        ErrorMessages?.Clear();
        try
        {
            var filesService = App.Current?.Services?.GetService<IFilesService>();
            if (filesService is null)
            {
                throw new NullReferenceException(nameof(filesService));
            }

            var file = await filesService.OpenFileAsync();
            if (file is null)
            {
                return;
            }

            await using var readStream = await file.OpenReadAsync();
            using var reader = new StreamReader(readStream);
            FileText = await reader.ReadToEndAsync(ct);
        }
        catch (Exception e)
        {
            ErrorMessages?.Add(e.Message);
        }
    }
}