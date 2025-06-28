using Microsoft.Data.Sqlite;

namespace LightFunctional.Database.Sqlite;

public static class SqliteConnectionExtensions
{
    public static Result<SqliteConnection> TryOpen(this SqliteConnection conn) =>
        Try.Run(() =>
        {
            conn.Open();
            return conn;
        });
}