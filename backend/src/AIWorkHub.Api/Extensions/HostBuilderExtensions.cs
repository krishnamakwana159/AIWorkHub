using Serilog;

namespace AIWorkHub.Api.Extensions;

/// <summary>
/// Host builder extensions.
/// </summary>
public static class HostBuilderExtensions
{
    /// <summary>
    /// Configures Serilog from application configuration.
    /// </summary>
    public static ConfigureHostBuilder AddSerilogLogging(this ConfigureHostBuilder hostBuilder)
    {
        hostBuilder.UseSerilog((context, services, configuration) =>
        {
            configuration
                .ReadFrom.Configuration(context.Configuration)
                .ReadFrom.Services(services)
                .Enrich.FromLogContext();
        });

        return hostBuilder;
    }
}
