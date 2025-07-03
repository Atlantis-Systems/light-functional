using Microsoft.Data.Sqlite;

namespace LightFunctional.Database.Sqlite;

public class SqliteDatabaseWithOpenedConnection(SqliteConnection connection) : ISqliteDatabaseWithOpenedConnection
{

    public ISqliteDatabaseWithOpenedConnectionAndQuery Sql(FormattableString sql)
    {
        InternalQuery = sql;
        return new SqliteDatabaseWithOpenedConnectionAndQuery(this, connection);
    }

    
    public ISqliteDatabaseWithOpenedConnection LogBefore(string message)
    {
        InternalLogBefore = () => message;
        return this;
    }

    public ISqliteDatabaseWithOpenedConnection LogAfter(string message)
    {
        InternalLogAfter = () => message;
        return this;
    }


    public ISqliteDatabaseWithOpenedConnection LogError(string errorMessage)
    {
        InternalLogError = () => errorMessage;
        return this;
    }
    
    protected internal FormattableString? InternalQuery { get; private set; }

    protected internal Func<string>? InternalLogBefore { get; private set; } = null;

    protected internal Func<string>? InternalLogAfter { get; private set; } = null;

    protected internal Func<string>? InternalLogError { get; private set; } = null;
}