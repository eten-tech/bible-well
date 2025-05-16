using BibleWell.App.Desktop.Platform;
using BibleWell.Devices;
using BibleWell.Preferences;
using BibleWell.PushNotifications;
using BibleWell.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BibleWell.App.Desktop;

public class DesktopApp : App
{
    protected override void ConfigurePlatform(ConfigurationBuilder configurationBuilder, string environment)
    {
    }

    protected override void RegisterPlatformServices(IServiceCollection services)
    {
        services.AddSingleton<IApplicationInfoService, DesktopApplicationInfoService>();
        services.AddSingleton<IDeviceService, DesktopDeviceService>();
        services.AddSingleton<IStorageService, DesktopStorageService>();
        services.AddSingleton<IUserPreferencesService, DesktopUserPreferencesServices>();
        services.AddSingleton<ISecureUserStorageService, DesktopSecureUserStorageService>();
        services.AddSingleton<DesktopNotificationDeviceInstallationService, DesktopNotificationDeviceInstallationService>();
        // Use the mock implementation for Desktop to avoid NotImplementedException
        services.AddSingleton<INotificationRegistrationService, MockNotificationRegistrationService>();
    }
}