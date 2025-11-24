using System.Diagnostics;
using System.Net;
using Microsoft.AspNetCore.Diagnostics;
using TodoApi.Exceptions;
using ValidationException = TodoApi.Exceptions.ValidationException;

namespace TodoApi.Middleware;

public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger) : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(
        HttpContext httpContext, 
        Exception exception, 
        CancellationToken cancellationToken)
    {
        
        var stopwatch = httpContext.Items["RequestStopwatch"] as System.Diagnostics.Stopwatch;
        var elapsed = stopwatch?.ElapsedMilliseconds ?? 0;
        
        var traceId = Activity.Current?.Id ?? httpContext.TraceIdentifier;
        
        
        logger.LogError(
            exception,
            "Unhandled exception occurred. TraceId: {TraceId}", 
            traceId);
        
        var (statusCode, title) = MapException(exception);

        var problem = new ProblemDetailsWithTrace
        {
            Title = title,
            Detail = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Development"
                ? exception.ToString()
                : null,
            Status = (int)statusCode,
            Instance = httpContext.Request.Path,
            TraceId = traceId,
            Errors = exception is ValidationException ve
                ? ve.Errors
                : null
        };
    
        httpContext.Response.StatusCode = (int)statusCode;
        await httpContext.Response.WriteAsJsonAsync(problem, cancellationToken);
        
        return true;
    }

    private static (HttpStatusCode statusCode, string title) MapException(Exception ex)
    {
        return ex switch
        {
            BadRequestException => (HttpStatusCode.BadRequest, "Bad request"),
            UnauthorizedException => (HttpStatusCode.Unauthorized, "Unauthorized"),
            ForbiddenException => (HttpStatusCode.Forbidden, "Forbidden"),
            NotFoundException => (HttpStatusCode.NotFound, "Resource not found"),
            ConflictException => (HttpStatusCode.Conflict, "Conflict"),
            GoneException => (HttpStatusCode.Gone, "Resource gone"),
            ValidationException => (HttpStatusCode.BadRequest, "Validation error"),
            
            _ => (HttpStatusCode.InternalServerError, "An error occurred while processing your request")
        };
    }
}