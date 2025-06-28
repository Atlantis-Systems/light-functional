using Microsoft.Data.Sqlite;

namespace LightFunctional.Database.Sqlite;

public interface ISqliteDatabaseWithOpenedConnectionAndQuery
{
    ISqliteDatabaseWithOpenedConnectionAndQueryAndMapper<T> Map<T>(Func<SqliteDataReader, T> func);
    Task<Result<bool>> Execute();
}