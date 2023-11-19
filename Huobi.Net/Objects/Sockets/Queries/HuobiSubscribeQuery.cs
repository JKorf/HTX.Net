using CryptoExchange.Net;
using CryptoExchange.Net.Sockets;
using Huobi.Net.Objects.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace Huobi.Net.Objects.Sockets.Queries
{
    internal class HuobiSubscribeQuery : Query<HuobiSocketResponse>
    {
        public override List<string> Identifiers { get; }

        public HuobiSubscribeQuery(string topic, bool authenticated, int weight = 1) : base(new HuobiSubscribeRequest() { Id = ExchangeHelpers.NextId().ToString(), Topic = topic }, authenticated, weight)
        {
            Identifiers = new List<string> { ((HuobiSubscribeRequest)Request).Id };
        }

    }
}
