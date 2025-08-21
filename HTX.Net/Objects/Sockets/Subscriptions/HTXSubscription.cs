using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using HTX.Net.Objects.Internal;
using HTX.Net.Objects.Sockets.Queries;

namespace HTX.Net.Objects.Sockets.Subscriptions
{
    internal class HTXSubscription<T> : Subscription<HTXSocketResponse, HTXSocketResponse>
    {
        private readonly SocketApiClient _client;
        private string _topic;
        private Action<DataEvent<T>> _handler;

        public HTXSubscription(ILogger logger, SocketApiClient client, string topic, Action<DataEvent<T>> handler, bool authenticated) : base(logger, authenticated)
        {
            _client = client;
            _handler = handler;
            _topic = topic;
            MessageMatcher = MessageMatcher.Create<HTXDataEvent<T>>(topic, DoHandleMessage);
        }

        public override Query? GetSubQuery(SocketConnection connection)
        {
            return new HTXSubscribeQuery(_client, _topic, Authenticated);
        }
        public override Query? GetUnsubQuery()
        {
            return new HTXUnsubscribeQuery(_topic, Authenticated);
        }

        public CallResult DoHandleMessage(SocketConnection connection, DataEvent<HTXDataEvent<T>> message)
        {
            _handler.Invoke(message.As(message.Data.Data).WithStreamId(message.Data.Channel).WithDataTimestamp(message.Data.Timestamp));
            return CallResult.SuccessResult;
        }
    }
}
