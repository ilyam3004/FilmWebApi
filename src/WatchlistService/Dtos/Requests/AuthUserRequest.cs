namespace WatchlistService.Dtos.Requests;

public record AuthUserRequest(
    string Token,
    string Event);