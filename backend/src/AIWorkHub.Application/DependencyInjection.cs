using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace AIWorkHub.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        var assembly = Assembly.GetExecutingAssembly();

        services.AddMediatR(configuration =>
        {
            configuration.RegisterServicesFromAssembly(assembly);
        });

        services.AddAutoMapper(_ => { }, assembly);

        return services;
    }
}
