using Dapper;
using Microsoft.Data.Sqlite;

namespace BibleWell.Aquifer.Data;

internal class SqliteDbManager
{
    private readonly string _connectionString;

    public SqliteDbManager(string connectionString)
    {
        _connectionString = connectionString;
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
        // ? Ensure the directory exists
        var directory = Path.GetDirectoryName(Constants.DatabasePath);
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