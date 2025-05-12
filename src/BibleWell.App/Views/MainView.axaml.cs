using System.ComponentModel;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using BibleWell.App.ViewModels;

namespace BibleWell.App.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
        DataContextChanged += (_, _) =>
        {
            if (DataContext is MainViewModel vm)
            {
                vm.PropertyChanged += Vm_PropertyChanged;
            }
        };
    }

    private void Vm_PropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        // Activate the line above the menu
        if (sender is MainViewModel vm && e.PropertyName == nameof(vm.NavMenuVisible))
        {
            BottomNavBorder.BorderThickness = vm.NavMenuVisible ? new Thickness(0, 1, 0, 0) : new Thickness(0);
        }

        // Selected Menu Item Changed
        if (sender is MainViewModel vmSelectedMenuItem && e.PropertyName == nameof(vm.SelectedMenuItem))
        {
            if (string.Equals(vmSelectedMenuItem.SelectedMenuItem?.IconName, "WellIcon"))
            {
            }
        }
    }

    /// <summary>
    /// Register handling of back button.
    /// </summary>
    protected override void OnLoaded(RoutedEventArgs e)
    {
        base.OnLoaded(e);
        if (TopLevel.GetTopLevel(this) is { } topLevel)
        {
            topLevel.BackRequested += MainView_BackRequested;
        }
    }

    /// <summary>
    /// Unregister handling of back button.
    /// </summary>
    protected override void OnUnloaded(RoutedEventArgs e)
    {
        if (TopLevel.GetTopLevel(this) is { } topLevel)
        {
            topLevel.BackRequested -= MainView_BackRequested;
        }

        base.OnUnloaded(e);
    }

    /// <summary>
    /// Handle back button.
    /// </summary>
    private void MainView_BackRequested(object? sender, RoutedEventArgs e)
    {
        if (DataContext is MainViewModel viewModel)
        {
            viewModel.NavigateBackCommand.Execute(null);
            e.Handled = true;
        }
    }
}