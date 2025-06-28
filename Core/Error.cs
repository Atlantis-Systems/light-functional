namespace LightFunctional;

public class Error
{
    public string Message { get; set; }
    public int StatusCode { get; set; }

    public Error(string message, int statusCode = 500)
    {
        Message = message;
        StatusCode = statusCode;
    }
}