using System.Globalization;
using System.Resources;

namespace BibleWell.App.Resources;

public static class ResourceHelper
{
    /// <summary>
    /// Gets all supported cultures where we have localization files.
    /// This includes all cultures that are supported by the OS and that have a direct or parent resx file.
    /// </summary>
    public static IReadOnlyList<CultureInfo> SupportedCultures { get; } = GetSupportedCultures();

    public static bool IsSupportedCulture(CultureInfo cultureInfo)
    {
        return GetCultureInfoAndAllParents(cultureInfo)
            .Any(ci => SupportedCultures.Any(sci => sci.Name.Equals(ci.Name, StringComparison.OrdinalIgnoreCase)));
    }

    /// <summary>
    /// Gets all cultures that directly have a resx file (e.g. "en-US", "en-GB", etc.).  If they don't directly have a resx file then
    /// also fall back to the parent culture if it has a resx file (e.g. "en" for both "en-US" and "en-GB", etc.).
    /// </summary>
    private static IReadOnlyList<CultureInfo> GetSupportedCultures()
    {
        var rm = new ResourceManager(typeof(AppResources));

        return CultureInfo.GetCultures(CultureTypes.AllCultures)
            .Where(cultureInfo => !cultureInfo.Equals(CultureInfo.InvariantCulture))
            .SelectMany(GetCultureInfoAndAllParents)
            .DistinctBy(ci => ci.Name)
            .Where(cultureInfo => rm.GetResourceSet(cultureInfo, createIfNotExists: true, tryParents: false) != null)
            .OrderBy(ci => ci.Name)
            .ToList();
    }

    /// <summary>
    /// Tries to get the parent culture for the given culture info.
    /// Examples:
    /// * return "en-US" and "en" for "en-US".
    /// * return "zh-Hans-CN", "zh-Hans", and "zh" for "zh-Hans-CN".
    /// </summary>
    private static IEnumerable<CultureInfo> GetCultureInfoAndAllParents(CultureInfo cultureInfo)
    {
        yield return cultureInfo;
        while (!cultureInfo.Parent.Equals(CultureInfo.InvariantCulture))
        {
            cultureInfo = cultureInfo.Parent;
            yield return cultureInfo;
        }
    }
}