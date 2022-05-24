using Newtonsoft.Json.Linq;

namespace FilmWebApi.TmdbAccess
{
    public interface ITMDBHttpClient
    {
        JObject GetPopularMovies();
        JObject SearchMovie(string title);
        JObject GetDayTrending();
        JObject GetWeekTrending();
        JObject GetUpComing();
        //JObject GetLatest();
        Movie GetById(int id);
    }
}