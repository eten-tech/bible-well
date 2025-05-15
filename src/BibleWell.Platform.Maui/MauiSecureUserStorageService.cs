using BibleWell.Storage;
using Microsoft.Maui.Storage;

namespace BibleWell.Platform.Maui;

/// <summary>
/// See https://learn.microsoft.com/en-us/dotnet/maui/platform-integration/storage/secure-storage?view=net-maui-9.0.
/// </summary>
public class MauiSecureUserStorageService : ISecureUserStorageService
{
    public Task SetAsync(string key, string value)
    {
        return SecureStorage.SetAsync(key, value);
    }

    public Task<string?> GetAsync(string key)
    {
        return SecureStorage.GetAsync(key);
    }

    public bool Remove(string key)
    {
        return SecureStorage.Remove(key);
    }

    public void Clear()
    {
        SecureStorage.RemoveAll();
    }
}