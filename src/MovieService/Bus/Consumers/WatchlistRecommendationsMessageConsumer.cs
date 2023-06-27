using MassTransit;
using MovieService.Common.Services;
using Shared.Messages;
using Shared.Replies;

namespace MovieService.Bus.Consumers;

public class WatchlistRecommendationsMessageConsumer : IConsumer<WatchlistRecommendationsMessage>
{
    private readonly IMovieService _movieService;
    
    public WatchlistRecommendationsMessageConsumer(IMovieService movieService)
    {
        _movieService = movieService;
    }
    
    public async Task Consume(ConsumeContext<WatchlistRecommendationsMessage> context)
    {
        var movies = await _movieService
            .GetWatchlistRecommendations(
                context.Message.MoviesId, 
                context.Message.MoviesCount);
        
        await context.RespondAsync(new WatchlistRecommendationsReply
        {
            Movies = movies
        });
    }
}