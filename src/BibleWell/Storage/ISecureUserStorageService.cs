namespace BibleWell.Storage;

public interface ISecureUserStorageService
{
    /// <summary>
    /// Sets a value for a given key.
    /// </summary>
    /// <param name="key"></param>
    /// <param name="value"></param>
    Task SetAsync(string key, string value);

    /// <summary>
    /// Gets the value for a given key.
    /// </summary>
    /// <param name="key"></param>
    Task<string?> GetAsync(string key);

    /// <summary>
    /// Removes the value for a given key.
    /// </summary>
    /// <param name="key"></param>
    bool Remove(string key);

    /// <summary>
    /// Clears all values.
    /// </summary>
    void Clear();
}