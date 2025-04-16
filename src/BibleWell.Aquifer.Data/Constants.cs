namespace BibleWell.Aquifer.Data;

public static class Constants
{
    private const string DatabaseFilename = "BibleWellAquifer.db3";

    // Flags are not used with Microsoft.Data.Sqlite.Core 
    // public const SQLite.SQLiteOpenFlags Flags =
    //     SQLite.SQLiteOpenFlags.ReadWrite |
    //     SQLite.SQLiteOpenFlags.Create |
    //     SQLite.SQLiteOpenFlags.SharedCache;

    public static string DatabasePath =>
        Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            DatabaseFilename);

    public static string ConnectionString => $"Data Source={DatabasePath}";
}