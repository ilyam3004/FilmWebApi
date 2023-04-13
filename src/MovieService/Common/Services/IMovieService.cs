using TMDbLib.Objects.Movies;
using Shared.Messages;

namespace MovieService.Common.Services;

public interface IMovieService
{
    Task<List<Movie>> GetMoviesData(MoviesDataMessage message);
}