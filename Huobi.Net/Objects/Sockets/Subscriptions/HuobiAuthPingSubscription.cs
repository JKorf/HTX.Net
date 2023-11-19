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
    internal class HuobiAuthPingSubscription : SystemSubscription<HuobiAuthPingMessage>
    {
        public override List<string> Identifiers { get; } = new List<string>() { "pingv2" };

        public HuobiAuthPingSubscription(ILogger logger) : base(logger, false)
        {
        }

        public override Task<CallResult> HandleMessageAsync(SocketConnection connection, DataEvent<ParsedMessage<HuobiAuthPingMessage>> message)
        {
            connection.Send(ExchangeHelpers.NextId(), new HuobiAuthPongMessage() { Action = "pong", Data = new HuobiAuthPongMessageTimestamp { Pong = message.Data.TypedData.Data.Ping } }, 1);
            return Task.FromResult(new CallResult(null));
        }
    }
}
