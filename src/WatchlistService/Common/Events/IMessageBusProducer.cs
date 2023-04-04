using System.Security.Claims;
using WatchlistService.Dtos.Requests;
using WatchlistService.MessageBus.Requests;

namespace WatchlistService.Common.Events;

public interface IMessageBusProducer
{
    Task<string> PublishDecodeTokenMessage(DecodeTokenRequest decodeTokenRequest);
}