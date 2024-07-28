using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HTX.Net.Objects.Sockets.Subscriptions
{
    internal class HTXAuthPingSubscription : SystemSubscription<HTXAuthPingMessage>
    {
        public override HashSet<string> ListenerIdentifiers { get; set; } = new HashSet<string>() { "pingv2" };

        public HTXAuthPingSubscription(ILogger logger) : base(logger, false)
        {
        }

        public override CallResult HandleMessage(SocketConnection connection, DataEvent<HTXAuthPingMessage> message)
        {
            connection.Send(ExchangeHelpers.NextId(), new HTXAuthPongMessage() { Action = "pong", Data = new HTXAuthPongMessageTimestamp { Pong = message.Data.Data.Ping } }, 1);
            return new CallResult(null);
        }
    }
}
