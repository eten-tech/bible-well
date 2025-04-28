using BibleWell.Devices;
using Microsoft.Maui.Devices;

namespace BibleWell.Platform.Maui;

public sealed class MauiDeviceService : IDeviceService
{
    public FormFactor FormFactor { get; } = MapToFormFactor(DeviceInfo.Current.Idiom);
    public bool IsEmulated { get; } = DeviceInfo.Current.DeviceType == DeviceType.Virtual;
    public string Manufacturer { get; } = DeviceInfo.Current.Manufacturer;
    public string Model { get; } = DeviceInfo.Current.Model;
    public Devices.Platform Platform { get; } = MapToPlatform(DeviceInfo.Current.Platform);
    public string PlatformVersion { get; } = DeviceInfo.Current.VersionString;

    private static FormFactor MapToFormFactor(DeviceIdiom source)
    {
        if (source == DeviceIdiom.Phone)
        {
            return FormFactor.Phone;
        }

        if (source == DeviceIdiom.Tablet)
        {
            return FormFactor.Tablet;
        }

        if (source == DeviceIdiom.Desktop)
        {
            return FormFactor.Desktop;
        }

        return FormFactor.Other;
    }

    private static Devices.Platform MapToPlatform(DevicePlatform source)
    {
        if (source == DevicePlatform.Android)
        {
            return Devices.Platform.Android;
        }

        if (source == DevicePlatform.iOS)
        {
            return Devices.Platform.iOS;
        }

        if (source == DevicePlatform.MacCatalyst || source == DevicePlatform.macOS)
        {
            return Devices.Platform.Mac;
        }

        if (source == DevicePlatform.WinUI)
        {
            return Devices.Platform.Windows;
        }

        return Devices.Platform.Other;
    }
}