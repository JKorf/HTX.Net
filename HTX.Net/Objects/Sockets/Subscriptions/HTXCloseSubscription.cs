using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;

namespace HTX.Net.Objects.Sockets.Subscriptions
{
    internal class HTXCloseSubscription : SystemSubscription
    {
        public HTXCloseSubscription(ILogger logger) : base(logger, false)
        {
            MessageRouter = MessageRouter.CreateWithoutTopicFilter<HTXOpPingMessage>("close", HandleMessage);
        }

        public CallResult HandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, HTXOpPingMessage message)
        {
            _ = connection.TriggerReconnectAsync();
            return CallResult.SuccessResult;
        }
    }
}
