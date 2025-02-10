using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using HTX.Net.Objects.Internal;
using HTX.Net.Objects.Models.Socket;
using HTX.Net.Objects.Sockets.Queries;

namespace HTX.Net.Objects.Sockets.Subscriptions
{
    internal class HTXAccountSubscription : Subscription<HTXSocketAuthResponse, HTXSocketAuthResponse>
    {
        private string _topic;
        private Action<DataEvent<HTXAccountUpdate>> _handler;

        public override HashSet<string> ListenerIdentifiers { get; set; }

        public HTXAccountSubscription(ILogger logger, string topic, Action<DataEvent<HTXAccountUpdate>> handler, bool authenticated) : base(logger, authenticated)
        {
            _handler = handler;
            _topic = topic;
            ListenerIdentifiers = new HashSet<string>() { topic };
        }

        public override Query? GetSubQuery(SocketConnection connection)
        {
            return new HTXAuthQuery("sub", _topic, Authenticated);
        }
        public override Query? GetUnsubQuery()
        {
            return new HTXAuthQuery("unsub", _topic, Authenticated);
        }
        public override CallResult DoHandleMessage(SocketConnection connection, DataEvent<object> message)
        {
            var update = (HTXDataEvent<HTXAccountUpdate>)message.Data;
            _handler.Invoke(message.As(update.Data, update.Channel, null, SocketUpdateType.Update).WithDataTimestamp(update.Data.ChangeTime));
            return new CallResult(null);
        }

        public override Type? GetMessageType(IMessageAccessor message) => typeof(HTXDataEvent<HTXAccountUpdate>);
    }
}
