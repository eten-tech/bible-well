using Avalonia;
using Avalonia.Headless;
using BibleWell.App.IntegrationTests;

// Tell Avalonia to use this class to configure the headless test application.
[assembly: AvaloniaTestApplication(typeof(TestAppBuilder))]

// Tell xUnit to run all tests in this assembly sequentially.
// This is necessary because the Avalonia headless test application uses a static Application.Current instance as well as other global
// config, and we also use the CommunityToolkit.Mvvm.DependencyInjection Ioc.Default static instance (see the TestApp for handling it).
// See also https://github.com/AvaloniaUI/Avalonia/discussions/18289.
[assembly: CollectionBehavior(CollectionBehavior.CollectionPerAssembly)]

namespace BibleWell.App.IntegrationTests;

public sealed class TestAppBuilder
{
    public static AppBuilder BuildAvaloniaApp()
    {
        // Test the Desktop app and turn on Skia drawing so we can take screenshots.
        return AppBuilder.Configure<TestApp>()
            .UseHeadless(new AvaloniaHeadlessPlatformOptions
            {
                UseHeadlessDrawing = false,
            })
            .UseSkia();
    }
}