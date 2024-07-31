using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using HTX.Net.Objects.Internal;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HTX.Net.Objects.Sockets.Queries
{
    internal class HTXOpQuery : Query<HTXOpResponse>
    {
        public override HashSet<string> ListenerIdentifiers { get; set; }

        public HTXOpQuery(string topic, string op, bool authenticated, int weight = 1) : base(new HTXOpMessage { RequestId = ExchangeHelpers.NextId().ToString(), Topic = topic, Operation = op }, authenticated, weight)
        {
            ListenerIdentifiers = new HashSet<string> { ((HTXOpMessage)Request).RequestId };
        }

        public override CallResult<HTXOpResponse> HandleMessage(SocketConnection connection, DataEvent<HTXOpResponse> message)
        {
            if (message.Data.ErrorCode == 0)
                return message.ToCallResult(message.Data);

            return new CallResult<HTXOpResponse>(new ServerError(message.Data.ErrorCode!, message.Data.ErrorMessage));
        }
    }
}
