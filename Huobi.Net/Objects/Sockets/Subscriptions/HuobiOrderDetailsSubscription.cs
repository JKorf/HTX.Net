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
    internal class HuobiOrderDetailsSubscription : Subscription<HuobiSocketAuthResponse, HuobiSocketAuthResponse>
    {
        private string _topic;
        private Action<DataEvent<HuobiTradeUpdate>>? _onOrderMatch;
        private Action<DataEvent<HuobiOrderCancelationUpdate>>? _onOrderCancel;

        public override HashSet<string> ListenerIdentifiers { get; set; }

        public HuobiOrderDetailsSubscription(
            ILogger logger,
            string? symbol,
            Action<DataEvent<HuobiTradeUpdate>>? onOrderMatch,
            Action<DataEvent<HuobiOrderCancelationUpdate>>? onOrderCancel) : base(logger, true)
        {
            _topic = $"trade.clearing#{symbol ?? "*"}#1";
            _onOrderMatch = onOrderMatch;
            _onOrderCancel = onOrderCancel;
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
            if (data is HuobiDataEvent<HuobiTradeUpdate> tradeEvent)
                _onOrderMatch?.Invoke(message.As(tradeEvent.Data, tradeEvent.Channel, tradeEvent.Data.Symbol, SocketUpdateType.Update));
            if (data is HuobiDataEvent<HuobiOrderCancelationUpdate> cancelEvent)
                _onOrderCancel?.Invoke(message.As(cancelEvent.Data, cancelEvent.Channel, cancelEvent.Data.Symbol, SocketUpdateType.Update));
            return new CallResult(null);
        }

        public override Type? GetMessageType(IMessageAccessor message)
        {
            var typePath = MessagePath.Get().Property("data").Property("eventType");
            var eventType = message.GetValue<string>(typePath);
            if (string.Equals(eventType, "trade", StringComparison.Ordinal))
                return typeof(HuobiDataEvent<HuobiTradeUpdate>);
            if (string.Equals(eventType, "cancellation", StringComparison.Ordinal))
                return typeof(HuobiDataEvent<HuobiOrderCancelationUpdate>);

            return null;
        }
    }
}
