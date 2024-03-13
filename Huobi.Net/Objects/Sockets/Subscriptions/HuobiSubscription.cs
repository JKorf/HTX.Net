using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using Huobi.Net.Objects.Internal;
using Huobi.Net.Objects.Sockets.Queries;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Huobi.Net.Objects.Sockets.Subscriptions
{
    internal class HuobiSubscription<T> : Subscription<HuobiSocketResponse, HuobiSocketResponse>
    {
        private string _topic;
        private Action<DataEvent<T>> _handler;

        public override HashSet<string> ListenerIdentifiers { get; set; }

        public HuobiSubscription(ILogger logger, string topic, Action<DataEvent<T>> handler, bool authenticated) : base(logger, authenticated)
        {
            _handler = handler;
            _topic = topic;
            ListenerIdentifiers = new HashSet<string>() { topic };
        }

        public override Query? GetSubQuery(SocketConnection connection)
        {
            return new HuobiSubscribeQuery(_topic, Authenticated);
        }
        public override Query? GetUnsubQuery()
        {
            return new HuobiUnsubscribeQuery(_topic, Authenticated);
        }

        public override Type? GetMessageType(IMessageAccessor message) => typeof(HuobiDataEvent<T>);

        public override CallResult DoHandleMessage(SocketConnection connection, DataEvent<object> message)
        {
            var huobiEvent = (HuobiDataEvent<T>)message.Data;
            _handler.Invoke(message.As(huobiEvent.Data, huobiEvent.Channel));
            return new CallResult(null);
        }
    }
}
