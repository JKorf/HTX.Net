using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using HTX.Net.Objects.Internal;
using HTX.Net.Objects.Sockets.Queries;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HTX.Net.Objects.Sockets.Subscriptions
{
    internal class HTXOpSubscription<T> : Subscription<HTXOpResponse, HTXOpResponse> where T: HTXOpMessage
    {
        private string _topic;
        private Action<DataEvent<T>> _handler;

        public override HashSet<string> ListenerIdentifiers { get; set; }

        public HTXOpSubscription(ILogger logger, string topic, Action<DataEvent<T>> handler, bool authenticated) : base(logger, authenticated)
        {
            _handler = handler;
            _topic = topic;
            ListenerIdentifiers = new HashSet<string>() { topic };
        }

        public override Query? GetSubQuery(SocketConnection connection)
        {
            return new HTXOpQuery(_topic, "sub", Authenticated);
        }
        public override Query? GetUnsubQuery()
        {
            return new HTXOpQuery(_topic, "unsub", Authenticated);
        }

        public override Type? GetMessageType(IMessageAccessor message) => typeof(T);

        public override CallResult DoHandleMessage(SocketConnection connection, DataEvent<object> message)
        {
            var huobiEvent = (T)message.Data;
            _handler.Invoke(message.As(huobiEvent).WithUpdateType(SocketUpdateType.Update).WithStreamId(huobiEvent.Topic));
            return new CallResult(null);
        }
    }
}
