namespace BibleWell.Storage;

public interface IStorageService
{
    string AppDataDirectory
    {
        get;
    }

    string ApplicationDirectoryPath
    {
        get;
    }
}