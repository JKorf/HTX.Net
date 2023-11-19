using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Huobi.Net.Objects.Sockets.Subscriptions
{
    internal class HuobiPingSubscription : SystemSubscription<HuobiPingMessage>
    {
        public override List<string> Identifiers { get; } = new List<string>() { "ping" };

        public HuobiPingSubscription(ILogger logger) : base(logger, false)
        {
        }

        public override Task<CallResult> HandleMessageAsync(SocketConnection connection, DataEvent<ParsedMessage<HuobiPingMessage>> message)
        {
            // TODO should be able to respond here
            connection.Send(ExchangeHelpers.NextId(), new HuobiPongMessage() { Pong = message.Data.TypedData.Ping }, 1);
            return Task.FromResult(new CallResult(null));
        }
    }
}
