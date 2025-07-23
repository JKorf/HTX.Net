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

        public CallResult HandleMessage(SocketConnection connection, DataEvent<HTXSpotPingWrapper> message)
        {
            connection.Send(ExchangeHelpers.NextId(), new HTXSpotPongMessage() { Pong = new HTXSpotPingMessage { Ping = message.Data.Data.Ping } }, 1);
            return CallResult.SuccessResult;
        }
    }
}
