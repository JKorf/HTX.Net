using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;

namespace HTX.Net.Objects.Sockets.Subscriptions
{
    internal class HTXCloseSubscription : SystemSubscription
    {
        public HTXCloseSubscription(ILogger logger) : base(logger, false)
        {
            MessageMatcher = MessageMatcher.Create<HTXOpPingMessage>("close");
        }

        public CallResult HandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, HTXOpPingMessage message)
        {
            _ = connection.TriggerReconnectAsync();
            return CallResult.SuccessResult;
        }
    }
}
