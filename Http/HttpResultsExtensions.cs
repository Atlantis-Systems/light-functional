using Microsoft.AspNetCore.Http.HttpResults;

namespace LightFunctional.Http;

public static class HttpResultsExtensions
{
    public static Ok<T> ToOk<T>(this Result<T> result)
    {
        return TypedResults.Ok(result.Value);
    }

    public static BadRequest<string> ToBadRequest<T>(this Result<T> result)
    {
        return TypedResults.BadRequest(result.Error.Message);
    }
    
    public static NotFound ToNotFound<T>(this Result<T> result)
    {
        return TypedResults.NotFound();
    }
    
    public static Results<NotFound, BadRequest<string>, Ok<T>> ToResults<T>(this Result<T> result)
    {
        return result.IsError
            ? TypedResults.BadRequest(result.Error.Message)
            : TypedResults.Ok(result.Value);
    }
}