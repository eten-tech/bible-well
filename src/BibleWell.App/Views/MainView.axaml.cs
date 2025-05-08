using Avalonia.Controls;
using Avalonia.Interactivity;
using BibleWell.App.ViewModels;

namespace BibleWell.App.Views;

public partial class MainView : UserControl
{
    public MainView()
    {
        InitializeComponent();
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

#if ANDROID
    /// <summary>
    /// Request permissions for notifications
    /// </summary>
    protected override async void OnAppearing()
    {
        base.OnAppearing();

        PermissionStatus status = await Permissions.RequestAsync<Permissions.PostNotifications>();
    }
#endif
}