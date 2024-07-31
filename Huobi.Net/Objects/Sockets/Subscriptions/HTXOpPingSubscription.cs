using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HTX.Net.Objects.Sockets.Subscriptions
{
    internal class HTXOpPingSubscription : SystemSubscription<HTXOpPingMessage>
    {
        public override HashSet<string> ListenerIdentifiers { get; set; } = new HashSet<string>() { "ping" };

        public HTXOpPingSubscription(ILogger logger) : base(logger, false)
        {
        }

        public override CallResult HandleMessage(SocketConnection connection, DataEvent<HTXOpPingMessage> message)
        {
            connection.Send(ExchangeHelpers.NextId(), new { op = "pong", ts = message.Data.Timestamp.ToString() }, 1);
            return new CallResult(null);
        }
    }
}
