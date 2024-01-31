using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using Huobi.Net.Objects.Internal;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Huobi.Net.Objects.Sockets.Queries
{
    internal class HuobiQuery<T> : Query<HuobiSocketResponse<T>>
    {
        public override HashSet<string> ListenerIdentifiers { get; set; }

        public HuobiQuery(string topic, bool authenticated, int weight = 1) : base(new HuobiSocketRequest(ExchangeHelpers.NextId().ToString(), topic), authenticated, weight)
        {
            ListenerIdentifiers = new HashSet<string> { ((HuobiSocketRequest)Request).Id };
        }

        public override Task<CallResult<HuobiSocketResponse<T>>> HandleMessageAsync(SocketConnection connection, DataEvent<HuobiSocketResponse<T>> message)
        {
            if (message.Data.IsSuccessful)
                return Task.FromResult(new CallResult<HuobiSocketResponse<T>>(message.Data, message.OriginalData, null));

            return Task.FromResult(new CallResult<HuobiSocketResponse<T>>(new ServerError(message.Data.ErrorCode!, message.Data.ErrorMessage)));
        }
    }
}
