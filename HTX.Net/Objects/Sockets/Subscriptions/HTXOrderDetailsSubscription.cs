using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using HTX.Net.Objects.Internal;
using HTX.Net.Objects.Models.Socket;
using HTX.Net.Objects.Sockets.Queries;

namespace HTX.Net.Objects.Sockets.Subscriptions
{
    internal class HTXOrderDetailsSubscription : Subscription<HTXSocketAuthResponse, HTXSocketAuthResponse>
    {
        private string _topic;
        private Action<DataEvent<HTXTradeUpdate>>? _onOrderMatch;
        private Action<DataEvent<HTXOrderCancelationUpdate>>? _onOrderCancel;

        public override HashSet<string> ListenerIdentifiers { get; set; }

        public HTXOrderDetailsSubscription(
            ILogger logger,
            string? symbol,
            Action<DataEvent<HTXTradeUpdate>>? onOrderMatch,
            Action<DataEvent<HTXOrderCancelationUpdate>>? onOrderCancel) : base(logger, true)
        {
            _topic = $"trade.clearing#{symbol ?? "*"}#1";
            _onOrderMatch = onOrderMatch;
            _onOrderCancel = onOrderCancel;
            ListenerIdentifiers = new HashSet<string>() { _topic };
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
            var data = message.Data;
            if (data is HTXDataEvent<HTXTradeUpdate> tradeEvent)
                _onOrderMatch?.Invoke(message.As(tradeEvent.Data, tradeEvent.Channel, tradeEvent.Data.Symbol, SocketUpdateType.Update).WithDataTimestamp(tradeEvent.Timestamp));
            if (data is HTXDataEvent<HTXOrderCancelationUpdate> cancelEvent)
                _onOrderCancel?.Invoke(message.As(cancelEvent.Data, cancelEvent.Channel, cancelEvent.Data.Symbol, SocketUpdateType.Update).WithDataTimestamp(cancelEvent.Timestamp));
            return new CallResult(null);
        }

        public override Type? GetMessageType(IMessageAccessor message)
        {
            var typePath = MessagePath.Get().Property("data").Property("eventType");
            var eventType = message.GetValue<string>(typePath);
            if (string.Equals(eventType, "trade", StringComparison.Ordinal))
                return typeof(HTXDataEvent<HTXTradeUpdate>);
            if (string.Equals(eventType, "cancellation", StringComparison.Ordinal))
                return typeof(HTXDataEvent<HTXOrderCancelationUpdate>);

            return null;
        }
    }
}
