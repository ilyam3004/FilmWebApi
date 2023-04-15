using Shared.Messages;
using TMDbLib.Client;
using TMDbLib.Objects.Movies;

namespace MovieService.Common.Services;

public class MovieServiceImp : IMovieService
{
    private readonly TMDbClient _movieClient;

    public MovieServiceImp(IConfiguration configuration)
    {
        _movieClient = new TMDbClient(configuration["TmdbApiKey"]);
    }

    public async Task<List<Movie>> GetMoviesData(MoviesDataMessage message)
    {
        var movies = new List<Movie>();

        foreach (var movieId in message.MoviesId)
        {
            var movie = await _movieClient.GetMovieAsync(movieId);
            movies.Add(movie);
        }
        
        return movies;
    }
}