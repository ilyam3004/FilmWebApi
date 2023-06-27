using AutoMapper;
using Microsoft.AspNetCore.Mvc.Rendering;
using Shared.Models;
using TMDbLib.Objects.Movies;
using WatchlistService.Models;
using WatchlistService.Dtos.Responses;
using WatchlistService.Dtos;

namespace WatchlistService.Common.Profiles;

public class WatchlistProfile : Profile
{
    public WatchlistProfile()
    {
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

        CreateMap<Watchlist, SharedWatchlist>()
            .ForMember(dest => dest.Id, opt =>
                opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Name, opt =>
                opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.UserId, opt =>
                opt.MapFrom(src => src.UserId))
            .ForMember(dest => dest.Movies, opt =>
                opt.MapFrom(src => ConvertMovies(src.Movies)))
            .ForMember(dest => dest.DateTimeOfCreating, opt =>
                opt.MapFrom(src => src.DateTimeOfCreating));
    }

    private List<SharedWatchlistMovie> ConvertMovies(List<WatchlistMovie> movies)
    {
        List<SharedWatchlistMovie> sharedMovies = movies.Select(movie =>
            new SharedWatchlistMovie
            {
                MovieId = movie.MovieId,
                DateTimeOfAdding = movie.DateTimeOfAdding
            }).ToList();

        return sharedMovies;
    }
}