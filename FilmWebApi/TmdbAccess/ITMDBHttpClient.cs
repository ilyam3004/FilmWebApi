using Newtonsoft.Json.Linq;

namespace FilmWebApi.TmdbAccess
{
    public interface ITMDBHttpClient
    {
        JObject GetPopularMovies();
        JObject GetTopRatedMovies();
        JObject SearchMovie(string title);
        JObject GetDayTrending();
        JObject GetWeekTrending();
        JObject GetUpComing();
        Movie GetById(int id);
    }
}