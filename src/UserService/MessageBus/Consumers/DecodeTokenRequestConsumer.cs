using MassTransit;
using UserService.MessageBus.Requests;
using UserService.MessageBus.Responses;

namespace UserService.MessageBus.Consumers;

public class DecodeTokenRequestConsumer : IConsumer<DecodeTokenRequest>
{
    public async Task Consume(ConsumeContext<DecodeTokenRequest> context)
    {
        Console.WriteLine($"Received request: {context.Message}");
        var response = new DecodeTokenResponse
        {
            UserId = "userId"
        };

        await context.RespondAsync(response);
    }
}