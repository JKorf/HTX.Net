using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using HTX.Net.Objects.Sockets.Queries;

namespace HTX.Net.Objects.Sockets.Subscriptions
{
    internal class HTXOpSubscription<T> : Subscription where T: HTXOpMessage
    {
        private readonly SocketApiClient _client;
        private string _topic;
        private Action<DateTime, string?, T> _handler;

        public HTXOpSubscription(ILogger logger, SocketApiClient client, string listenId, string topic, Action<DateTime, string?, T> handler, bool authenticated) : base(logger, authenticated)
        {
            _client = client;
            _handler = handler;
            _topic = topic;

            MessageMatcher = MessageMatcher.Create<T>(listenId, DoHandleMessage);
            MessageRouter = MessageRouter.CreateWithoutTopicFilter<T>(listenId, DoHandleMessage);
        }

        protected override Query? GetSubQuery(SocketConnection connection)
        {
            return new HTXOpQuery(_client, _topic, "sub", Authenticated);
        }
        protected override Query? GetUnsubQuery(SocketConnection connection)
        {
            return new HTXOpQuery(_client, _topic, "unsub", Authenticated);
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, T message)
        {
            _handler.Invoke(receiveTime, originalData, message);
            return CallResult.SuccessResult;
        }
    }
}
