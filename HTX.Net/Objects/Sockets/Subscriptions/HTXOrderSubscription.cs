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
        private string _topic;
        private Action<DataEvent<HTXSubmittedOrderUpdate>>? _onOrderSubmitted;
        private Action<DataEvent<HTXMatchedOrderUpdate>>? _onOrderMatched;
        private Action<DataEvent<HTXCanceledOrderUpdate>>? _onOrderCancelation;
        private Action<DataEvent<HTXTriggerFailureOrderUpdate>>? _onConditionalOrderTriggerFailure;
        private Action<DataEvent<HTXOrderUpdate>>? _onConditionalOrderCanceled;

        public override HashSet<string> ListenerIdentifiers { get; set; }

        public HTXOrderSubscription(
            ILogger logger,
            string? symbol,
            Action<DataEvent<HTXSubmittedOrderUpdate>>? onOrderSubmitted,
            Action<DataEvent<HTXMatchedOrderUpdate>>? onOrderMatched,
            Action<DataEvent<HTXCanceledOrderUpdate>>? onOrderCancelation,
            Action<DataEvent<HTXTriggerFailureOrderUpdate>>? onConditionalOrderTriggerFailure,
            Action<DataEvent<HTXOrderUpdate>>? onConditionalOrderCanceled) : base(logger, true)
        {
            _topic = $"orders#{symbol ?? "*"}";
            _onOrderSubmitted = onOrderSubmitted;
            _onOrderMatched = onOrderMatched;
            _onOrderCancelation = onOrderCancelation;
            _onConditionalOrderTriggerFailure = onConditionalOrderTriggerFailure;
            _onConditionalOrderCanceled = onConditionalOrderCanceled;
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
            if (data is HTXDataEvent<HTXTriggerFailureOrderUpdate> triggerFailEvent)
                _onConditionalOrderTriggerFailure?.Invoke(message.As(triggerFailEvent.Data, triggerFailEvent.Channel, triggerFailEvent.Data.Symbol, SocketUpdateType.Update).WithDataTimestamp(triggerFailEvent.Timestamp));
            if (data is HTXDataEvent<HTXOrderUpdate> orderEvent)
                _onConditionalOrderCanceled?.Invoke(message.As(orderEvent.Data, orderEvent.Channel, orderEvent.Data.Symbol, SocketUpdateType.Update).WithDataTimestamp(orderEvent.Timestamp));
            if (data is HTXDataEvent<HTXSubmittedOrderUpdate> submitOrderEvent)
                _onOrderSubmitted?.Invoke(message.As(submitOrderEvent.Data, submitOrderEvent.Channel, submitOrderEvent.Data.Symbol, SocketUpdateType.Update).WithDataTimestamp(submitOrderEvent.Timestamp));
            if (data is HTXDataEvent<HTXMatchedOrderUpdate> matchOrderEvent)
                _onOrderMatched?.Invoke(message.As(matchOrderEvent.Data, matchOrderEvent.Channel, matchOrderEvent.Data.Symbol, SocketUpdateType.Update).WithDataTimestamp(matchOrderEvent.Timestamp));
            if (data is HTXDataEvent<HTXCanceledOrderUpdate> cancelOrderEvent)
                _onOrderCancelation?.Invoke(message.As(cancelOrderEvent.Data, cancelOrderEvent.Channel, cancelOrderEvent.Data.Symbol, SocketUpdateType.Update).WithDataTimestamp(cancelOrderEvent.Timestamp));
            return CallResult.SuccessResult;
        }

        public override Type? GetMessageType(IMessageAccessor message)
        {
            var typePath = MessagePath.Get().Property("data").Property("eventType");
            var eventType = message.GetValue<string>(typePath);
            if (string.Equals(eventType, "trigger", StringComparison.Ordinal))
                return typeof(HTXDataEvent<HTXTriggerFailureOrderUpdate>);
            if (string.Equals(eventType, "deletion", StringComparison.Ordinal))
                return typeof(HTXDataEvent<HTXOrderUpdate>);
            if (string.Equals(eventType, "creation", StringComparison.Ordinal))
                return typeof(HTXDataEvent<HTXSubmittedOrderUpdate>);
            if (string.Equals(eventType, "trade", StringComparison.Ordinal))
                return typeof(HTXDataEvent<HTXMatchedOrderUpdate>);
            if (string.Equals(eventType, "cancellation", StringComparison.Ordinal))
                return typeof(HTXDataEvent<HTXCanceledOrderUpdate>);

            return null;
        }
    }
}
