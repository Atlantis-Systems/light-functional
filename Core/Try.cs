namespace LightFunctional;

public static class Try
{
    public static Result<T> Run<T>(Func<T> func)
    {
        try { return Result<T>.Ok(func()); }
        catch (Exception ex) { return Result<T>.Fail(ex); }
    }

    public static Result<Unit> Run(Action action)
    {
        try { action(); return Result<Unit>.Ok(Unit.Value); }
        catch (Exception ex) { return Result<Unit>.Fail(ex); }
    }
}