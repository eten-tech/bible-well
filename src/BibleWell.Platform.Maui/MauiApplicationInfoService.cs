using Microsoft.Maui.ApplicationModel;

namespace BibleWell.Platform.Maui;

public sealed class MauiApplicationInfoService : IApplicationInfoService
{
    public string BuildNumber { get; } = AppInfo.Current.BuildString;
    public string Version { get; } = AppInfo.Current.VersionString;
}