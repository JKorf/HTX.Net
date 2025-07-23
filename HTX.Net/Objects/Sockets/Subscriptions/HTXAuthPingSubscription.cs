using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;

namespace HTX.Net.Objects.Sockets.Subscriptions
{
    internal class HTXAuthPingSubscription : SystemSubscription
    {
        public HTXAuthPingSubscription(ILogger logger) : base(logger, false)
        {
            MessageMatcher = MessageMatcher.Create<HTXAuthPingMessage>("pingv2", HandleMessage);
        }

        public CallResult HandleMessage(SocketConnection connection, DataEvent<HTXAuthPingMessage> message)
        {
            connection.Send(ExchangeHelpers.NextId(), new HTXAuthPongMessage() { Action = "pong", Data = new HTXAuthPongMessageTimestamp { Pong = message.Data.Data.Ping } }, 1);
            return CallResult.SuccessResult;
        }
    }
}
