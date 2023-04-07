using UserService.Bus.Consumers;
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
                config.Host(configuration["RabbitMqConnectionString"]);
                config.ReceiveEndpoint("decode-token", e =>
                    e.ConfigureConsumer<DecodeTokenMessageConsumer>(context));
            });
        });

        return services;
    }
}