using System.IO;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;

namespace FirstWebApi.TmdbAccess
{
    public interface ITMDBHttpClient
    {
        JObject GetTopMovies();
        JObject SearchMovie(string title);
    }
}