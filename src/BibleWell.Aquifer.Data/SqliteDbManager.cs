using BibleWell.Storage;
using Dapper;
using Microsoft.Data.Sqlite;

namespace BibleWell.Aquifer.Data;

public class SqliteDbManager
{
    private readonly IStorageService _storageService;
    private readonly string _connectionString;
    private readonly string _databasePath;

    public SqliteDbManager(IStorageService storageService)
    {
        _storageService = storageService;
        _databasePath = Path.Combine(_storageService.ApplicationDirectoryPath, Constants.AquiferDatabaseFilename);
        _connectionString = $"Data Source={_databasePath}";
        InitializeDatabase();
    }

    public SqliteConnection CreateConnection()
    {
        var connection = new SqliteConnection(_connectionString);
        connection.Open();
        return connection;
    }

    private void InitializeDatabase()
    {
        // Ensure the directory exists
        var directory = Path.GetDirectoryName(_databasePath);
        if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
        {
            Directory.CreateDirectory(directory);
        }

        // ensure DB is in WAL mode
        using var connection = new SqliteConnection(_connectionString);
        const string sql = "PRAGMA journal_mode = WAL;";
        connection.Execute(sql);
        
        // ensure all tables have been created using repositories?
        
        
        // add other tables here or swap this entire section for migrations
    }
}