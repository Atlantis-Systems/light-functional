namespace LightFunctional.Database.Sqlite;

public interface ISqliteDatabase
{
    ISqliteDatabaseWithOpenedConnection OpenConnection();
}