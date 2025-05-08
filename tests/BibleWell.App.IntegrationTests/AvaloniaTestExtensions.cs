using System.Runtime.CompilerServices;
using Avalonia;
using Avalonia.Controls;
using Avalonia.Headless;
using Avalonia.Input;
using Avalonia.Threading;
using BibleWell.App.ViewModels;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace BibleWell.App.IntegrationTests;

public static class AvaloniaTestExtensions
{
    public static TView GetTestView<TViewModel, TView>(this Application application)
        where TViewModel : ViewModelBase
        where TView : UserControl
    {
        var viewModel = Ioc.Default.GetRequiredService<TViewModel>();

        var viewLocator = application.DataTemplates.SingleOrDefault(dt => dt is ViewLocator) as ViewLocator ??
            throw new InvalidOperationException("ViewLocator not found in DataTemplates.");

        var view = viewLocator.Build(viewModel);

        var window = new Window
        {
            Content = view,
            DataContext = viewModel,
            Width = 540,
            Height = 960,
        };

        window.Show();

        return view as TView ?? throw new InvalidOperationException($"Unable to build view for {typeof(TViewModel)}.");
    }

    public static void ExecuteUiTest(this Control control, Action executeTest, [CallerMemberName] string callerName = "")
    {
        try
        {
            executeTest();
        }
        catch
        {
            control.SaveTestImage(callerName: callerName);
            throw;
        }
    }

    public static async Task ExecuteUiTestAsync(
        this Control control,
        Func<Task> executeTestAsync,
        [CallerMemberName] string callerName = "")
    {
        try
        {
            await executeTestAsync();
        }
        catch
        {
            control.SaveTestImage(callerName: callerName);
            throw;
        }
    }

    public static void SaveTestImage(this Control control, string fileName = "", [CallerMemberName] string callerName = "")
    {
        var window = TopLevel.GetTopLevel(control) ??
            throw new InvalidOperationException("Control was not attached to window in headless Avalonia test.");

        SaveTestImage(window, fileName, callerName);
    }

    public static void SaveTestImage(this TopLevel topLevel, string fileName = "", [CallerMemberName] string callerName = "")
    {
        var frame = topLevel.CaptureRenderedFrame() ?? throw new InvalidOperationException("No frame is rendered!");

        // Get the following directory: "/tests/{projectDir}/TestResults/{callerName}/"
        var testOutputDirectory = Path.Combine(AppContext.BaseDirectory.Split("bin")[0], $"TestResults/{callerName}/");
        if (!Directory.Exists(testOutputDirectory))
        {
            Directory.CreateDirectory(testOutputDirectory);
        }

        var testImageFileName =
            $"{fileName}{(!string.IsNullOrEmpty(fileName) ? "_" : "")}{DateTime.UtcNow:yyyy'-'MM'-'dd'T'HH'_'mm'_'ss'Z'}.png";

        frame.Save(Path.Combine(testOutputDirectory, testImageFileName));
    }

    /// <summary>
    /// "Clicks" the control by focusing on it and hitting the space bar.
    /// </summary>
    /// <param name="control">The control to click.</param>
    public static void TestClick(this InputElement control)
    {
        if (control.Focus())
        {
            var topLevel = TopLevel.GetTopLevel(control) ??
                throw new InvalidOperationException("Control was not attached to window in headless Avalonia test.");

            topLevel.KeyReleaseQwerty(PhysicalKey.Space, RawInputModifiers.None);
        }
    }

    /// <summary>
    /// Waits for all UI events to complete, including animations and rendering.
    /// See https://docs.avaloniaui.net/docs/concepts/headless/#simulating-user-delay.
    /// Note: Transitions/animations may still not have completed after this method is called.
    /// </summary>
    public static void WaitForAllUiEventsToComplete()
    {
        Dispatcher.UIThread.RunJobs();
        AvaloniaHeadlessPlatform.ForceRenderTimerTick();
    }
}