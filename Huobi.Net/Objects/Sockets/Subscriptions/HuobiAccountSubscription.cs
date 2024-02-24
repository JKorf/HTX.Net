using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
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
    internal class HuobiAccountSubscription : Subscription<HuobiSocketAuthResponse, HuobiSocketAuthResponse>
    {
        private string _topic;
        private Action<DataEvent<HuobiAccountUpdate>> _handler;

        public override HashSet<string> ListenerIdentifiers { get; set; }

        public HuobiAccountSubscription(ILogger logger, string topic, Action<DataEvent<HuobiAccountUpdate>> handler, bool authenticated) : base(logger, authenticated)
        {
            _handler = handler;
            _topic = topic;
            ListenerIdentifiers = new HashSet<string>() { topic };
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
            var update = (HuobiDataEvent<HuobiAccountUpdate>)message.Data;
            _handler.Invoke(message.As(update.Data, update.Channel));
            return Task.FromResult(new CallResult(null));
        }

        public override Type? GetMessageType(IMessageAccessor message) => typeof(HuobiDataEvent<HuobiAccountUpdate>);
    }
}
