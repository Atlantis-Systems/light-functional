namespace LightFunctional;

public static class Dispose
{
    public static Result<T> Using<T>(Func<(T resource, Action dispose)> factory)
    {
        try
        {
            var (resource, dispose) = factory();
            return Result<T>.Ok(resource).Tap(_ => dispose());
        }
        catch (Exception ex)
        {
            return Result<T>.Fail(ex);
        }
    }

    public static Result<Unit> Using(Action dispose, Action action)
    {
        return Try.Run(action).Tap(_ => dispose());
    }

    public static Result<T> Using<T>(Action dispose, Func<T> func)
    {
        return Try.Run(func).Tap(_ => dispose());
    }
}