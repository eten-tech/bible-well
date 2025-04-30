using BibleWell.Devices;
using CommunityToolkit.Mvvm.DependencyInjection;
using Microsoft.ApplicationInsights.Channel;
using Microsoft.ApplicationInsights.Extensibility;

namespace BibleWell.App.Telemetry;

public sealed class AppTelemetryInitializer : ITelemetryInitializer
{
    public AppTelemetryInitializer()
    {
        // Manual DI lookup is required for this type because the initializer cannot have constructor arguments
        // when doing basic/manual app insights setup (required for mobile apps).
        var applicationInfoService = Ioc.Default.GetRequiredService<IApplicationInfoService>();
        var deviceService = Ioc.Default.GetRequiredService<IDeviceService>();

        AppVersion = applicationInfoService.Version;
        DeviceId = deviceService.DeviceId;
        DeviceOperatingSystem = $"{deviceService.Platform}_{deviceService.PlatformVersion}";
        DeviceManufacturer = deviceService.Manufacturer;
        DeviceModel = deviceService.Model;
        DeviceFormFactor = deviceService.FormFactor.ToString();
        SessionId = Guid.NewGuid().ToString();
        Source = "BibleWell";
    }

    public string AppVersion { get; }

    public string DeviceId { get; }
    public string DeviceOperatingSystem { get; }
    public string DeviceManufacturer { get; }
    public string DeviceModel { get; }
    public string DeviceFormFactor { get; }

    public string SessionId { get; }

    public string Source { get; }

    public void Initialize(ITelemetry telemetry)
    {
        telemetry.Context.Session.Id = SessionId;
        telemetry.Context.Device.Id = DeviceId;
        telemetry.Context.Device.OperatingSystem = DeviceOperatingSystem;
        telemetry.Context.Device.OemName = DeviceManufacturer;
        telemetry.Context.Device.Model = DeviceModel;
        telemetry.Context.Device.Type = DeviceFormFactor;
        telemetry.Context.Component.Version = AppVersion;

        telemetry.Context.GlobalProperties["source"] = Source;
    }
}