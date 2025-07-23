using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;

namespace HTX.Net.Objects.Sockets.Subscriptions
{
    internal class HTXPingSubscription : SystemSubscription
    {
        public HTXPingSubscription(ILogger logger) : base(logger, false)
        {
            MessageMatcher = MessageMatcher.Create<HTXPingMessage>("pingV3", HandleMessage);
        }

        public CallResult HandleMessage(SocketConnection connection, DataEvent<HTXPingMessage> message)
        {
            connection.Send(ExchangeHelpers.NextId(), new HTXPongMessage() { Pong = message.Data.Ping }, 1);
            return CallResult.SuccessResult;
        }
    }
}
