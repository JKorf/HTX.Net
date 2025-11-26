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

        public CallResult HandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, HTXPingMessage message)
        {
            _ = connection.SendAsync(ExchangeHelpers.NextId(), new HTXPongMessage() { Pong = message.Ping }, 1);
            return CallResult.SuccessResult;
        }
    }
}
