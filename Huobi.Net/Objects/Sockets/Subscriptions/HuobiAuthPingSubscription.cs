using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Huobi.Net.Objects.Sockets.Subscriptions
{
    internal class HuobiAuthPingSubscription : SystemSubscription<HuobiAuthPingMessage>
    {
        public override HashSet<string> ListenerIdentifiers { get; set; } = new HashSet<string>() { "pingv2" };

        public HuobiAuthPingSubscription(ILogger logger) : base(logger, false)
        {
        }

        public override CallResult HandleMessage(SocketConnection connection, DataEvent<HuobiAuthPingMessage> message)
        {
            connection.Send(ExchangeHelpers.NextId(), new HuobiAuthPongMessage() { Action = "pong", Data = new HuobiAuthPongMessageTimestamp { Pong = message.Data.Data.Ping } }, 1);
            return new CallResult(null);
        }
    }
}
