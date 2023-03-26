namespace WatchlistService.Dtos.Requests;

public record CreateWatchlistRequest(
    string Token,
    string WatchlistName,
    List<int> MoviesId);