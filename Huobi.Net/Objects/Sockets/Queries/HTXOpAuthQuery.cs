using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using HTX.Net.Objects.Internal;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HTX.Net.Objects.Sockets.Queries
{
    internal class HTXOpAuthQuery : Query<HTXOpResponse>
    {
        public override HashSet<string> ListenerIdentifiers { get; set; }
        public HTXOpAuthQuery(HTXAuthenticationRequest2 request) : base(request, false, 1)
        {
            ListenerIdentifiers = new HashSet<string> { "auth" };
        }

        public override CallResult<HTXOpResponse> HandleMessage(SocketConnection connection, DataEvent<HTXOpResponse> message)
        {
            if (message.Data.ErrorCode != 0)
                return new CallResult<HTXOpResponse>(new ServerError(message.Data.ErrorCode, message.Data.ErrorMessage!));

            return base.HandleMessage(connection, message);
        }
    }
}
