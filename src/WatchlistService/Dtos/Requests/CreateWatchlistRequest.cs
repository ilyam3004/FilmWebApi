namespace WatchlistService.Dtos.Requests;

public record CreateWatchlistRequest(
    string Name,
    Guid UserId);