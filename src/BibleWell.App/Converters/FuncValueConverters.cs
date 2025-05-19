using Avalonia;
using Avalonia.Controls;
using Avalonia.Data.Converters;
using Avalonia.Media;

namespace BibleWell.App.Converters;

/// <summary>
/// A static class holding FuncValueConverters
/// </summary>
/// <remarks>
/// Consume converts from XAML via <code>{x:Static conv:FuncValueConverters.MyConverter}</code>
/// </remarks>
public static class FuncValueConverters
{
    /// <summary>
    /// Gets a Converter that returns a parsed PathIcon for a given icon name input. Returns null if the input was not parsed successfully.
    /// </summary>
    public static FuncValueConverter<string?, StreamGeometry?> StringToIconConverter { get; } =
        new(
            iconName =>
            {
                if (iconName != null &&
                    Application.Current?.TryGetResource(iconName, out var resource) == true &&
                    resource is StreamGeometry icon)
                {
                    return icon;
                }

                return null;
            });

    /// <summary>
    /// Gets a Converter that returns a parsed  for a given icon name input. Returns null if the input was not parsed successfully.
    /// </summary>
    public static FuncValueConverter<string?, DrawingBrush?> StringToDrawingBrushConverter { get; } =
        new(
            iconName =>
            {
                if (iconName != null &&
                    Application.Current?.TryGetResource(iconName, out var resource) == true &&
                    resource is DrawingBrush icon)
                {
                    return icon;
                }

                return null;
            });
}