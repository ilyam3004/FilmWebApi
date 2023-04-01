using WatchlistService.Dtos.Requests;

namespace WatchlistService.Common.Events;

public interface IMessageBusProducer
{
    Task<string> PublishAuthMessage(AuthUserRequest authUserRequest);
}