using BibleWell.Platform.Maui;

namespace BibleWell.App.iOS.Platform;

#pragma warning disable IDE1006 // Naming Styles: allow "iOS" prefix
public sealed class iOSDeviceService : MauiDeviceService
#pragma warning restore IDE1006 // Naming Styles
{
    public override string DeviceId { get; } = UIDevice.CurrentDevice.IdentifierForVendor?.ToString() ?? "";
}