using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace FirstWebApi.TmdbAccess
{
    public interface ITMDBHttpClient
    {
        JObject GetPopularMovies();
        JObject SearchMovie(string title);
        JObject GetDayTrending();
        JObject GetWeekTrending();
        JObject GetUpComing();
        JObject GetLatest();
        string GetById(int id);
    }
}