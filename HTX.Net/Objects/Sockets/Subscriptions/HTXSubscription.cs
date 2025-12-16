using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using HTX.Net.Objects.Internal;
using HTX.Net.Objects.Sockets.Queries;

namespace HTX.Net.Objects.Sockets.Subscriptions
{
    internal class HTXSubscription<T> : Subscription
    {
        private readonly SocketApiClient _client;
        private string _topic;
        private Action<DateTime, string?, HTXDataEvent<T>> _handler;

        public HTXSubscription(ILogger logger, SocketApiClient client, string topic, Action<DateTime, string?, HTXDataEvent<T>> handler, bool authenticated) : base(logger, authenticated)
        {
            _client = client;
            _handler = handler;
            _topic = topic;

            MessageMatcher = MessageMatcher.Create<HTXDataEvent<T>>(topic, DoHandleMessage);
            MessageRouter = MessageRouter.CreateWithoutTopicFilter<HTXDataEvent<T>>(topic, DoHandleMessage);
        }

        protected override Query? GetSubQuery(SocketConnection connection)
        {
            return new HTXSubscribeQuery(_client, _topic, Authenticated);
        }
        protected override Query? GetUnsubQuery(SocketConnection connection)
        {
            return new HTXUnsubscribeQuery(_topic, Authenticated);
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, HTXDataEvent<T> message)
        {
            _handler.Invoke(receiveTime, originalData, message);
            return CallResult.SuccessResult;
        }
    }
}
