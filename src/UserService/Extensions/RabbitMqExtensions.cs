using RabbitMQ.Client;
using UserService.Common.Events.EventProcessing;
using UserService.EventProcessing;

namespace UserService.Extensions;

public static class RabbitMqExtensions
{
    public static IServiceCollection AddRabbitMq(
        this IServiceCollection services, 
        ConfigurationManager configuration)
    {
        var factory = new ConnectionFactory
        {
            HostName = configuration["RabbitMQHost"],
        };

        services.AddSingleton(factory.CreateConnection());
        services.AddSingleton<IEventProcessor, EventProcessor>();
        
        return services;
    }
}