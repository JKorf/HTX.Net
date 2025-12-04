using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using HTX.Net.Objects.Internal;
using HTX.Net.Objects.Models.Socket;
using HTX.Net.Objects.Sockets.Queries;

namespace HTX.Net.Objects.Sockets.Subscriptions
{
    internal class HTXOrderDetailsSubscription : Subscription
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
            MessageRouter = MessageRouter.Create([
                MessageRoute<HTXDataEvent<HTXTradeUpdate>>.CreateWithoutTopicFilter(_topic + "trade", DoHandleMessage),
                MessageRoute<HTXDataEvent<HTXOrderCancelationUpdate>>.CreateWithoutTopicFilter(_topic + "cancellation", DoHandleMessage)
                ]);
        }

        protected override Query? GetSubQuery(SocketConnection connection)
        {
            return new HTXAuthQuery(_client, "sub", _topic, Authenticated);
        }
        protected override Query? GetUnsubQuery(SocketConnection connection)
        {
            return new HTXAuthQuery(_client, "unsub", _topic, Authenticated);
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, HTXDataEvent<HTXOrderCancelationUpdate> message)
        {
            _onOrderCancel?.Invoke(
                new DataEvent<HTXOrderCancelationUpdate>(message.Data, receiveTime, originalData)
                    .WithStreamId(message.Channel)
                    .WithSymbol(message.Data.Symbol)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithDataTimestamp(message.Timestamp)
                );
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, HTXDataEvent<HTXTradeUpdate> message)
        {
            _onOrderMatch?.Invoke(
                new DataEvent<HTXTradeUpdate>(message.Data, receiveTime, originalData)
                    .WithStreamId(message.Channel)
                    .WithSymbol(message.Data.Symbol)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithDataTimestamp(message.Timestamp)
                );
            return CallResult.SuccessResult;
        }
    }
}
