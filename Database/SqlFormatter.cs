namespace LightFunctional.Database;

public static class SqlFormatter
{
    public static (string format, Dictionary<string, object?> parameters) FormatSql(FormattableString fs)
    {
        var format = fs.Format;
        var args = fs.GetArguments();

        var parameters = new Dictionary<string, object?>();

        for (var i = 0; i < args.Length; i++)
        {
            var paramName = $"@p{i}";
            format = format.Replace("{" + i + "}", paramName);
            parameters[paramName] = args[i];
        }

        return (format, parameters);
    }
}