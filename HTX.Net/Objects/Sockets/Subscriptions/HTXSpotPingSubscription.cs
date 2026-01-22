using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;

namespace HTX.Net.Objects.Sockets.Subscriptions
{
    internal class HTXSpotPingSubscription : SystemSubscription
    {
        public HTXSpotPingSubscription(ILogger logger) : base(logger, false)
        {
            MessageRouter = MessageRouter.CreateWithoutTopicFilter<HTXSpotPingWrapper>("pingV2", HandleMessage);
        }

        public CallResult HandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, HTXSpotPingWrapper message)
        {
            _ = connection.SendAsync(ExchangeHelpers.NextId(), new HTXSpotPongMessage() { Pong = new HTXSpotPingMessage { Ping = message.Data.Ping } }, 1);
            return CallResult.SuccessResult;
        }
    }
}
