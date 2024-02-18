using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Huobi.Net.Objects.Sockets.Subscriptions
{
    internal class HuobiPingSubscription : SystemSubscription<HuobiPingMessage>
    {
        public override HashSet<string> ListenerIdentifiers { get; set; } = new HashSet<string>() { "pingV3" };

        public HuobiPingSubscription(ILogger logger) : base(logger, false)
        {
        }

        public override Task<CallResult> HandleMessageAsync(SocketConnection connection, DataEvent<HuobiPingMessage> message)
        {
            connection.Send(ExchangeHelpers.NextId(), new HuobiPongMessage() { Pong = message.Data.Ping }, 1);
            return Task.FromResult(new CallResult(null));
        }
    }
}
