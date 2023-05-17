using WatchlistService.Models;
using LanguageExt.Common;
using MediatR;
using WatchlistService.Dtos.Responses;

namespace WatchlistService.Messages.Commands.RemoveMovie;

public record RemoveMovieCommand(
    string WatchlistId,
    int MovieId) : IRequest<Result<WatchlistResponse>>;