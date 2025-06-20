﻿using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;

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
            connection.Send(ExchangeHelpers.NextId(), new HTXOpPingMessage { Operation = "pong", Timestamp = message.Data.Timestamp }, 1);
            return CallResult.SuccessResult;
        }
    }
}
