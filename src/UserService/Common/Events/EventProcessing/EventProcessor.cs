using System.Text.Json;
using UserService.Common.Authentication;
using UserService.Dtos.Requests;
using UserService.EventProcessing;

namespace UserService.Common.Events.EventProcessing;

public class EventProcessor : IEventProcessor
{
    private readonly IServiceScopeFactory _scopeFactory;

    public EventProcessor(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public string ProcessEvent(string message)
    {
        var eventType = DetermineEvent(message);

        switch (eventType)
        {
            case EventType.DecodeToken:
                var request = JsonSerializer
                    .Deserialize<DecodeTokenRequest>(message);
                return DecodeToken(request!.Token);
                break;
            default:
                Console.Write("Could not determine the event type");
                break;
        }

        return "";
    }

    private string DecodeToken(string token)
    {
        using var scope = _scopeFactory.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IJwtTokenService>();
        
        return service.DecodeJwt(token);
    }

    private EventType DetermineEvent(string notificationMessage)
    {
        var eventType = JsonSerializer
            .Deserialize<GenericEventDto>(notificationMessage);

        return eventType!.Event switch
        {
            "decode-token-event" => EventType.DecodeToken,
            _ => EventType.DecodeToken
        };
    }
}