using System.Collections.Generic;
using System.Threading.Tasks;

namespace FirstWebApi.DataAccess
{
    public interface IWatchListRepository
    {
        int GetLastId();
        Task<Movie> Add(Movie movie);
        List<Movie> GetWatchList(string login);
    }
}