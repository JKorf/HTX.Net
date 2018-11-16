using CryptoExchange.Net.Converters;
using Huobi.Net.Objects;
using System.Collections.Generic;

namespace Huobi.Net.Converters
{
    public class OrderSideConverter : BaseConverter<HuobiOrderSide>
    {
        public OrderSideConverter() : this(true) { }
        public OrderSideConverter(bool quotes) : base(quotes) { }

        protected override Dictionary<HuobiOrderSide, string> Mapping => new Dictionary<HuobiOrderSide, string>
        {
            { HuobiOrderSide.Buy, "buy" },
            { HuobiOrderSide.Sell, "sell" },
        };
    }
}
