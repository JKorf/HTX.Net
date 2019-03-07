using CryptoExchange.Net.Converters;
using Huobi.Net.Objects;
using System.Collections.Generic;

namespace Huobi.Net.Converters
{
    public class OrderSideConverter : BaseConverter<HuobiOrderSide>
    {
        public OrderSideConverter() : this(true) { }
        public OrderSideConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<HuobiOrderSide, string>> Mapping => new List<KeyValuePair<HuobiOrderSide, string>>
        {
            new KeyValuePair<HuobiOrderSide, string>(HuobiOrderSide.Buy, "buy"),
            new KeyValuePair<HuobiOrderSide, string>(HuobiOrderSide.Sell, "sell")
        };
    }
}
