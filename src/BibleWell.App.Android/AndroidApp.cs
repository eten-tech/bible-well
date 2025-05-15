using System.Reflection;
using BibleWell.App.Android.Platform;
using BibleWell.Devices;
using BibleWell.Platform.Maui;
using BibleWell.Preferences;
using BibleWell.PushNotifications;
using BibleWell.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BibleWell.App.Android;

public sealed class AndroidApp : App
{
    protected override void ConfigurePlatform(ConfigurationBuilder configurationBuilder, string environment)
    {
        if (environment == nameof(AppEnvironment.Development))
        {
            // Ideally we'd dispose of this stream, but we can't because it's used by the configuration builder in the Build() call.
            // The GC will dispose of it eventually.
            var environmentConfigurationSettingsFileStream = GetAppSettingsFileStream(
                Assembly.GetExecutingAssembly(),
                $"appsettings.{environment}.json");
            configurationBuilder.AddJsonStream(environmentConfigurationSettingsFileStream);
        }
    }

    protected override void RegisterPlatformServices(IServiceCollection services)
    {
        services.AddSingleton<IApplicationInfoService, MauiApplicationInfoService>();
        services.AddSingleton<IDeviceService, AndroidDeviceService>();
        services.AddSingleton<IStorageService, MauiStorageService>();
        services.AddSingleton<IUserPreferencesService, MauiUserPreferencesService>();
        services.AddSingleton<ISecureUserStorageService, MauiSecureUserStorageService>();
        services.AddSingleton<IDeviceInstallationService, AndroidNotificationDeviceInstallationService>();
        services.AddSingleton<INotificationRegistrationService, NotificationRegistrationService>();
    }
}