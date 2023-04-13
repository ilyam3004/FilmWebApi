using MovieService.Bus.Consumers;
using MassTransit;

namespace MovieService.Extensions;

public static class RabbitMqExtensions
{
    public static IServiceCollection AddRabbitMq(
        this IServiceCollection services, 
        ConfigurationManager configuration)
    {
        services.AddMassTransit(config =>
        {
            config.AddConsumer<MoviesDataMessageConsumer>();

            config.UsingRabbitMq((context, config) =>
            {
                config.Host(configuration["RabbitMqConnectionString"]);
                config.ReceiveEndpoint("movie-data", e =>
                    e.ConfigureConsumer<MoviesDataMessageConsumer>(context));
            });
        });

        return services;
    }
}