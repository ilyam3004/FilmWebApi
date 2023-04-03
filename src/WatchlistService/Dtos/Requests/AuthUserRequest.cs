namespace WatchlistService.Dtos.Requests;

public record DecodeTokenRequest(
    string Token,
    string Event);