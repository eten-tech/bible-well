using BibleWell.Devices;

namespace BibleWell.App.Desktop.Platform;

public sealed class DesktopDeviceService : IDeviceService
{
    public FormFactor FormFactor { get; } = FormFactor.Desktop;
    public bool IsEmulated { get; } = false;
    public string Manufacturer { get; } = "";
    public string Model { get; } = "";
    public Devices.Platform Platform { get; } = OperatingSystem.IsWindows()
        ? Devices.Platform.Windows
        : OperatingSystem.IsMacOS()
            ? Devices.Platform.Mac
            : OperatingSystem.IsLinux()
                ? Devices.Platform.Linux
                : Devices.Platform.Other;
    public string PlatformVersion { get; } = Environment.OSVersion.Version.ToString();
}
