using UserService.MessageBus.Consumers;
using MassTransit;

namespace UserService.Extensions;

public static class RabbitMqExtensions
{
    public static IServiceCollection AddRabbitMq(
        this IServiceCollection services, 
        ConfigurationManager configuration)
    {
        services.AddMassTransit(x =>
        {
            x.AddConsumer<DecodeRequestConsumer>();
            x.AddConsumer<DecodeResponseConsumer>();
            
            
    
            x.UsingRabbitMq((context, cfg) =>
            {
                cfg.Host(configuration["RabbitMQHost"], h =>
                {
                    h.Username(configuration["RabbitMQUser"]);
                    h.Password(configuration["RabbitMQPassword"]);
                });
        
                cfg.ReceiveEndpoint("request-queue", e =>
                {
                    e.ConfigureConsumer<DecodeRequestConsumer>(context);
                });
        
                cfg.ReceiveEndpoint("response-queue", e =>
                {
                    e.ConfigureConsumer<DecodeResponseConsumer>(context);
                });
            });
        });

        services.AddMassTransitHostedService();

        return services;
    }
}