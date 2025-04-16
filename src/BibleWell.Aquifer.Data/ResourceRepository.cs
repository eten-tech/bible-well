namespace BibleWell.Aquifer.Data;

internal class ResourceRepository
{
    private bool _hasBeenInitialized  = false;
    private readonly SqliteDbManager _dbManager;
    // private readonly ILogger _logger;

    public ResourceRepository(SqliteDbManager dbManager)
    {
        // _logger = logger;
        _dbManager = dbManager;
        _ = InitAsync();
    }

    private async Task InitAsync()
    {
        if (_hasBeenInitialized)
        {
            return;
        }

        await using var connection = _dbManager.CreateConnection();
        connection.Open();

        try
        {
            await using var command = connection.CreateCommand();
            command.CommandText = @"
                CREATE TABLE IF NOT EXISTS Resources (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    Content TEXT NOT NULL
                );";
           await command.ExecuteNonQueryAsync();
        }
        catch (Exception e)
        {
            // todo: log and/or throw
            Console.WriteLine(e);
            throw new NotImplementedException(e.Message);
        }
        
        _hasBeenInitialized = true;
    }
    
    public async Task<Resource?> GetByIdAsync(int id)
    {
        await using var connection = _dbManager.CreateConnection();
        await using var command = connection.CreateCommand();
        command.CommandText = "SELECT Id, Content FROM Resources WHERE Id = @Id;";
        command.Parameters.AddWithValue("@Id", id);

        await using var reader = await command.ExecuteReaderAsync();
        if (!await reader.ReadAsync())
        {
            return null;
        }

        var resourceId = reader.GetInt32(0);
        var content = reader.GetString(1);

        return new Resource(resourceId, content);
    }
    
    public async Task<int> SaveAsync(Resource resource)
    {
        ArgumentNullException.ThrowIfNull(resource);

        if (resource.Id <= 0)
        {
            return await InsertAsync(resource);
        }
        
        return await UpdateAsync(resource);
    }

    private async Task<int> InsertAsync(Resource resource) 
    {
        await using var connection = _dbManager.CreateConnection();
        await using var command = connection.CreateCommand();

        command.CommandText = "INSERT INTO Resources (Id, Content) VALUES (@Id, @Content);";
        command.Parameters.AddWithValue("@Content", resource.Content);
        // todo: error handling
        return await command.ExecuteNonQueryAsync();
    }

    private async Task<int> UpdateAsync(Resource resource)
    {
        await using var connection = _dbManager.CreateConnection();
        await using var command = connection.CreateCommand();

        command.CommandText = "UPDATE Resources SET Content = @Content WHERE Id = @Id;";
        command.Parameters.AddWithValue("@Id", resource.Id);
        command.Parameters.AddWithValue("@Content", resource.Content);
        // todo: error handling 
        return await command.ExecuteNonQueryAsync();
    }

    
    public async Task<IEnumerable<Resource>> GetAllAsync()
    {
        var resources = new List<Resource>();

        await using var connection = _dbManager.CreateConnection();
        await using var command = connection.CreateCommand();
        command.CommandText = "SELECT Id, Content FROM Resources;";

        await using var reader = await command.ExecuteReaderAsync();
        while (await reader.ReadAsync())
        {
            var id = reader.GetInt32(0);
            var content = reader.GetString(1);
            resources.Add(new Resource(id, content));
        }

        return resources;
    }
}