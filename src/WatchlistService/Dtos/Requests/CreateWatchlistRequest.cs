namespace WatchlistService.Dtos.Requests;

public record CreateWatchlistRequest(
    string WatchlistName,
    List<int> MoviesId);