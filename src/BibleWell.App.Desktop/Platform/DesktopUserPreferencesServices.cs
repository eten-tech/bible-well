using BibleWell.Preferences;

namespace BibleWell.App.Desktop.Platform;

/// <summary>
/// A fake implementation of <see cref="IUserPreferencesService" /> that does not store any user preferences.
/// </summary>
public sealed class DesktopUserPreferencesServices : IUserPreferencesService
{
    public bool ContainsKey(string key)
    {
        return false;
    }

    public void Remove(string key)
    {
    }

    public void Clear()
    {
    }

    public void Set<T>(string key, T value)
    {
    }

    public T Get<T>(string key, T defaultValue)
    {
        return defaultValue;
    }
}