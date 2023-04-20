using MovieService.Common.Exceptions;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Search;
using LanguageExt.Common;
using TMDbLib.Client;

namespace MovieService.Common.Services;

public class MovieServiceImp : IMovieService
{
    private TMDbClient _movieClient;
    private const int PagesCount = 1;
    
    public MovieServiceImp(TMDbClient movieClient)
    {
        _movieClient = movieClient;
    }

    public async Task<Result<Movie>> GetMovieData(int movieId)
    {
        var movie = await _movieClient.GetMovieAsync(movieId);
        
        if (movie is null)
        {
            var exception = new MovieNotFoundException("Movie not found");
            return new Result<Movie>(exception);
        }

        return movie;
    }

    public async Task<List<Movie>> GetMoviesData(
        List<int> moviesId)
    {
        var movies = new List<Movie>();

        foreach (var movieId in moviesId)
        {
            var movie = await _movieClient.GetMovieAsync(movieId);
            if (movie is not null)
            {
                movies.Add(movie);        
            }
        }
        
        return movies;
    }

    public async Task<Result<List<SearchMovie>>> SearchMovies(string query)
    {
        var movies = await _movieClient
            .SearchMovieAsync(query, PagesCount);
        
        if (movies is null)
        {
            var exception = new MovieNotFoundException();
            return new Result<List<SearchMovie>>(exception);
        }
        
        return movies.Results.ToList();
    }

    public async Task<Result<List<SearchMovie>>> GetPopularMovies()
    {
        var movies = await _movieClient
            .GetMoviePopularListAsync(page: PagesCount);

        if (movies is null)
        {
            var exception = new MovieNotFoundException();
            return new Result<List<SearchMovie>>(exception);
        }
        
        return movies.Results.ToList();
    }

    public async Task<Result<List<SearchMovie>>> GetTopRated()
    {
        var movies = await _movieClient
            .GetMovieTopRatedListAsync(page: PagesCount);

        if (movies is null)
        {
            var exception = new MovieNotFoundException();
            return new Result<List<SearchMovie>>(exception);
        }
        
        return movies.Results.ToList();
    }

    public async Task<Result<List<SearchMovie>>> GetUpcoming()
    {
        var movies = await _movieClient
            .GetMovieUpcomingListAsync(page: PagesCount);

        if (movies is null)
        {
            var exception = new MovieNotFoundException();
            return new Result<List<SearchMovie>>(exception);
        }
        
        return movies.Results.ToList();
    }
}