using PayTrack.Application;
using System.Net;
using System.Text.Json;

namespace PayTrack.API.Middleware;

public class ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await next(httpContext);
        }
        catch (CustomException ex)
        {
            logger.LogError(ex, "Custom exception: {ExMessage}", ex.Message);
            await HandleCustomExceptionAsync(httpContext, ex);

        }
        catch (Exception ex)
        {
            logger.LogError(ex, "Unhandled exception: {ExMessage}", ex.Message);
            await HandleExceptionAsync(httpContext, ex);
        }
    }

    private Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

        var errorId = Guid.NewGuid();

        logger.LogError(exception,
            "[{ErrorId}] Exception: \nPath: {Path}\nMessage: {Message}",
            errorId, context.Request.Path, exception.Message);

        var result = JsonSerializer.Serialize(new
        {
            context.Response.StatusCode,
            Message = "An unexpected error occurred.",
            ErrorId = errorId
        });

        return context.Response.WriteAsync(result);
    }

    private Task HandleCustomExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";
        context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;


        logger.LogError(exception,
            "{Message}", exception.Message);

        var result = JsonSerializer.Serialize(new
        {
            context.Response.StatusCode,
            exception.Message,
        });

        return context.Response.WriteAsync(result);
    }
}