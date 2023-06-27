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
            config.AddConsumer<WatchlistRecommendationsMessageConsumer>();
            
            config.UsingRabbitMq((context, config) =>
            {
                config.Host(configuration["RabbitMqConnectionString"]);
                config.ReceiveEndpoint("watchlist-recommendations", e =>
                    e.ConfigureConsumer<WatchlistRecommendationsMessageConsumer>(context));
                config.ReceiveEndpoint("movie-data", e =>
                    e.ConfigureConsumer<MoviesDataMessageConsumer>(context));
            });
        });

        return services;
    }
}