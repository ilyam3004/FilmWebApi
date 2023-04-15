namespace WatchlistService.Dtos.Requests;

public record AddMovieRequest(
    string WatchlistId, 
    int MovieId);