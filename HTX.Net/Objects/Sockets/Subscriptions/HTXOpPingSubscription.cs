using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;

namespace HTX.Net.Objects.Sockets.Subscriptions
{
    internal class HTXOpPingSubscription : SystemSubscription
    {
        public HTXOpPingSubscription(ILogger logger) : base(logger, false)
        {
            MessageMatcher = MessageMatcher.Create<HTXOpPingMessage>("ping");
            MessageRouter = MessageRouter.Create<HTXOpPingMessage>("ping");
        }

        public CallResult HandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, HTXOpPingMessage message)
        {
            _ = connection.SendAsync(ExchangeHelpers.NextId(), new HTXOpPingMessage { Operation = "pong", Timestamp = message.Timestamp }, 1);
            return CallResult.SuccessResult;
        }
    }
}
