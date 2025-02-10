using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using HTX.Net.Objects.Internal;
using HTX.Net.Objects.Sockets.Queries;

namespace HTX.Net.Objects.Sockets.Subscriptions
{
    internal class HTXSubscription<T> : Subscription<HTXSocketResponse, HTXSocketResponse>
    {
        private string _topic;
        private Action<DataEvent<T>> _handler;

        public override HashSet<string> ListenerIdentifiers { get; set; }

        public HTXSubscription(ILogger logger, string topic, Action<DataEvent<T>> handler, bool authenticated) : base(logger, authenticated)
        {
            _handler = handler;
            _topic = topic;
            ListenerIdentifiers = new HashSet<string>() { topic };
        }

        public override Query? GetSubQuery(SocketConnection connection)
        {
            return new HTXSubscribeQuery(_topic, Authenticated);
        }
        public override Query? GetUnsubQuery()
        {
            return new HTXUnsubscribeQuery(_topic, Authenticated);
        }

        public override Type? GetMessageType(IMessageAccessor message) => typeof(HTXDataEvent<T>);

        public override CallResult DoHandleMessage(SocketConnection connection, DataEvent<object> message)
        {
            var htxEvent = (HTXDataEvent<T>)message.Data;
            _handler.Invoke(message.As(htxEvent.Data).WithStreamId(htxEvent.Channel).WithDataTimestamp(htxEvent.Timestamp));
            return new CallResult(null);
        }
    }
}
