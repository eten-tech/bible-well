using Avalonia.Controls;
using Avalonia.Interactivity;
using BibleWell.App.ViewModels;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace BibleWell.App.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
    }

    /// <summary>
    /// Wire handling of back button.
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
    /// Unwire handling of back button.
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
    private static void MainView_BackRequested(object? sender, RoutedEventArgs e)
    {
        var router = Ioc.Default.GetRequiredService<Router<ViewModelBase>>();
        if (router.CanGoBack)
        {
            router.Back();
            e.Handled = true;
        }
    }
}