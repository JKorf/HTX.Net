using CryptoExchange.Net.Converters;
using Huobi.Net.Objects;
using System.Collections.Generic;

namespace Huobi.Net.Converters
{
    public class OrderTypeConverter : BaseConverter<HuobiOrderType>
    {
        public OrderTypeConverter() : this(true) { }
        public OrderTypeConverter(bool quotes) : base(quotes) { }

        protected override Dictionary<HuobiOrderType, string> Mapping => new Dictionary<HuobiOrderType, string>
        {
            { HuobiOrderType.LimitBuy, "buy-limit" },
            { HuobiOrderType.LimitSell, "sell-limit" },
            { HuobiOrderType.MarketBuy, "buy-market" },
            { HuobiOrderType.MarketSell, "sell-market" },
            { HuobiOrderType.IOCBuy, "buy-ioc" },
            { HuobiOrderType.IOCSell, "sell-ioc" },
            { HuobiOrderType.LimitMakerBuy, "buy-limit-maker" },
            { HuobiOrderType.LimitMakerSell, "sell-limit-maker" }
        };
    }
}
