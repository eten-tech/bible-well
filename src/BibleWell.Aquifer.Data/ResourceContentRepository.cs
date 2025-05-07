using BibleWell.Aquifer.Data.DbModels;
using Dapper;

namespace BibleWell.Aquifer.Data;

public class ResourceContentRepository
{
    private const string TableName = "ResourceContents";
    private readonly SqliteDbManager _dbManager;

    private bool _hasBeenInitialized;
    // private readonly ILogger _logger;

    public ResourceContentRepository(SqliteDbManager dbManager)
    {
        // _logger = logger;
        _dbManager = dbManager;
        Init();
    }

    private void Init()
    {
        if (_hasBeenInitialized)
        {
            return;
        }

        using var connection = _dbManager.CreateConnection();
        connection.Open();

        try
        {
            const string sql = $"""
                CREATE TABLE IF NOT EXISTS {TableName} (
                    Id INTEGER UNIQUE NOT NULL,
                    Name TEXT NOT NULL,
                    Content TEXT NOT NULL
                );
                """;

            connection.Execute(sql);

            Insert(new DbResourceContent(Id: 1, "Genesis test RC", "Genesis Test RC Content"));
        }
        catch (Exception e)
        {
            // todo: log and/or throw
            Console.WriteLine(e);
            throw new NotImplementedException(e.Message);
        }

        _hasBeenInitialized = true;
    }

    public DbResourceContent? GetById(int id)
    {
        using var connection = _dbManager.CreateConnection();
        const string sql = $"SELECT Id, Name, Content FROM {TableName} WHERE Id = @Id;";
        var resourceContent = connection.QuerySingleOrDefault<DbResourceContent>(sql, new { Id = id });

        return resourceContent;
    }

    public int Save(DbResourceContent resourceContent)
    {
        // might want some refactoring here with error handling or db transaction
        var existingResourceContent = GetById(checked((int)resourceContent.Id));
        return existingResourceContent != null ? Update(resourceContent) : Insert(resourceContent);
    }

    private int Insert(DbResourceContent resourceContent)
    {
        using var connection = _dbManager.CreateConnection();
        const string sql = $"INSERT INTO {TableName} (Id, Name, Content) VALUES (@Id, @Name, @Content) ON CONFLICT (Id) DO NOTHING;";

        // todo: error handling
        return connection.Execute(sql, resourceContent);
    }

    private int Update(DbResourceContent resourceContent)
    {
        using var connection = _dbManager.CreateConnection();
        const string sql = $"UPDATE {TableName} SET Content = @Content, Name = @Name WHERE Id = @Id;";

        // todo: error handling
        return connection.Execute(sql, resourceContent);
    }

    public IReadOnlyList<DbResourceContent> GetAll()
    {
        using var connection = _dbManager.CreateConnection();
        const string sql = $"SELECT Id, Name, Content FROM {TableName};";

        var resourceContents = connection.Query<DbResourceContent>(sql).AsList();

        return resourceContents;
    }
}