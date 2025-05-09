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

    /// <summary>
    /// Gets all cultures that directly have a resx file (e.g. "en-US", "en-GB", etc.).  If they don't directly have a resx file then
    /// also fall back to the parent culture if it has a resx file (e.g. "en" for both "en-US" and "en-GB", etc.).
    /// </summary>
    private static IReadOnlyList<CultureInfo> GetSupportedCultures()
    {
        var rm = new ResourceManager(typeof(AppResources));

        return CultureInfo.GetCultures(CultureTypes.AllCultures)
            .Where(cultureInfo => !cultureInfo.Equals(CultureInfo.InvariantCulture))
            .SelectMany<CultureInfo, CultureInfo>(cultureInfo =>
            {
                // We want to check both the culture and the parent culture to see if there are resources.
                // e.g. "en-US", "en-GB", and "en" could all have unique resource files.
                try
                {
                    var parentCultureInfo = new CultureInfo(cultureInfo.TwoLetterISOLanguageName);
                    return [cultureInfo, parentCultureInfo];
                }
                catch (CultureNotFoundException)
                {
                    // there is no supported parent for this culture on this OS
                    return [cultureInfo];
                }
            })
            .DistinctBy(ci => ci.Name)
            .Where(cultureInfo => rm.GetResourceSet(cultureInfo, createIfNotExists: true, tryParents: false) != null)
            .OrderBy(ci => ci.Name)
            .ToList();
    }
}