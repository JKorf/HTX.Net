using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using HTX.Net.Objects.Internal;
using HTX.Net.Objects.Models.Socket;
using HTX.Net.Objects.Sockets.Queries;

namespace HTX.Net.Objects.Sockets.Subscriptions
{
    internal class HTXIncrementalOrderBookSubscription : Subscription<HTXSocketResponse, HTXSocketResponse>
    {
        private string _topic;
        private bool _snapshots;
        private Action<DataEvent<HTXIncrementalOrderBookUpdate>> _handler;

        public override HashSet<string> ListenerIdentifiers { get; set; }

        public HTXIncrementalOrderBookSubscription(ILogger logger, bool snapshots, string topic, Action<DataEvent<HTXIncrementalOrderBookUpdate>> handler) : base(logger, false)
        {
            _handler = handler;
            _snapshots = snapshots;
            _topic = topic;
            ListenerIdentifiers = new HashSet<string>() { topic };
        }

        public override Query? GetSubQuery(SocketConnection connection)
        {
            return new HTXSubscribeQuery(_topic, Authenticated, dataType: _snapshots ? null : "incremental");
        }
        public override Query? GetUnsubQuery()
        {
            return new HTXUnsubscribeQuery(_topic, Authenticated, dataType: _snapshots ? null : "incremental");
        }

        public override Type? GetMessageType(IMessageAccessor message) => typeof(HTXDataEvent<HTXIncrementalOrderBookUpdate>);

        public override CallResult DoHandleMessage(SocketConnection connection, DataEvent<object> message)
        {
            var htxEvent = (HTXDataEvent<HTXIncrementalOrderBookUpdate>)message.Data;
            _handler.Invoke(message.As(htxEvent.Data)
                .WithStreamId(htxEvent.Channel).WithUpdateType(htxEvent.Data.Event == "snapshot" ? SocketUpdateType.Snapshot: SocketUpdateType.Update)
                .WithDataTimestamp(htxEvent.Timestamp));
            return new CallResult(null);
        }
    }
}
