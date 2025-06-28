using Microsoft.Data.Sqlite;

namespace LightFunctional.Database.Sqlite;

public class SqliteDatabaseWithOpenedConnection(string connectionString) : ISqliteDatabaseWithOpenedConnection
{
    private readonly SqliteConnection _connection = new(connectionString);

    public ISqliteDatabaseWithOpenedConnectionAndQuery Sql(FormattableString sql)
    {
        InternalQuery = sql;
        return new SqliteDatabaseWithOpenedConnectionAndQuery(this, _connection);
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