using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;

namespace HTX.Net.Objects.Sockets.Subscriptions
{
    internal class HTXSpotPingSubscription : SystemSubscription
    {
        public HTXSpotPingSubscription(ILogger logger) : base(logger, false)
        {
            MessageMatcher = MessageMatcher.Create<HTXSpotPingWrapper>("pingV2", HandleMessage);
        }

        public CallResult HandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, HTXSpotPingWrapper message)
        {
            _ = connection.SendAsync(ExchangeHelpers.NextId(), new HTXSpotPongMessage() { Pong = new HTXSpotPingMessage { Ping = message.Data.Ping } }, 1);
            return CallResult.SuccessResult;
        }
    }
}
