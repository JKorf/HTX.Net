using CryptoExchange.Net.Clients;
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
        private readonly SocketApiClient _client;
        private string _topic;
        private Action<DataEvent<HTXTradeUpdate>>? _onOrderMatch;
        private Action<DataEvent<HTXOrderCancelationUpdate>>? _onOrderCancel;

        public HTXOrderDetailsSubscription(
            ILogger logger,
            SocketApiClient client,
            string? symbol,
            Action<DataEvent<HTXTradeUpdate>>? onOrderMatch,
            Action<DataEvent<HTXOrderCancelationUpdate>>? onOrderCancel) : base(logger, true)
        {
            _client = client;
            _topic = $"trade.clearing#{symbol ?? "*"}#1";
            _onOrderMatch = onOrderMatch;
            _onOrderCancel = onOrderCancel;

            MessageMatcher = MessageMatcher.Create([
                new MessageHandlerLink<HTXDataEvent<HTXTradeUpdate>>(_topic + "trade", DoHandleMessage),
                new MessageHandlerLink<HTXDataEvent<HTXOrderCancelationUpdate>>(_topic + "cancellation", DoHandleMessage)
                ]);
        }

        public override Query? GetSubQuery(SocketConnection connection)
        {
            return new HTXAuthQuery(_client, "sub", _topic, Authenticated);
        }
        public override Query? GetUnsubQuery()
        {
            return new HTXAuthQuery(_client, "unsub", _topic, Authenticated);
        }

        public CallResult DoHandleMessage(SocketConnection connection, DataEvent<HTXDataEvent<HTXOrderCancelationUpdate>> message)
        {
            _onOrderCancel?.Invoke(message.As(message.Data.Data, message.Data.Channel, message.Data.Data.Symbol, SocketUpdateType.Update).WithDataTimestamp(message.Data.Timestamp));
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DataEvent<HTXDataEvent<HTXTradeUpdate>> message)
        {
            _onOrderMatch?.Invoke(message.As(message.Data.Data, message.Data.Channel, message.Data.Data.Symbol, SocketUpdateType.Update).WithDataTimestamp(message.Data.Timestamp));
            return CallResult.SuccessResult;
        }
    }
}
