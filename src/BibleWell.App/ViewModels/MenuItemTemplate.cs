using BibleWell.App.ViewModels.Pages;
using CommunityToolkit.Mvvm.ComponentModel;

namespace BibleWell.App.ViewModels;

public partial class MenuItemTemplate(Type viewModelType, string iconName, bool isSvg = false, string? iconNameInactive = null)
    : ObservableObject
{

    [ObservableProperty]
    private bool _isSelected;

    // TODO localization
    public string Label { get; } = viewModelType.Name.Replace("PageViewModel", "");

    public Type ViewModelType { get; } = viewModelType.BaseType == typeof(PageViewModelBase)
        ? viewModelType
        : throw new ArgumentException($"Type must derive from {nameof(PageViewModelBase)}.", nameof(viewModelType));
    public string IconName { get; } = iconName;

    public string? IconNameInactive { get; } = iconNameInactive ?? iconName;
    public bool IsSvg { get; } = isSvg;
    public bool ShowActiveIndicator => IsSvg && IsSelected;
    public bool ShowInactiveIndicator => IsSvg && !IsSelected;

    partial void OnIsSelectedChanged(bool oldValue, bool newValue)
    {
        OnPropertyChanged(nameof(ShowActiveIndicator));
        OnPropertyChanged(nameof(ShowInactiveIndicator));
    }
}