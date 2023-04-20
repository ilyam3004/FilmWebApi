using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Search;
using LanguageExt.Common;

namespace MovieService.Common.Services;

public interface IMovieService
{  
    Task<Result<Movie>> GetMovieData(int movieId);
    Task<List<Movie>> GetMoviesData(List<int> moviesId);
    Task<Result<List<SearchMovie>>> SearchMovies(string query);
    Task<Result<List<SearchMovie>>> GetPopularMovies();
    Task<Result<List<SearchMovie>>> GetTopRated();
    Task<Result<List<SearchMovie>>> GetUpcoming();
}