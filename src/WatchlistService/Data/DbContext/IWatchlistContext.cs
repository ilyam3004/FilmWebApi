using MongoDB.Driver;
using WatchlistService.Models;

namespace WatchlistService.Data.DbContext
{
    public interface IWatchlistContext
    {
        IMongoCollection<Watchlist> Watchlists { get; }
    }
}