using TMDbLib.Objects.Movies;

namespace MovieService.Common.Services;

public interface IMovieService
{
    Task<List<Movie>> GetMoviesData(List<int> moviesId);
}