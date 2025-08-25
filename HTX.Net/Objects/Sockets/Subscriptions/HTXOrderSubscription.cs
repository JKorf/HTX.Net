using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using HTX.Net.Objects.Internal;
using HTX.Net.Objects.Models.Socket;
using HTX.Net.Objects.Sockets.Queries;

namespace HTX.Net.Objects.Sockets.Subscriptions
{
    internal class HTXOrderSubscription : Subscription<HTXSocketAuthResponse, HTXSocketAuthResponse>
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
        }

        protected override Query? GetSubQuery(SocketConnection connection)
        {
            return new HTXAuthQuery(_client, "sub", _topic, Authenticated);
        }
        protected override Query? GetUnsubQuery(SocketConnection connection)
        {
            return new HTXAuthQuery(_client, "unsub", _topic, Authenticated);
        }

        public CallResult DoHandleMessage(SocketConnection connection, DataEvent<HTXDataEvent<HTXTriggerFailureOrderUpdate>> message)
        {
            _onConditionalOrderTriggerFailure?.Invoke(message.As(message.Data.Data, message.Data.Channel, message.Data.Data.Symbol, SocketUpdateType.Update).WithDataTimestamp(message.Data.Timestamp));
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DataEvent<HTXDataEvent<HTXOrderUpdate>> message)
        {
            _onConditionalOrderCanceled?.Invoke(message.As(message.Data.Data, message.Data.Channel, message.Data.Data.Symbol, SocketUpdateType.Update).WithDataTimestamp(message.Data.Timestamp));
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DataEvent<HTXDataEvent<HTXSubmittedOrderUpdate>> message)
        {
            _onOrderSubmitted?.Invoke(message.As(message.Data.Data, message.Data.Channel, message.Data.Data.Symbol, SocketUpdateType.Update).WithDataTimestamp(message.Data.Timestamp));
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DataEvent<HTXDataEvent<HTXMatchedOrderUpdate>> message)
        {
            _onOrderMatched?.Invoke(message.As(message.Data.Data, message.Data.Channel, message.Data.Data.Symbol, SocketUpdateType.Update).WithDataTimestamp(message.Data.Timestamp));
            return CallResult.SuccessResult;
        }

        public CallResult DoHandleMessage(SocketConnection connection, DataEvent<HTXDataEvent<HTXCanceledOrderUpdate>> message)
        {
            _onOrderCancelation?.Invoke(message.As(message.Data.Data, message.Data.Channel, message.Data.Data.Symbol, SocketUpdateType.Update).WithDataTimestamp(message.Data.Timestamp));
            return CallResult.SuccessResult;
        }
    }
}
