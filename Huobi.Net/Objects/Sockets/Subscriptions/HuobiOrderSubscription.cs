using CryptoExchange.Net.Converters.MessageParsing;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using Huobi.Net.Objects.Internal;
using Huobi.Net.Objects.Models.Socket;
using Huobi.Net.Objects.Sockets.Queries;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Huobi.Net.Objects.Sockets.Subscriptions
{
    internal class HuobiOrderSubscription : Subscription<HuobiSocketAuthResponse, HuobiSocketAuthResponse>
    {
        private string _topic;
        private Action<DataEvent<HuobiSubmittedOrderUpdate>>? _onOrderSubmitted;
        private Action<DataEvent<HuobiMatchedOrderUpdate>>? _onOrderMatched;
        private Action<DataEvent<HuobiCanceledOrderUpdate>>? _onOrderCancelation;
        private Action<DataEvent<HuobiTriggerFailureOrderUpdate>>? _onConditionalOrderTriggerFailure;
        private Action<DataEvent<HuobiOrderUpdate>>? _onConditionalOrderCanceled;

        public override HashSet<string> ListenerIdentifiers { get; set; }

        public HuobiOrderSubscription(
            ILogger logger,
            string? symbol,
            Action<DataEvent<HuobiSubmittedOrderUpdate>>? onOrderSubmitted,
            Action<DataEvent<HuobiMatchedOrderUpdate>>? onOrderMatched,
            Action<DataEvent<HuobiCanceledOrderUpdate>>? onOrderCancelation,
            Action<DataEvent<HuobiTriggerFailureOrderUpdate>>? onConditionalOrderTriggerFailure,
            Action<DataEvent<HuobiOrderUpdate>>? onConditionalOrderCanceled) : base(logger, true)
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
            return new HuobiAuthQuery("sub", _topic, Authenticated);
        }
        public override Query? GetUnsubQuery()
        {
            return new HuobiAuthQuery("unsub", _topic, Authenticated);
        }
        public override CallResult DoHandleMessage(SocketConnection connection, DataEvent<object> message)
        {
            var data = message.Data;
            if (data is HuobiDataEvent<HuobiTriggerFailureOrderUpdate> triggerFailEvent)
                _onConditionalOrderTriggerFailure?.Invoke(message.As(triggerFailEvent.Data, triggerFailEvent.Channel, triggerFailEvent.Data.Symbol, SocketUpdateType.Update));
            if (data is HuobiDataEvent<HuobiOrderUpdate> orderEvent)
                _onConditionalOrderCanceled?.Invoke(message.As(orderEvent.Data, orderEvent.Channel, orderEvent.Data.Symbol, SocketUpdateType.Update));
            if (data is HuobiDataEvent<HuobiSubmittedOrderUpdate> submitOrderEvent)
                _onOrderSubmitted?.Invoke(message.As(submitOrderEvent.Data, submitOrderEvent.Channel, submitOrderEvent.Data.Symbol, SocketUpdateType.Update));
            if (data is HuobiDataEvent<HuobiMatchedOrderUpdate> matchOrderEvent)
                _onOrderMatched?.Invoke(message.As(matchOrderEvent.Data, matchOrderEvent.Channel, matchOrderEvent.Data.Symbol, SocketUpdateType.Update));
            if (data is HuobiDataEvent<HuobiCanceledOrderUpdate> cancelOrderEvent)
                _onOrderCancelation?.Invoke(message.As(cancelOrderEvent.Data, cancelOrderEvent.Channel, cancelOrderEvent.Data.Symbol, SocketUpdateType.Update));
            return new CallResult(null);
        }

        public override Type? GetMessageType(IMessageAccessor message)
        {
            var typePath = MessagePath.Get().Property("data").Property("eventType");
            var eventType = message.GetValue<string>(typePath);
            if (string.Equals(eventType, "trigger", StringComparison.Ordinal))
                return typeof(HuobiDataEvent<HuobiTriggerFailureOrderUpdate>);
            if (string.Equals(eventType, "deletion", StringComparison.Ordinal))
                return typeof(HuobiDataEvent<HuobiOrderUpdate>);
            if (string.Equals(eventType, "creation", StringComparison.Ordinal))
                return typeof(HuobiDataEvent<HuobiSubmittedOrderUpdate>);
            if (string.Equals(eventType, "trade", StringComparison.Ordinal))
                return typeof(HuobiDataEvent<HuobiMatchedOrderUpdate>);
            if (string.Equals(eventType, "cancellation", StringComparison.Ordinal))
                return typeof(HuobiDataEvent<HuobiCanceledOrderUpdate>);

            return null;
        }
    }
}
