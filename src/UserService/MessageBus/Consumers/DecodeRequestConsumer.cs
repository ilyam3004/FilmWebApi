using MassTransit;
using UserService.MessageBus.Requests;
using UserService.MessageBus.Responses;

namespace UserService.MessageBus.Consumers;

public class DecodeRequestConsumer : 
    IConsumer<DecodeTokenRequest>
{
    public async Task Consume(ConsumeContext<DecodeTokenRequest> context)
    {
            
            var response = new DecodeTokenResponse(context.Message.Token);

            await context.RespondAsync(response);
    }
}