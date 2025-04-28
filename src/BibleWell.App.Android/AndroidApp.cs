using BibleWell.App.Android.Platform;
using BibleWell.Devices;
using BibleWell.Platform.Maui;
using BibleWell.Preferences;
using BibleWell.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BibleWell.App.Android;

public sealed class AndroidApp : App
{
    protected override void ConfigurePlatform(ConfigurationBuilder configurationBuilder)
    {
    }

    protected override void RegisterPlatformServices(IServiceCollection services)
    {
        services.AddSingleton<IApplicationInfoService, MauiApplicationInfoService>();
        services.AddSingleton<IDeviceService, AndroidDeviceService>();
        services.AddSingleton<IStorageService, MauiStorageService>();
        services.AddSingleton<IUserPreferencesService, MauiUserPreferencesService>();
    }
}