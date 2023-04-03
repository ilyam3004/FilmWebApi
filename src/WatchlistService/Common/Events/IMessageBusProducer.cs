using System.Security.Claims;
using WatchlistService.Dtos.Requests;

namespace WatchlistService.Common.Events;

public interface IMessageBusProducer
{
    string PublishDecodeTokenMessage(DecodeTokenRequest decodeTokenRequest);
}