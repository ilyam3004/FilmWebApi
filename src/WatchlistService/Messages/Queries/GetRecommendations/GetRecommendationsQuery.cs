using WatchlistService.Dtos.Responses;
using LanguageExt.Common;
using MediatR;

namespace WatchlistService.Messages.Queries.GetRecommendations;

public record GetRecommendationsQuery(string Token)
    : IRequest<Result<List<RecommendationsResponse>>>;