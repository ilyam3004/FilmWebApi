using MassTransit;
using UserService.Common.Authentication;
using WatchwiseShared.Messages;
using WatchwiseShared.Replies;

namespace UserService.Bus.Consumers;

public class DecodeTokenMessageConsumer : IConsumer<DecodeTokenMessage>
{
    private readonly IJwtTokenService _jwtTokenService;

    public DecodeTokenMessageConsumer(IJwtTokenService jwtTokenService)
    {
        _jwtTokenService = jwtTokenService;
    }

    public async Task Consume(ConsumeContext<DecodeTokenMessage> context)
    {
        var userId = _jwtTokenService.DecodeJwt(
            context.Message.Token);

        await context.RespondAsync(
            new DecodeTokenReply
            {
                UserId = userId
            });
    }
}