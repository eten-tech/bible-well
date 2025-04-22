namespace BibleWell.Aquifer.Data;

public static class Constants
{
    private const string DatabaseFilename = "BibleWellAquifer.db3";
    private const string ApplicationDir = "BibleWell";

    // public static string FsAppDir =>
    //     Path.Combine(
    //         FileSystem.AppDataDirectory,
    //         ApplicationDir);

    private static string ApplicationPath =>
        Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
            ApplicationDir);

    public static string DatabasePath => Path.Combine(ApplicationPath, DatabaseFilename);

    public static string ConnectionString => $"Data Source={DatabasePath}";
}