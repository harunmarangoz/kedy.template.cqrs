using Serilog;

namespace Api.Configurations;

public static class SerilogServiceRegistration
{
    public static void AddSerilogServices(this IServiceCollection services)
    {
        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .Enrich.WithCorrelationId()
            .Enrich.FromLogContext()
            .CreateLogger();

        Log.Information($"Application starting");
    }
}