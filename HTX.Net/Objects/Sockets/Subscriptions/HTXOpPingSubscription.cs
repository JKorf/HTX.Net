using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;

namespace HTX.Net.Objects.Sockets.Subscriptions
{
    internal class HTXOpPingSubscription : SystemSubscription
    {
        public HTXOpPingSubscription(ILogger logger) : base(logger, false)
        {
            MessageMatcher = MessageMatcher.Create<HTXOpPingMessage>("ping");
        }

        public CallResult HandleMessage(SocketConnection connection, DataEvent<HTXOpPingMessage> message)
        {
            connection.Send(ExchangeHelpers.NextId(), new HTXOpPingMessage { Operation = "pong", Timestamp = message.Data.Timestamp }, 1);
            return CallResult.SuccessResult;
        }
    }
}
