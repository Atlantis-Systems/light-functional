namespace LightFunctional.Database.Sqlite;

public interface ISqliteDatabaseWithOpenedConnectionAndQueryAndMapper<T>
{
    Task<Result<T[]>> ToArray();
    Task<Result<List<T>>> ToList();
    Task<Result<T>> Single();
    Task<Result<T>> FirstOrDefault();
    SqliteDatabaseWithOpenedConnectionAndQueryAndMapper<T> LogAfter(string message);
}