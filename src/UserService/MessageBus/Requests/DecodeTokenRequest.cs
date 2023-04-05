namespace UserService.MessageBus.Requests;

public record DecodeTokenRequest
{
    public string Token { get; set; }
};