using BibleWell.Platform.Maui;
using MonoAndroidApplication = Android.App.Application;
using MonoAndroidSettings = Android.Provider.Settings;

namespace BibleWell.App.Android.Platform;

public sealed class AndroidDeviceService : MauiDeviceService
{
    public override string DeviceId { get; } =
        MonoAndroidSettings.Secure.GetString(MonoAndroidApplication.Context.ContentResolver, MonoAndroidSettings.Secure.AndroidId) ?? "";
}