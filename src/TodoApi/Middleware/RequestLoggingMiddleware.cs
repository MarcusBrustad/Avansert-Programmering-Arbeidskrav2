using Serilog.Context;

namespace TodoApi.Middleware;

public class RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        using (LogContext.PushProperty("TraceId", context.TraceIdentifier))
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();
            
            // 🆕 Lagre stopwatch i context så GlobalExceptionHandler kan bruke den
            context.Items["RequestStopwatch"] = stopwatch;
            
            // 🆕 Flag for å indikere om exception skjedde
            var exceptionOccurred = false;

            try
            {
                await next(context);
            }
            catch (Exception)
            {
                exceptionOccurred = true;
                throw; // Re-throw så GlobalExceptionHandler får den
            }
            finally
            {
                stopwatch.Stop();
                
                // 🆕 Bare logg hvis INGEN exception
                if (!exceptionOccurred)
                {
                    var status = context.Response.StatusCode;
                    var level = MapLogLevel(status);
                    
                    logger.Log(level,
                        "HTTP {Method} {Path} completed with {StatusCode} in {ElapsedMs}ms",
                        context.Request.Method,
                        context.Request.Path,
                        status,
                        stopwatch.ElapsedMilliseconds);
                }
            }
        }
    }

    private static LogLevel MapLogLevel(int statusCode)
    {
        return statusCode switch
        {
            >= 500 => LogLevel.Error,
            >= 400 => LogLevel.Warning,
            _ => LogLevel.Information
        };
    }
}

public static class RequestLoggingMiddlewareExtensions
{
    public static IApplicationBuilder UseRequestLoggingMiddleware(this IApplicationBuilder app)
    {
        return app.UseMiddleware<RequestLoggingMiddleware>();
    }
}