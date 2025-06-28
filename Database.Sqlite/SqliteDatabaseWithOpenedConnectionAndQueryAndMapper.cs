using Microsoft.Data.Sqlite;

namespace LightFunctional.Database.Sqlite;

public class SqliteDatabaseWithOpenedConnectionAndQueryAndMapper<T>(
    ISqliteDatabaseWithOpenedConnection sqliteDatabaseWithOpenedConnection,
    Func<SqliteDataReader, T> mapper,
    SqliteConnection connection) : ISqliteDatabaseWithOpenedConnectionAndQueryAndMapper<T>
{
    
    
    public async Task<Result<T[]>> ToArray()
    {
        if (connection.State != System.Data.ConnectionState.Open)
        {
            await connection.OpenAsync();
        }

        if (sqliteDatabaseWithOpenedConnection is not SqliteDatabaseWithOpenedConnection dbWithOpenedConnection)
            throw new InvalidOperationException("Invalid database connection type.");
        
        if (dbWithOpenedConnection.InternalLogBefore != null)
        {
            Console.WriteLine(dbWithOpenedConnection.InternalLogBefore());
        }

        await using var command = connection.CreateCommand();
            
        if(dbWithOpenedConnection.InternalQuery == null)
        {
            throw new InvalidOperationException("Query cannot be null.");
        }

        var sql = SqlFormatter.FormatSql(dbWithOpenedConnection.InternalQuery);
            
        command.CommandText = sql.format;
        foreach (var param in sql.parameters)
        {
            command.Parameters.AddWithValue(param.Key, param.Value);
        }
            
        await using var reader = await command.ExecuteReaderAsync();
            
        var items = new List<T>();
            
        while (await reader.ReadAsync())
        {
            var item = mapper(reader);
            if (item is null)
            {
                throw new InvalidOperationException("Mapper function returned null.");
            }

            items.Add(item);
        }
            
        if (dbWithOpenedConnection.InternalLogAfter != null)
        {
            Console.WriteLine(dbWithOpenedConnection.InternalLogAfter());
        }
            
        return Result<T[]>.Ok(items.ToArray());
    }

    public async Task<Result<List<T>>> ToList()
    {
        if (connection.State != System.Data.ConnectionState.Open)
        {
            await connection.OpenAsync();
        }

        if (sqliteDatabaseWithOpenedConnection is not SqliteDatabaseWithOpenedConnection dbWithOpenedConnection)
            throw new InvalidOperationException("Invalid database connection type.");
        
        if (dbWithOpenedConnection.InternalLogBefore != null)
        {
            Console.WriteLine(dbWithOpenedConnection.InternalLogBefore());
        }

        await using var command = connection.CreateCommand();
            
        if(dbWithOpenedConnection.InternalQuery == null)
        {
            throw new InvalidOperationException("Query cannot be null.");
        }

        var sql = SqlFormatter.FormatSql(dbWithOpenedConnection.InternalQuery);
            
        command.CommandText = sql.format;
        foreach (var param in sql.parameters)
        {
            command.Parameters.AddWithValue(param.Key, param.Value);
        }
            
        await using var reader = await command.ExecuteReaderAsync();
            
        var items = new List<T>();
            
        while (await reader.ReadAsync())
        {
            var item = mapper(reader);
            if (item is null)
            {
                throw new InvalidOperationException("Mapper function returned null.");
            }

            items.Add(item);
        }
            
        if (dbWithOpenedConnection.InternalLogAfter != null)
        {
            Console.WriteLine(dbWithOpenedConnection.InternalLogAfter());
        }
            
        return Result<List<T>>.Ok(items);
    }
    
    public async Task<Result<T>> Single()
    {
        if (connection.State != System.Data.ConnectionState.Open)
        {
            await connection.OpenAsync();
        }

        if (sqliteDatabaseWithOpenedConnection is not SqliteDatabaseWithOpenedConnection dbWithOpenedConnection)
            throw new InvalidOperationException("Invalid database connection type.");
        
        if (dbWithOpenedConnection.InternalLogBefore != null)
        {
            Console.WriteLine(dbWithOpenedConnection.InternalLogBefore());
        }

        await using var command = connection.CreateCommand();
            
        if(dbWithOpenedConnection.InternalQuery == null)
        {
            throw new InvalidOperationException("Query cannot be null.");
        }

        var sql = SqlFormatter.FormatSql(dbWithOpenedConnection.InternalQuery);
            
        command.CommandText = sql.format;
        foreach (var param in sql.parameters)
        {
            command.Parameters.AddWithValue(param.Key, param.Value);
        }
            
        await using var reader = await command.ExecuteReaderAsync();


        await reader.ReadAsync();
        
        var item = mapper(reader);
            
        if (dbWithOpenedConnection.InternalLogAfter != null)
        {
            Console.WriteLine(dbWithOpenedConnection.InternalLogAfter());
        }
            
        return Result<T>.Ok(item);
    }
    
    public async Task<Result<T>> FirstOrDefault()
    {
        if (connection.State != System.Data.ConnectionState.Open)
        {
            await connection.OpenAsync();
        }

        if (sqliteDatabaseWithOpenedConnection is not SqliteDatabaseWithOpenedConnection dbWithOpenedConnection)
            throw new InvalidOperationException("Invalid database connection type.");
        
        if (dbWithOpenedConnection.InternalLogBefore != null)
        {
            Console.WriteLine(dbWithOpenedConnection.InternalLogBefore());
        }

        await using var command = connection.CreateCommand();
            
        if(dbWithOpenedConnection.InternalQuery == null)
        {
            throw new InvalidOperationException("Query cannot be null.");
        }

        var sql = SqlFormatter.FormatSql(dbWithOpenedConnection.InternalQuery);
            
        command.CommandText = sql.format;
        foreach (var param in sql.parameters)
        {
            command.Parameters.AddWithValue(param.Key, param.Value);
        }
            
        await using var reader = await command.ExecuteReaderAsync();
        await reader.ReadAsync();
        var item = mapper(reader);
        
        if (dbWithOpenedConnection.InternalLogAfter != null)
        {
            Console.WriteLine(dbWithOpenedConnection.InternalLogAfter());
        }
            
        return Result<T>.Ok(item);
    }
    
    public SqliteDatabaseWithOpenedConnectionAndQueryAndMapper<T> LogAfter(string queryCompleted)
    {
        return this;
    }
}