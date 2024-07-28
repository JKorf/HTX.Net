using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HTX.Net.Objects.Sockets.Queries
{
    internal class HTXAuthQuery : Query<HTXSocketAuthResponse>
    {
        public override HashSet<string> ListenerIdentifiers { get; set; }
        public HTXAuthQuery(string action, string topic, bool authenticated, int weight = 1) : base(new HTXAuthRequest() { Action = action, Channel = topic }, authenticated, weight)
        {
            ListenerIdentifiers = new HashSet<string> { action + topic };
        }
        public HTXAuthQuery(HTXAuthRequest request) : base(request, true, 1)
        {
            ListenerIdentifiers = new HashSet<string> { request.Action + request.Channel };
        }

        public override CallResult<HTXSocketAuthResponse> HandleMessage(SocketConnection connection, DataEvent<HTXSocketAuthResponse> message)
        {
            if (message.Data.Code != 200)
                return new CallResult<HTXSocketAuthResponse>(new ServerError(message.Data.Code, message.Data.Message!));

            return base.HandleMessage(connection, message);
        }
    }
}
