using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using Huobi.Net.Objects.Sockets.Queries;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Huobi.Net.Objects.Sockets.Subscriptions
{
    internal class HuobiSubscription<T> : Subscription<HuobiSocketResponse, T>
    {
        private string _topic;
        private Action<DataEvent<T>> _handler;

        public override List<string> Identifiers { get; }

        public HuobiSubscription(ILogger logger, string topic, Action<DataEvent<T>> handler, bool authenticated) : base(logger, authenticated)
        {
            _handler = handler;
            _topic = topic;
            Identifiers = new List<string>() { topic };
        }

        public override BaseQuery? GetSubQuery(SocketConnection connection)
        {
            return new HuobiSubscribeQuery(_topic, Authenticated);
        }
        public override BaseQuery? GetUnsubQuery()
        {
            return new HuobiUnsubscribeQuery(_topic, Authenticated);
        }
        public override Task<CallResult> HandleEventAsync(SocketConnection connection, DataEvent<ParsedMessage<T>> message)
        {
            _handler.Invoke(message.As(message.Data.TypedData, null)); // TODO
            return Task.FromResult(new CallResult(null));
        }
    }
}
