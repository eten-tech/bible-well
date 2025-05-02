using BibleWell.Models;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BibleWell.App.ViewModels.Components;

public partial class TiptapRendererViewModel : ViewModelBase
{
    [ObservableProperty]
    private TiptapModel<TiptapNode> _resourceContentTiptap = null!;
}