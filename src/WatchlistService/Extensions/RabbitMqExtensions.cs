using MassTransit;
using WatchwiseShared.Messages;

namespace WatchlistService.Extensions;

public static class RabbitMqExtensions
{
    public static IServiceCollection AddRabbitMq(
        this IServiceCollection services, 
        ConfigurationManager configuration)
    {
        services.AddMassTransit(config =>
        {
            config.UsingRabbitMq((context, config) =>
            {
                config.Host(configuration["RabbitMqConnectionString"]);
            });
        });
        
        return services;
    }
}