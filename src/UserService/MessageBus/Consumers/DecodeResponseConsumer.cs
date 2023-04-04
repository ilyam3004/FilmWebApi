using UserService.MessageBus.Responses;
using MassTransit;

namespace UserService.MessageBus.Consumers;

public class DecodeResponseConsumer : IConsumer<DecodeTokenResponse>
{
    public async Task Consume(ConsumeContext<DecodeTokenResponse> context)
    {
        Console.WriteLine($"Received response: {context.Message.UserId}");
    }
}