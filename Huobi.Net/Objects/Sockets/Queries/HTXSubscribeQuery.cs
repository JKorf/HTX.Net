using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HTX.Net.Objects.Sockets.Queries
{
    internal class HTXSubscribeQuery : Query<HTXSocketResponse>
    {
        public override HashSet<string> ListenerIdentifiers { get; set; }

        public HTXSubscribeQuery(string topic, bool authenticated, int weight = 1) : base(new HTXSubscribeRequest() { Id = ExchangeHelpers.NextId().ToString(), Topic = topic }, authenticated, weight)
        {
            ListenerIdentifiers = new HashSet<string> { ((HTXSubscribeRequest)Request).Id };
        }

        public override CallResult<HTXSocketResponse> HandleMessage(SocketConnection connection, DataEvent<HTXSocketResponse> message)
        {
            if (message.Data.Status != "ok")
                return new CallResult<HTXSocketResponse>(new ServerError(message.Data.ErrorMessage!));

            return new CallResult<HTXSocketResponse>(message.Data, message.OriginalData, null);
        }
    }
}
