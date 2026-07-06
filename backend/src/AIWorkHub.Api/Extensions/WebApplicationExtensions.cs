namespace AIWorkHub.Api.Extensions;

/// <summary>
/// Web application pipeline extensions.
/// </summary>
public static class WebApplicationExtensions
{
    /// <summary>
    /// Adds OpenAPI and Swagger middleware.
    /// </summary>
    public static WebApplication UseApiDocumentation(this WebApplication app)
    {
        if (!app.Environment.IsDevelopment())
        {
            return app;
        }

        app.UseSwagger();
        app.UseSwaggerUI(options =>
        {
            options.SwaggerEndpoint("/swagger/v1/swagger.json", "AIWorkHub API v1");
            options.DisplayRequestDuration();
        });

        return app;
    }

    /// <summary>
    /// Maps API endpoints.
    /// </summary>
    public static WebApplication MapApiEndpoints(this WebApplication app)
    {
        app.MapControllers();
        app.MapHealthChecks("/health");

        return app;
    }
}
