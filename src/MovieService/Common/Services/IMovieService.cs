using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Search;
using LanguageExt.Common;
using MovieService.Dtos;

namespace MovieService.Common.Services;

public interface IMovieService
{  
    Task<Result<Movie>> GetMovieData(int movieId);
    Task<List<Movie>> GetMoviesData(List<int> moviesId);
    Task<Result<List<SearchMovie>>> SearchMovies(string query);
    Task<Result<List<SearchMovie>>> GetPopularMovies();
    Task<Result<List<SearchMovie>>> GetTopRated();
    Task<Result<List<SearchMovie>>> GetUpcoming();
    Task<Result<List<SearchMovie>>> GetNowPlayingMovies();
    Task<Result<List<RecommendationsResponse>>> GetRecommendations(string token);
}