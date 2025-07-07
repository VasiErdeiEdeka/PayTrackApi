namespace PayTrack.Application;

public class CustomException(string message, int statusCode = 500) : Exception(message)
{
    public int StatusCode { get; } = statusCode;
}
