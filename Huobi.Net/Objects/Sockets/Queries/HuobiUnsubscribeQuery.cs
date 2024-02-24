using CryptoExchange.Net;
using CryptoExchange.Net.Sockets;
using System.Collections.Generic;

namespace Huobi.Net.Objects.Sockets.Queries
{
    internal class HuobiUnsubscribeQuery : Query<HuobiSocketResponse>
    {
        public override HashSet<string> ListenerIdentifiers { get; set; }

        public HuobiUnsubscribeQuery(string topic, bool authenticated, int weight = 1) : base(new HuobiUnsubscribeRequest() { Id = ExchangeHelpers.NextId().ToString(), Topic = topic }, authenticated, weight)
        {
            ListenerIdentifiers = new HashSet<string> { ((HuobiUnsubscribeRequest)Request).Id };
        }

    }
}
