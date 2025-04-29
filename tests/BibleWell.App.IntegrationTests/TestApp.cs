using System.Reflection;
using BibleWell.App.Desktop;
using CommunityToolkit.Mvvm.DependencyInjection;

namespace BibleWell.App.IntegrationTests;

public sealed class TestApp : DesktopApp
{
    /// <summary>
    /// The CommunityToolkit.Mvvm.DependencyInjection DI service provider does not allow configuring services twice. The Avalonia
    /// headless testing framework sets up the headless application for every test, resulting in an attempt to configure services for
    /// every test run.  Because the static Ioc.Default instance is used, we need to reset that static value's services.
    /// It doesn't have a setter to do this, so we need to use reflection to set the value.
    ///
    /// Corollary: all headless tests need to be run sequentially because the static Ioc.Default instance and Application.Current
    /// instances are used by the tests (and UI under test).
    /// See <see cref="TestAppBuilder" /> for the xUnit CollectionBehavior attribute that forces sequential test execution.
    /// </summary>
    /// <exception cref="InvalidOperationException"></exception>
    public override void OnFrameworkInitializationCompleted()
    {
        // Get the serviceProvider private field.
        var serviceProviderField = typeof(Ioc).GetField("serviceProvider", BindingFlags.NonPublic | BindingFlags.Instance)
            ?? throw new InvalidOperationException($"Could not retrieve the {nameof(Ioc)}.{nameof(Ioc.Default)}.serviceProvider private field.");

        // Set serviceProvider to null on the static Default IOC instance.
        // Note that the serviceProvider field is marked "volatile" but no other thread should be accessing it at this point.
        serviceProviderField.SetValue(Ioc.Default, null);

        // Now run the standard app initialization.
        base.OnFrameworkInitializationCompleted();
    }
}