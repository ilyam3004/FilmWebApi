using MassTransit;

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
                config.Host("amqp://guest:guest@localhost:5672", hostConfigurator => { });
            });
        });
        
        services.AddMassTransitHostedService();
        
        return services;
    }
}