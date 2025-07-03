using Microsoft.Data.Sqlite;

namespace LightFunctional.Database.Sqlite;

public class SqliteDatabase(SqliteConnection sqliteConnection) : ISqliteDatabase
{
    public static ISqliteDatabase CreateFile(string fileName)
    {
        var connectionString = $"Data Source={fileName}";
        var connection = new SqliteConnection(connectionString);
        return new SqliteDatabase(connection);
    }
    
    public static ISqliteDatabase CreateInMemory()
    {
        var connectionString = "Data Source=:memory:";
        var connection = new SqliteConnection(connectionString);
        return new SqliteDatabase(connection);
    }

    public static ISqliteDatabase CreateSharedInMemory()
    {
        var connectionString = "Data Source=InMemoryDatabase;Mode=Memory;Cache=Shared";
        var connection = new SqliteConnection(connectionString);
        return new SqliteDatabase(connection);
    }

    public ISqliteDatabaseWithOpenedConnection OpenConnection()
    {
        return new SqliteDatabaseWithOpenedConnection(sqliteConnection);
    }
}
