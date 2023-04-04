using MassTransit;
using WatchlistService.MessageBus;

namespace WatchlistService.Extensions;

public static class RabbitMqExtensions
{
    public static IServiceCollection AddRabbitMq(
        this IServiceCollection services, 
        ConfigurationManager configuration)
    {
        services.AddScoped<Requestor>();
        
        services.AddMassTransit(x =>
        {
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(configuration["RabbitMQHost"], h =>
                {
                    h.Username(configuration["RabbitMQUser"]);
                    h.Password(configuration["RabbitMQPassword"]);
                });
            });
        });

        services.AddMassTransitHostedService();

        return services;
    }
}