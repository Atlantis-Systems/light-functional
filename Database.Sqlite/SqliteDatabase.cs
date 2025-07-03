namespace LightFunctional.Database.Sqlite;

public class SqliteDatabase : ISqliteDatabase
{
    private readonly string _connectionString;

    private SqliteDatabase(string connectionString)
    {
        _connectionString = connectionString;
    }
    
    public static ISqliteDatabase CreateFile(string fileName)
    {
        return new SqliteDatabase(
            $"Data Source={fileName};Version=3;Pooling=True;Cache Size=10000;Journal Mode=WAL;Synchronous=Normal;");
    }
    
    public static ISqliteDatabase CreateInMemory()
    {
        return new SqliteDatabase("Data Source=:memory:");
    }

     public static ISqliteDatabase CreateSharedInMemory()
    {
        return new SqliteDatabase("Data Source=InMemoryDatabase;Mode=Memory;Cache=Shared");
    }

    public ISqliteDatabaseWithOpenedConnection OpenConnection()
    {
        return new SqliteDatabaseWithOpenedConnection(_connectionString);
    }

    public static ISqliteDatabase FromConnectionString(string connectionString)
    {
        return new SqliteDatabase(connectionString);
    }
}
