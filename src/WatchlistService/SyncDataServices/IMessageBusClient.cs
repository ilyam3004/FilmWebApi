using WatchlistService.Dtos.Requests;

namespace WatchlistService.SyncDataServices;

public interface IMessageBusClient
{
    void PublishAuthUser(AuthUserRequest authUserRequest);
}