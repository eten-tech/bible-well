using BibleWell.Storage;
using Dapper;
using Microsoft.Data.Sqlite;

namespace BibleWell.Aquifer.Data;

internal class SqliteDbManager
{
    private readonly IStorageService _storageService;

    public SqliteDbManager(IStorageService storageService)
    {
        _storageService = storageService;
        InitializeDatabase();
    }

    public SqliteConnection CreateConnection()
    {
        var connection = new SqliteConnection(_storageService.ConnectionString);
        connection.Open();
        return connection;
    }

    private void InitializeDatabase()
    {
        // Ensure the directory exists
        var directory = Path.GetDirectoryName(_storageService.DatabasePath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // ensure DB is in WAL mode
        using var connection = new SqliteConnection(_storageService.ConnectionString);
        const string sql = "PRAGMA journal_mode = WAL;";
        connection.Execute(sql);
        
        // ensure all tables have been created using repositories?
        
        
        // add other tables here or swap this entire section for migrations
    }
}