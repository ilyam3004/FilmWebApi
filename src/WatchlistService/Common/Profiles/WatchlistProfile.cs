using AutoMapper;
using TMDbLib.Objects.Movies;
using WatchlistService.Models;
using WatchlistService.Dtos.Responses;
using WatchlistService.Dtos;

namespace WatchlistService.Common.Profiles;

public class WatchlistProfile : Profile
{
    private const int MoviesCountInEmptyWatchlist = 0; 
    
    public WatchlistProfile()
    {
        CreateMap<Watchlist, CreateWatchlistResponse>()
            .ForMember(dest => dest.Id, opt =>
                opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt =>
                opt.MapFrom(src => src.Name));

        CreateMap<(Watchlist, List<MovieResponse>), WatchlistResponse>()
            .ForMember(dest => dest.Id, opt =>
                opt.MapFrom(src => src.Item1.Id))
            .ForMember(dest => dest.Name, opt =>
                opt.MapFrom(src => src.Item1.Name))
            .ForMember(dest => dest.Movies, opt =>
                opt.MapFrom(src => src.Item2))
            .ForMember(dest => dest.MoviesCount, opt =>
                opt.MapFrom(src => src.Item2.Count))
            .ForMember(dest => dest.DateTimeOfCreating, opt =>
                opt.MapFrom(src => src.Item1.DateTimeOfCreating));
    }
}