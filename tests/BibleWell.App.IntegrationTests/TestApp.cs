using BibleWell.App.Desktop;

namespace BibleWell.App.IntegrationTests;

public sealed class TestApp : DesktopApp
{
    /// <summary>
    /// The Avalonia headless testing framework sets up the headless application for every test, resulting in an attempt to configure
    /// services for every test run. Therefore, we have to reset the Ioc.Default instance before running configuration.
    /// 
    /// Corollary: all headless tests need to be run sequentially because the static Ioc.Default instance and Application.Current
    /// instances are used by the tests (and UI under test).
    /// See <see cref="TestAppBuilder" /> for the xUnit CollectionBehavior attribute that forces sequential test execution.
    /// </summary>
    public override void OnFrameworkInitializationCompleted()
    {
        ResetIoc();

        base.OnFrameworkInitializationCompleted();
    }
}