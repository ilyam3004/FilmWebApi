using WatchlistService.Dtos.Responses;
using LanguageExt.Common;
using MediatR;

namespace WatchlistService.Messages.Commands.AddMovie;

public record AddMovieCommand(
    string WatchlistId,
    int MovieId,
    string Token) : IRequest<Result<WatchlistResponse>>;