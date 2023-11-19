using CryptoExchange.Net;
using CryptoExchange.Net.Sockets;
using Huobi.Net.Objects.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace Huobi.Net.Objects.Sockets.Queries
{
    internal class HuobiUnsubscribeQuery : Query<HuobiSocketResponse>
    {
        public override List<string> Identifiers { get; }

        public HuobiUnsubscribeQuery(string topic, bool authenticated, int weight = 1) : base(new HuobiUnsubscribeRequest() { Id = ExchangeHelpers.NextId().ToString(), Topic = topic }, authenticated, weight)
        {
            Identifiers = new List<string> { ((HuobiUnsubscribeRequest)Request).Id };
        }

    }
}
