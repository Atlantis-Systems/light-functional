namespace LightFunctional.Database.Sqlite;

public interface ISqliteDatabaseWithOpenedConnection
{
    ISqliteDatabaseWithOpenedConnectionAndQuery Sql(FormattableString sql);
    ISqliteDatabaseWithOpenedConnection LogBefore(string message);
    ISqliteDatabaseWithOpenedConnection LogAfter(string message);
    ISqliteDatabaseWithOpenedConnection LogError(string errorMessage);
}