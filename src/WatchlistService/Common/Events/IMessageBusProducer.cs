using WatchlistService.Dtos.Requests;

namespace WatchlistService.Common.Events;

public interface IMessageBusProducer
{
    string PublishAuthMessage(AuthUserRequest authUserRequest);
}