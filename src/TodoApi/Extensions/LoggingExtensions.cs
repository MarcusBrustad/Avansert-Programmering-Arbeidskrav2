using Serilog;

namespace TodoApi.Extensions;

public static class LoggingExtensions
{
    public static WebApplicationBuilder AddSerilog(this WebApplicationBuilder builder)
    {
        Log.Logger = new LoggerConfiguration()
            .ReadFrom.Configuration(builder.Configuration)
            .CreateLogger();

        builder.Host.UseSerilog();
        
        return builder;
    }
}