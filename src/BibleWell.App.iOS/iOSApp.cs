using BibleWell.App.iOS.Platform;
using BibleWell.Devices;
using BibleWell.Platform.Maui;
using BibleWell.Preferences;
using BibleWell.PushNotifications;
using BibleWell.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BibleWell.App.iOS;

#pragma warning disable IDE1006 // Naming Styles: Allow "iOS" prefix
// ReSharper disable once InconsistentNaming
public sealed class iOSApp : App
#pragma warning restore IDE1006 // Naming Styles
{
    protected override void ConfigurePlatform(ConfigurationBuilder configurationBuilder, string environment)
    {
    }

    protected override void RegisterPlatformServices(IServiceCollection services)
    {
        services.AddSingleton<IApplicationInfoService, MauiApplicationInfoService>();
        services.AddSingleton<IDeviceService, iOSDeviceService>();
        services.AddSingleton<IStorageService, MauiStorageService>();
        services.AddSingleton<IUserPreferencesService, MauiUserPreferencesService>();
        services.AddSingleton<ISecureUserStorageService, MauiSecureUserStorageService>();
        services.AddSingleton<IDeviceInstallationService, IosNotificationDeviceInstallationService>();
        // Use the mock implementation for iOS to avoid NotImplementedException
        services.AddSingleton<INotificationRegistrationService, MockNotificationRegistrationService>();
    }
}