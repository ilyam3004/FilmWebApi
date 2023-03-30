using System.Text.Json;
using UserService.Common.Services;
using UserService.Dtos.Requests;

namespace UserService.EventProcessing;

public class EventProcessor : IEventProcessor
{
    private readonly IServiceScopeFactory _scopeFactory;

    public EventProcessor(IServiceScopeFactory scopeFactory)
    {
        _scopeFactory = scopeFactory;
    }

    public void ProcessEvent(string message)
    {
        var eventType = DetermineEvent(message);
        
        switch (eventType)
        {
            case EventType.AuthUser:
                AuthenticateUser(message);
                break;
            default:
                Console.Write("Could not determine the event type");
                break;
        }
    }

    private void AuthenticateUser(string authMessage)
    {
        Console.WriteLine($"Message received: {authMessage}");
        Console.WriteLine("Ready to authenticate user");
        using var scope = _scopeFactory.CreateScope();
        var service = scope.ServiceProvider.GetRequiredService<IUserService>();
    }

    private EventType DetermineEvent(string notificationMessage)
    {
        var eventType = JsonSerializer.Deserialize<GenericEventDto>(notificationMessage);

        return eventType!.Event switch
        {
            "Auth_User" => EventType.AuthUser,
            _ => EventType.AuthUser
        };
    }
}