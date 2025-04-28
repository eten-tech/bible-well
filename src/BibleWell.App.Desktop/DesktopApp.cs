using BibleWell.App.Desktop.Platform;
using BibleWell.Devices;
using BibleWell.Preferences;
using BibleWell.Storage;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BibleWell.App.Desktop;

public partial class DesktopApp : App
{
    protected override void ConfigurePlatform(ConfigurationBuilder configurationBuilder)
    {
    }

    protected override void RegisterPlatformServices(IServiceCollection services)
    {
        services.AddSingleton<IDeviceService, DesktopDeviceService>();
        services.AddSingleton<IStorageService, DesktopStorageService>();
        services.AddSingleton<IUserPreferencesService, DesktopUserPreferencesServices>();
    }
}