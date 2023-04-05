using WatchlistService.MessageBus.Requests;
using MassTransit;

namespace WatchlistService.Extensions;

public static class RabbitMqExtensions
{
    public static IServiceCollection AddRabbitMq(
        this IServiceCollection services, 
        ConfigurationManager configuration)
    {
        services.AddMassTransit(x =>
        {
            x.AddBus(provider => Bus.Factory
                .CreateUsingRabbitMq(config =>
            {
                config.Host($"localhost", "/", h => {
                        h.Username("guest");
                        h.Password("guest");
                });
            }));

            x.AddRequestClient<DecodeTokenRequest>(
                new Uri("queue:my-new-request-queue"));
        });
        services.AddMassTransitHostedService();
        
        return services;
    }
}