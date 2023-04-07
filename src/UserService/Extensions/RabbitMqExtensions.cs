using UserService.MessageBus.Consumers;
using MassTransit;

namespace UserService.Extensions;

public static class RabbitMqExtensions
{
    public static IServiceCollection AddRabbitMq(
        this IServiceCollection services, 
        ConfigurationManager configuration)
    {
        services.AddMassTransit(config =>
        {
            config.AddConsumer<DecodeTokenMessageConsumer>();  
            
            config.UsingRabbitMq((context, config) =>
            {
                config.Host("amqp://guest:guest@localhost:5672");

                config.ReceiveEndpoint("decode-token", e =>
                {
                    e.ConfigureConsumer<DecodeTokenMessageConsumer>(context);
                });
            });
        });

        services.AddMassTransitHostedService();
        
        return services;
    }
}