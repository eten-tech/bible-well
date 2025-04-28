using System.Reflection;

namespace BibleWell.App.Desktop.Platform;

public sealed class DesktopApplicationInfoService : IApplicationInfoService
{
    public string BuildNumber { get; } = "";
    public string Version { get; } = Assembly.GetExecutingAssembly().GetName().Version?.ToString()
        ?? throw new InvalidOperationException("Unable to fetch assembly version.");
}