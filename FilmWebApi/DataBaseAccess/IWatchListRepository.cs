using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace FilmWebApi.DataBaseAccess
{
    public interface IWatchListRepository
    {
        int GetLastId();
        Task<DBMovie> Add(DBMovie movie);
        JObject GetWatchList(string login);
        Task Delete(int id);
    }
}