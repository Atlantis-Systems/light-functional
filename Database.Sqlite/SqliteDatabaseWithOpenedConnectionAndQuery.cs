using Microsoft.Data.Sqlite;

namespace LightFunctional.Database.Sqlite;

public class SqliteDatabaseWithOpenedConnectionAndQuery(
    SqliteDatabaseWithOpenedConnection sqliteDatabaseWithOpenedConnection,
    SqliteConnection connection) : ISqliteDatabaseWithOpenedConnectionAndQuery
{
    public ISqliteDatabaseWithOpenedConnectionAndQueryAndMapper<T> Map<T>(Func<SqliteDataReader, T> mapper)
    {
        return new SqliteDatabaseWithOpenedConnectionAndQueryAndMapper<T>(sqliteDatabaseWithOpenedConnection, mapper, connection);
    }

    public async Task<Result<bool>> Execute()
    {
        if (connection.State != System.Data.ConnectionState.Open)
        {
            await connection.OpenAsync();
        }

        if (sqliteDatabaseWithOpenedConnection is null)
            throw new InvalidOperationException("Invalid database connection type.");
        
        if (sqliteDatabaseWithOpenedConnection.InternalLogBefore != null)
        {
            Console.WriteLine(sqliteDatabaseWithOpenedConnection.InternalLogBefore());
        }

        await using var command = connection.CreateCommand();
            
        if(sqliteDatabaseWithOpenedConnection.InternalQuery == null)
        {
            throw new InvalidOperationException("Query cannot be null.");
        }

        var sql = SqlFormatter.FormatSql(sqliteDatabaseWithOpenedConnection.InternalQuery);
            
        command.CommandText = sql.format;
        foreach (var param in sql.parameters)
        {
            command.Parameters.AddWithValue(param.Key, param.Value);
        }

        var numberOfAffectedRows = await command.ExecuteNonQueryAsync();
            
        if (sqliteDatabaseWithOpenedConnection.InternalLogAfter != null)
        {
            Console.WriteLine(sqliteDatabaseWithOpenedConnection.InternalLogAfter());
        }

        return Result.Ok(numberOfAffectedRows > 0);
    }
}