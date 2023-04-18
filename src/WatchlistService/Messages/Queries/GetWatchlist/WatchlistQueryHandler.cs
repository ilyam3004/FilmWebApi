using WatchlistService.Common.Exceptions;
using WatchlistService.Data.Repositories;
using WatchlistService.Dtos.Responses;
using TMDbLib.Objects.Movies;
using LanguageExt.Common;
using Shared.Messages;
using Shared.Replies;
using AutoMapper;
using MassTransit;
using MediatR;
using WatchlistService.Bus;

namespace WatchlistService.Messages.Queries.GetWatchlist;

public class WatchlistQueryHandler
    : IRequestHandler<GetWatchlistQuery, Result<WatchlistResponse>>
{
    private readonly IWatchListRepository _watchListRepository;
    private readonly IMapper _mapper;
    private readonly IWatchlistRequestClient _requestClient;
    
    public WatchlistQueryHandler(
        IWatchListRepository watchListRepository, 
        IWatchlistRequestClient requestClient,
        IMapper mapper)
    {
        _watchListRepository = watchListRepository;
        _requestClient = requestClient;
        _mapper = mapper;
    }
    
    //TODO Check if the user is the owner of the watchlist
    
    public async Task<Result<WatchlistResponse>> Handle(
        GetWatchlistQuery query, CancellationToken cancellationToken)
    {
        if(!await _watchListRepository
               .WatchlistExistsByIdAsync(query.WatchlistId))
        {
            var notFoundException = new WatchlistNotFoundException();
            return new Result<WatchlistResponse>(notFoundException);
        }
        
        var dbWatchlist =  await _watchListRepository
            .GetWatchlistByIdAsync(query.WatchlistId);

        var movieData = await _requestClient.GetMoviesData(
            dbWatchlist.MoviesId);
        
        return _mapper.Map<WatchlistResponse>((dbWatchlist, movieData));
    }
}