using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using HTX.Net.Objects.Internal;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace HTX.Net.Objects.Sockets.Queries
{
    internal class HTXQuery<T> : Query<HTXSocketResponse<T>>
    {
        public override HashSet<string> ListenerIdentifiers { get; set; }

        public HTXQuery(string topic, bool authenticated, int weight = 1) : base(new HTXSocketRequest(ExchangeHelpers.NextId().ToString(), topic), authenticated, weight)
        {
            ListenerIdentifiers = new HashSet<string> { ((HTXSocketRequest)Request).Id };
        }

        public override CallResult<HTXSocketResponse<T>> HandleMessage(SocketConnection connection, DataEvent<HTXSocketResponse<T>> message)
        {
            if (message.Data.IsSuccessful)
                return new CallResult<HTXSocketResponse<T>>(message.Data, message.OriginalData, null);

            return new CallResult<HTXSocketResponse<T>>(new ServerError(message.Data.ErrorCode!, message.Data.ErrorMessage));
        }
    }
}
