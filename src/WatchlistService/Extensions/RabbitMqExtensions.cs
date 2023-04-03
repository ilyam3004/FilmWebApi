using WatchlistService.Common.Events;
using RabbitMQ.Client;

namespace WatchlistService.Extensions;

public static class RabbitMqExtensions
{
    public static IServiceCollection AddRabbitMQ(
        this IServiceCollection services, 
        ConfigurationManager configuration)
    {
        var factory = new ConnectionFactory
        {
            HostName = configuration["RabbitMQHost"],
        };
        
        services.AddSingleton(factory.CreateConnection());
        services.AddSingleton<IMessageBusProducer, MessageBusProducer>();
            
        return services;
    }
}