using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.MessageParsing;
using CryptoExchange.Net.Sockets.MessageParsing.Interfaces;
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
        public override Task<CallResult> DoHandleMessageAsync(SocketConnection connection, DataEvent<object> message)
        {
            var data = message.Data;
            if (data is HuobiDataEvent<HuobiTriggerFailureOrderUpdate> triggerFailEvent)
                _onConditionalOrderTriggerFailure?.Invoke(message.As(triggerFailEvent.Data, triggerFailEvent.Channel));
            if (data is HuobiDataEvent<HuobiOrderUpdate> orderEvent)
                _onConditionalOrderCanceled?.Invoke(message.As(orderEvent.Data, orderEvent.Channel));
            if (data is HuobiDataEvent<HuobiSubmittedOrderUpdate> submitOrderEvent)
                _onOrderSubmitted?.Invoke(message.As(submitOrderEvent.Data, submitOrderEvent.Channel));
            if (data is HuobiDataEvent<HuobiMatchedOrderUpdate> matchOrderEvent)
                _onOrderMatched?.Invoke(message.As(matchOrderEvent.Data, matchOrderEvent.Channel));
            if (data is HuobiDataEvent<HuobiCanceledOrderUpdate> cancelOrderEvent)
                _onOrderCancelation?.Invoke(message.As(cancelOrderEvent.Data, cancelOrderEvent.Channel));
            return Task.FromResult(new CallResult(null));
        }

        public override Type? GetMessageType(IMessageAccessor message)
        {
            var typePath = MessagePath.Get().Property("data").Property("eventType");
            var eventType = message.GetValue<string>(typePath);
            if (eventType == "trigger")
                return typeof(HuobiDataEvent<HuobiTriggerFailureOrderUpdate>);
            if (eventType == "deletion")
                return typeof(HuobiDataEvent<HuobiOrderUpdate>);
            if (eventType == "creation")
                return typeof(HuobiDataEvent<HuobiSubmittedOrderUpdate>);
            if (eventType == "trade")
                return typeof(HuobiDataEvent<HuobiMatchedOrderUpdate>);
            if (eventType == "cancellation")
                return typeof(HuobiDataEvent<HuobiCanceledOrderUpdate>);

            return null;
        }
    }
}
