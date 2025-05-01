using CommunityToolkit.Mvvm.ComponentModel;

namespace BibleWell.App.ViewModels.Components;

public partial class TiptapRendererViewModel : ViewModelBase
{
    [ObservableProperty]
    private string _resourceContentTiptapJson = "";
}
