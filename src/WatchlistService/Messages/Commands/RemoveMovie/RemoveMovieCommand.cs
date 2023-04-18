using WatchlistService.Dtos.Responses;
using LanguageExt.Common;
using MediatR;

namespace WatchlistService.Messages.Commands.RemoveMovie;

public record RemoveMovieCommand(
    string WatchlistId,
    int MovieId) : IRequest<Result<Deleted>>;