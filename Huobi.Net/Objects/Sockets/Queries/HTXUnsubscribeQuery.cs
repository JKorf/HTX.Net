using CryptoExchange.Net;
using CryptoExchange.Net.Sockets;
using System.Collections.Generic;

namespace HTX.Net.Objects.Sockets.Queries
{
    internal class HTXUnsubscribeQuery : Query<HTXSocketResponse>
    {
        public override HashSet<string> ListenerIdentifiers { get; set; }

        public HTXUnsubscribeQuery(string topic, bool authenticated, int weight = 1) : base(new HTXUnsubscribeRequest() { Id = ExchangeHelpers.NextId().ToString(), Topic = topic }, authenticated, weight)
        {
            ListenerIdentifiers = new HashSet<string> { ((HTXUnsubscribeRequest)Request).Id };
        }

    }
}
