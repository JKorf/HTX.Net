using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HTX.Net.Objects.Sockets.Subscriptions
{
    internal class HTXSpotPingSubscription : SystemSubscription<HTXSpotPingWrapper>
    {
        public override HashSet<string> ListenerIdentifiers { get; set; } = new HashSet<string>() { "pingV2" };

        public HTXSpotPingSubscription(ILogger logger) : base(logger, false)
        {
        }

        public override CallResult HandleMessage(SocketConnection connection, DataEvent<HTXSpotPingWrapper> message)
        {
            connection.Send(ExchangeHelpers.NextId(), new HTXSpotPongMessage() { Pong = new HTXSpotPingMessage { Ping = message.Data.Data.Ping } }, 1);
            return new CallResult(null);
        }
    }
}
