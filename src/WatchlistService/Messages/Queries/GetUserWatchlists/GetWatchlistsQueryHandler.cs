using WatchlistService.Data.Repositories;
using WatchlistService.Dtos.Responses;
using WatchlistService.Bus;
using LanguageExt.Common;
using AutoMapper;
using MediatR;

namespace WatchlistService.Messages.Queries.GetUserWatchlists;

public class GetWatchlistsQueryHandler :
    IRequestHandler<GetWatchlistsQuery, Result<List<WatchlistResponse>>>
{
    private readonly IWatchListRepository _watchListRepository;
    private readonly IWatchlistRequestClient _requestClient;
    private readonly IMapper _mapper;
    
    public GetWatchlistsQueryHandler(
        IWatchListRepository watchListRepository, 
        IWatchlistRequestClient requestClient, 
        IMapper mapper)
    {
        _watchListRepository = watchListRepository;
        _requestClient = requestClient;
        _mapper = mapper;
    }

    public async Task<Result<List<WatchlistResponse>>> Handle(
        GetWatchlistsQuery query, 
        CancellationToken cancellationToken)
    {
        string userId = await _requestClient
            .GetUserIdFromToken(query.Token);

        var dbWatchlists = await _watchListRepository
            .GetWatchlistsAsync(userId);

        var watchlists = new List<WatchlistResponse>();

        foreach (var watchlist in dbWatchlists)
        {
            var movieIds = watchlist.Movies
                .Select(movie => movie.MovieId).ToList();

            var moviesData = await _requestClient
                .GetMoviesData(movieIds);


            watchlists.Add(_mapper
                .Map<WatchlistResponse>((watchlist, moviesData)));
        }

        return watchlists;
    }
}