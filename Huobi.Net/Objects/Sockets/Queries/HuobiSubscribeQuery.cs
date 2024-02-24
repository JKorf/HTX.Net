using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Huobi.Net.Objects.Sockets.Queries
{
    internal class HuobiSubscribeQuery : Query<HuobiSocketResponse>
    {
        public override HashSet<string> ListenerIdentifiers { get; set; }

        public HuobiSubscribeQuery(string topic, bool authenticated, int weight = 1) : base(new HuobiSubscribeRequest() { Id = ExchangeHelpers.NextId().ToString(), Topic = topic }, authenticated, weight)
        {
            ListenerIdentifiers = new HashSet<string> { ((HuobiSubscribeRequest)Request).Id };
        }

        public override Task<CallResult<HuobiSocketResponse>> HandleMessageAsync(SocketConnection connection, DataEvent<HuobiSocketResponse> message)
        {
            if (message.Data.Status != "ok")
                return Task.FromResult(new CallResult<HuobiSocketResponse>(new ServerError(message.Data.ErrorMessage!)));

            return Task.FromResult(new CallResult<HuobiSocketResponse>(message.Data, message.OriginalData, null));
        }
    }
}
