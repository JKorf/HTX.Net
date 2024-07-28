using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HTX.Net.Objects.Sockets.Subscriptions
{
    internal class HTXPingSubscription : SystemSubscription<HTXPingMessage>
    {
        public override HashSet<string> ListenerIdentifiers { get; set; } = new HashSet<string>() { "pingV3" };

        public HTXPingSubscription(ILogger logger) : base(logger, false)
        {
        }

        public override CallResult HandleMessage(SocketConnection connection, DataEvent<HTXPingMessage> message)
        {
            connection.Send(ExchangeHelpers.NextId(), new HTXPongMessage() { Pong = message.Data.Ping }, 1);
            return new CallResult(null);
        }
    }
}
