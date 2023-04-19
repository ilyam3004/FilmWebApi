using MovieService.Common.Services;
using Shared.Messages;
using Shared.Replies;
using MassTransit;

namespace MovieService.Bus.Consumers;

public class MoviesDataMessageConsumer : IConsumer<MoviesDataMessage>
{
     private readonly IMovieService _movieService;
    
     public MoviesDataMessageConsumer(IMovieService movieService)
     {
         _movieService = movieService;
     }
    
     public async Task Consume(ConsumeContext<MoviesDataMessage> context)
     {
         var movies = await _movieService
             .GetMoviesData(context.Message.MoviesId);

         await context.RespondAsync(
             new MoviesDataReply
             {
                 Movies = movies
             });
     }
}