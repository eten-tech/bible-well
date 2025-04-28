namespace BibleWell.Devices;

public interface IDeviceService
{
    FormFactor FormFactor { get; }
    bool IsEmulated { get; }
    string Manufacturer { get; }
    string Model { get; }
    Platform Platform { get; }
    string PlatformVersion { get; }
}