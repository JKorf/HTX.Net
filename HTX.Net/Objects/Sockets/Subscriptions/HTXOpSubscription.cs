using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using HTX.Net.Objects.Sockets.Queries;

namespace HTX.Net.Objects.Sockets.Subscriptions
{
    internal class HTXOpSubscription<T> : Subscription<HTXOpResponse, HTXOpResponse> where T: HTXOpMessage
    {
        private string _topic;
        private Action<DataEvent<T>> _handler;

        public override HashSet<string> ListenerIdentifiers { get; set; }

        public HTXOpSubscription(ILogger logger, string listenId, string topic, Action<DataEvent<T>> handler, bool authenticated) : base(logger, authenticated)
        {
            _handler = handler;
            _topic = topic;
            ListenerIdentifiers = new HashSet<string>() { listenId };
        }

        public override Query? GetSubQuery(SocketConnection connection)
        {
            return new HTXOpQuery(_topic, "sub", Authenticated);
        }
        public override Query? GetUnsubQuery()
        {
            return new HTXOpQuery(_topic, "unsub", Authenticated);
        }

        public override Type? GetMessageType(IMessageAccessor message) => typeof(T);

        public override CallResult DoHandleMessage(SocketConnection connection, DataEvent<object> message)
        {
            var htxEvent = (T)message.Data;
            _handler.Invoke(message.As(htxEvent)
                .WithUpdateType(SocketUpdateType.Update)
                .WithStreamId(htxEvent.Topic));
            return CallResult.SuccessResult;
        }
    }
}
