namespace LightFunctional;

public class Result
{
    public Exception? Error { get; }
    public bool IsSuccess => Error is null;
    public bool IsError => !IsSuccess;

    protected Result() { }
    protected Result(Exception ex) => Error = ex;

    public static Result<T> Ok<T>(T obj) => Result<T>.Ok(obj);
    public static Result Fail(Exception ex) => new(ex);

    public Result OnError(Action<Exception> action)
    {
        if (!IsSuccess) action(Error!);
        return this;
    }
    
    public static Result Combine(params Result[] results)
    {
        var errors = new List<Exception>();

        foreach (var result in results)
            switch (result.IsSuccess)
            {
                case true:
                    break;
                default:
                    errors.Add(result.Error!);
                    break;
            }

        if (errors.Count <= 0) return Ok(Unit.Value);
        var aggregateException = new AggregateException("One or more results failed", errors);
        return Fail(aggregateException);
    }
}

public class Result<T> : Result
{
    public T Value { get; }

    private Result(T value) : base() => Value = value;
    private Result(Exception ex) : base(ex) { }

    public static Result<T> Ok(T value) => new(value);
    public new static Result<T> Fail(Exception ex) => new(ex);


    public Result<U> Map<U>(Func<T, U> func) =>
        IsSuccess ? Result<U>.Ok(func(Value!)) : Result<U>.Fail(Error!);

    public Result<T> Tap(Action<T> action)
    {
        if (IsSuccess) action(Value!);
        return this;
    }

    public new Result<T> OnError(Action<Exception> action)
    {
        if (!IsSuccess) action(Error!);
        return this;
    }
}