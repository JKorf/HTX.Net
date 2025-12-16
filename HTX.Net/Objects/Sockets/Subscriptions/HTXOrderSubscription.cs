using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using HTX.Net.Objects.Internal;
using HTX.Net.Objects.Models.Socket;
using HTX.Net.Objects.Sockets.Queries;

namespace HTX.Net.Objects.Sockets.Subscriptions
{
    internal class HTXOrderSubscription : Subscription
    {
        private readonly SocketApiClient _client;
        private string _topic;
        private Action<DataEvent<HTXSubmittedOrderUpdate>>? _onOrderSubmitted;
        private Action<DataEvent<HTXMatchedOrderUpdate>>? _onOrderMatched;
        private Action<DataEvent<HTXCanceledOrderUpdate>>? _onOrderCancelation;
        private Action<DataEvent<HTXTriggerFailureOrderUpdate>>? _onConditionalOrderTriggerFailure;
        private Action<DataEvent<HTXOrderUpdate>>? _onConditionalOrderCanceled;

        public HTXOrderSubscription(
            ILogger logger,
            SocketApiClient client,
            string? symbol,
            Action<DataEvent<HTXSubmittedOrderUpdate>>? onOrderSubmitted,
            Action<DataEvent<HTXMatchedOrderUpdate>>? onOrderMatched,
            Action<DataEvent<HTXCanceledOrderUpdate>>? onOrderCancelation,
            Action<DataEvent<HTXTriggerFailureOrderUpdate>>? onConditionalOrderTriggerFailure,
            Action<DataEvent<HTXOrderUpdate>>? onConditionalOrderCanceled) : base(logger, true)
        {
            _client = client;
            _topic = $"orders#{symbol ?? "*"}";
            _onOrderSubmitted = onOrderSubmitted;
            _onOrderMatched = onOrderMatched;
            _onOrderCancelation = onOrderCancelation;
            _onConditionalOrderTriggerFailure = onConditionalOrderTriggerFailure;
            _onConditionalOrderCanceled = onConditionalOrderCanceled;

            MessageMatcher = MessageMatcher.Create([
                new MessageHandlerLink<HTXDataEvent<HTXTriggerFailureOrderUpdate>>(_topic + "trigger", DoHandleMessage),
                new MessageHandlerLink<HTXDataEvent<HTXOrderUpdate>>(_topic + "deletion", DoHandleMessage),
                new MessageHandlerLink<HTXDataEvent<HTXSubmittedOrderUpdate>>(_topic + "creation", DoHandleMessage),
                new MessageHandlerLink<HTXDataEvent<HTXMatchedOrderUpdate>>(_topic + "trade", DoHandleMessage),
                new MessageHandlerLink<HTXDataEvent<HTXCanceledOrderUpdate>>(_topic + "cancellation", DoHandleMessage)
                ]);

            MessageRouter = MessageRouter.Create([
                MessageRoute<HTXDataEvent<HTXTriggerFailureOrderUpdate>>.CreateWithoutTopicFilter(_topic + "trigger", DoHandleMessage),
                MessageRoute<HTXDataEvent<HTXOrderUpdate>>.CreateWithoutTopicFilter(_topic + "deletion", DoHandleMessage),
                MessageRoute<HTXDataEvent<HTXSubmittedOrderUpdate>>.CreateWithoutTopicFilter(_topic + "creation", DoHandleMessage),
                MessageRoute<HTXDataEvent<HTXMatchedOrderUpdate>>.CreateWithoutTopicFilter(_topic + "trade", DoHandleMessage),
                MessageRoute<HTXDataEvent<HTXCanceledOrderUpdate>>.CreateWithoutTopicFilter(_topic + "cancellation", DoHandleMessage)
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

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, HTXDataEvent<HTXTriggerFailureOrderUpdate> message)
        {
            _onConditionalOrderTriggerFailure?.Invoke(
                new DataEvent<HTXTriggerFailureOrderUpdate>(HTXExchange.ExchangeName, message.Data, receiveTime, originalData)
                    .WithStreamId(message.Channel)
                    .WithSymbol(message.Data.Symbol)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithDataTimestamp(message.Timestamp)
                );
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, HTXDataEvent<HTXOrderUpdate> message)
        {
            _onConditionalOrderCanceled?.Invoke(
                new DataEvent<HTXOrderUpdate>(HTXExchange.ExchangeName, message.Data, receiveTime, originalData)
                    .WithStreamId(message.Channel)
                    .WithSymbol(message.Data.Symbol)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithDataTimestamp(message.Timestamp)
                );
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, HTXDataEvent<HTXSubmittedOrderUpdate> message)
        {
            _onOrderSubmitted?.Invoke(
                new DataEvent<HTXSubmittedOrderUpdate>(HTXExchange.ExchangeName, message.Data, receiveTime, originalData)
                    .WithStreamId(message.Channel)
                    .WithSymbol(message.Data.Symbol)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithDataTimestamp(message.Timestamp)
                );
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, HTXDataEvent<HTXMatchedOrderUpdate> message)
        {
            _onOrderMatched?.Invoke(
                new DataEvent<HTXMatchedOrderUpdate>(HTXExchange.ExchangeName, message.Data, receiveTime, originalData)
                    .WithStreamId(message.Channel)
                    .WithSymbol(message.Data.Symbol)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithDataTimestamp(message.Timestamp)
                );
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, HTXDataEvent<HTXCanceledOrderUpdate> message)
        {
            _onOrderCancelation?.Invoke(
                new DataEvent<HTXCanceledOrderUpdate>(HTXExchange.ExchangeName, message.Data, receiveTime, originalData)
                    .WithStreamId(message.Channel)
                    .WithSymbol(message.Data.Symbol)
                    .WithUpdateType(SocketUpdateType.Update)
                    .WithDataTimestamp(message.Timestamp)
                );
            return CallResult.SuccessResult;
        }
    }
}
