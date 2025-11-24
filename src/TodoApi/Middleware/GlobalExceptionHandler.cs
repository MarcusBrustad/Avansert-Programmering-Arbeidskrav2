using Microsoft.AspNetCore.Diagnostics;

namespace TodoApi.Middleware;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, 
        Exception exception, 
        CancellationToken cancellationToken)
    {
        // 🆕 Hent timing hvis det finnes
        var stopwatch = httpContext.Items["RequestStopwatch"] as System.Diagnostics.Stopwatch;
        var elapsed = stopwatch?.ElapsedMilliseconds ?? 0;
        
        logger.LogError(
            exception,
            "Unhandled exception occurred. TraceId: {TraceId}", 
            httpContext.TraceIdentifier);
        
        var (statusCode, title) = MapException(exception);

        // 🆕 Logg request completion med riktig status
        logger.LogError(
            "HTTP {Method} {Path} failed with {StatusCode} in {ElapsedMs}ms",
            httpContext.Request.Method,
            httpContext.Request.Path,
            statusCode,
            elapsed);

        await Results.Problem(
            title: title,
            statusCode: statusCode,
            extensions: new Dictionary<string, object?>
            {
                { "traceId", httpContext.TraceIdentifier }
            }
        ).ExecuteAsync(httpContext);
        
        return true;
    }

    private static (int statusCode, string title) MapException(Exception exception)
    {
        return exception switch
        {
            ArgumentNullException => (StatusCodes.Status400BadRequest, "Invalid argument"),
            ArgumentException => (StatusCodes.Status400BadRequest, "Invalid argument"),
            KeyNotFoundException => (StatusCodes.Status404NotFound, "Resource not found"),
            UnauthorizedAccessException => (StatusCodes.Status403Forbidden, "Access denied"),
            _ => (StatusCodes.Status500InternalServerError, "An error occurred while processing your request")
        };
    }
}