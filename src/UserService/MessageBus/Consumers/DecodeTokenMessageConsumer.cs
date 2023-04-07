using Watchwise.Shared.Responses;
using Watchwise.Shared.Messages;
using MassTransit;

namespace UserService.MessageBus.Consumers;

public class DecodeTokenMessageConsumer : IConsumer<DecodeTokenMessage>
{
    public async Task Consume(ConsumeContext<DecodeTokenMessage> context)
    {
        await context.RespondAsync(new DecodeTokenMessageResponse
        {
            UserId = "UserId"
        });
    }
}