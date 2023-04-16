using TMDbLib.Client;
using TMDbLib.Objects.Movies;

namespace MovieService.Common.Services;

public class MovieServiceImp : IMovieService
{
    private TMDbClient _movieClient;
    
    public MovieServiceImp(TMDbClient movieClient)
    {
        _movieClient = movieClient;
    }

    public async Task<List<Movie>> GetMoviesData(List<int> moviesId)
    {
        var movies = new List<Movie>();

        foreach (var movieId in moviesId)
        {
            var movie = await _movieClient.GetMovieAsync(movieId);
            movies.Add(movie);
        }
        
        return movies;
    }
}