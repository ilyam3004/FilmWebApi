using MovieService.Common.Exceptions;
using TMDbLib.Objects.General;
using TMDbLib.Objects.Movies;
using TMDbLib.Objects.Search;
using LanguageExt.Common;
using TMDbLib.Client;

namespace MovieService.Common.Services;

public class MovieServiceImp : IMovieService
{
    private readonly TMDbClient _movieClient;
    private const int PagesCount = 1;

    public MovieServiceImp(TMDbClient movieClient)
    {
        _movieClient = movieClient;
    }

    public async Task<Result<Movie>> GetMovieData(int movieId)
    {
        Movie movie = await _movieClient.GetMovieAsync(movieId);
        
        if (movie is null)
        {
            var exception = new MovieNotFoundException("Movie not found");
            return new Result<Movie>(exception);
        }
        
        if (movie.Video)
        {
            var videos = await GetMovieVideos(movie.Id);
            movie.Videos = videos;
            
            if(videos.Results.Count == 0)
                movie.Video = false;
        }
        
        SetMovieImagesLinks(ref movie);
        
        return movie;
    }

    public async Task<List<Movie>> GetMoviesData(List<int> moviesId)
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
        
        SetMovieImagesLinks(ref movies);

        return movies;
    }

    public async Task<Result<List<SearchMovie>>> SearchMovies(string query)
    {
        var movies = await _movieClient
            .SearchMovieAsync(query, PagesCount);
        
        if (movies.Results.Count == 0)
        {
            var exception = new MovieNotFoundException();
            return new Result<List<SearchMovie>>(exception);
        }
        
        SetMovieImagesLinks(ref movies);

        List<SearchMovie> sortedMovies = movies.Results
            .OrderBy(m => m.ReleaseDate).ToList();
        
        return sortedMovies;
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
        
        SetMovieImagesLinks(ref movies);

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
        
        SetMovieImagesLinks(ref movies);
        
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
        
        SetMovieImagesLinks(ref movies);
        
        return movies.Results.ToList();
    }

    private async Task<ResultContainer<Video>> GetMovieVideos(int movieId)
    {
        var videos  = await _movieClient
            .GetMovieVideosAsync(movieId);

        foreach (var video in videos.Results)
        {
            if (video.Site != "YouTube")
            {
                videos.Results.Remove(video);
            }
        }

        return videos;
    }

    private void SetMovieImagesLinks(
        ref SearchContainer<SearchMovie> movies)
    {
        movies.Results.ForEach(m => SetMovieImagesLinks(ref m));
    }
    
    private void SetMovieImagesLinks(
        ref List<Movie> movies)
    {
        movies.ForEach(m => SetMovieImagesLinks(ref m));
    }
    
    private void SetMovieImagesLinks(
        ref SearchContainerWithDates<SearchMovie> movies)
    {
        movies.Results.ForEach(m => SetMovieImagesLinks(ref m));
    }
    
    private void SetMovieImagesLinks(
        ref Movie movie)
    {
        movie.BackdropPath = "https://image.tmdb.org/t/p/original" + movie.BackdropPath;
        movie.PosterPath = "https://image.tmdb.org/t/p/original" + movie.PosterPath;
    }

    private void SetMovieImagesLinks(
        ref SearchMovie movie)
    {
        movie.BackdropPath = "https://image.tmdb.org/t/p/original" + movie.BackdropPath;
        movie.PosterPath = "https://image.tmdb.org/t/p/original" + movie.PosterPath;
    }
}