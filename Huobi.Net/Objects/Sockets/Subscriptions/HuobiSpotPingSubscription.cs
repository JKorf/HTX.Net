using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Huobi.Net.Objects.Sockets.Subscriptions
{
    internal class HuobiSpotPingSubscription : SystemSubscription<HuobiSpotPingWrapper>
    {
        public override HashSet<string> ListenerIdentifiers { get; set; } = new HashSet<string>() { "pingV2" };

        public HuobiSpotPingSubscription(ILogger logger) : base(logger, false)
        {
        }

        public override CallResult HandleMessage(SocketConnection connection, DataEvent<HuobiSpotPingWrapper> message)
        {
            connection.Send(ExchangeHelpers.NextId(), new HuobiSpotPongMessage() { Pong = new HuobiSpotPingMessage { Ping = message.Data.Data.Ping } }, 1);
            return new CallResult(null);
        }
    }
}
