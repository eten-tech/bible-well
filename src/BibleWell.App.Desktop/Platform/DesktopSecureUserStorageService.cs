using BibleWell.Storage;

namespace BibleWell.App.Desktop.Platform;

/// <summary>
/// A fake implementation of <see cref="ISecureUserStorageService" /> that does not store any values.
/// </summary>
public class DesktopSecureUserStorageService : ISecureUserStorageService
{
    public Task SetAsync(string key, string value)
    {
        return Task.CompletedTask;
    }

    public Task<string?> GetAsync(string key)
    {
        return Task.FromResult<string?>(null);
    }

    public bool Remove(string key)
    {
        return true;
    }

    public void Clear()
    {
    }
}