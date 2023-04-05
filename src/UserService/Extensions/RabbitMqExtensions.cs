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
            x.AddConsumer<DecodeTokenRequestConsumer>();
            x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                cfg.Host("localhost", "/", h => {
                        h.Username("guest");
                        h.Password("guest");
                });
                
                cfg.ReceiveEndpoint("/request-queue", ep =>
                {
                    ep.PrefetchCount = 16;
                    ep.UseMessageRetry(r => r.Interval(2, 100));
                    ep.ConfigureConsumer<DecodeTokenRequestConsumer>(provider);
                });
            }));
        });
        services.AddMassTransitHostedService();

        return services;
    }
}