using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Maui.Media;
using Microsoft.Maui.Storage;

namespace BibleWell.App.ViewModels.Pages;

public sealed partial class DevPageViewModel : PageViewModelBase
{
    [ObservableProperty]
    private string _fileName = "Click button to show file picker...";

    [RelayCommand]
    public async Task PickFileAsync()
    {
        try
        {
            var file = OperatingSystem.IsAndroid() || OperatingSystem.IsIOS()
                ? await FilePicker.PickAsync()
                : null;

            FileName = file is null
                ? "No file selected."
                : $"Found file: \"{file.FileName}\"";
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }

    [RelayCommand]
    public async Task SpeakFileNameAsync()
    {
        try
        {
            await TextToSpeech.Default.SpeakAsync(FileName);
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex);
            throw;
        }
    }
}