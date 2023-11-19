using CryptoExchange.Net;
using CryptoExchange.Net.Sockets;
using Huobi.Net.Objects.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace Huobi.Net.Objects.Sockets.Queries
{
    internal class HuobiQuery<T> : Query<HuobiSocketResponse<T>>
    {
        public override List<string> Identifiers { get; }

        public HuobiQuery(string topic, bool authenticated, int weight = 1) : base(new HuobiSocketRequest(ExchangeHelpers.NextId().ToString(), topic), authenticated, weight)
        {
            Identifiers = new List<string> { ((HuobiSocketRequest)Request).Id };
        }

    }
}
