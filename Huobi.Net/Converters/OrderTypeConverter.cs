using CryptoExchange.Net.Converters;
using Huobi.Net.Objects;
using System.Collections.Generic;

namespace Huobi.Net.Converters
{
    internal class OrderTypeConverter : BaseConverter<HuobiOrderType>
    {
        public OrderTypeConverter() : this(true) { }
        public OrderTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<HuobiOrderType, string>> Mapping => new List<KeyValuePair<HuobiOrderType, string>>
        {
            new KeyValuePair<HuobiOrderType, string>(HuobiOrderType.LimitBuy, "buy-limit"),
            new KeyValuePair<HuobiOrderType, string>(HuobiOrderType.LimitSell, "sell-limit"),
            new KeyValuePair<HuobiOrderType, string>(HuobiOrderType.MarketBuy, "buy-market"),
            new KeyValuePair<HuobiOrderType, string>(HuobiOrderType.MarketSell, "sell-market"),
            new KeyValuePair<HuobiOrderType, string>(HuobiOrderType.IOCBuy, "buy-ioc"),
            new KeyValuePair<HuobiOrderType, string>(HuobiOrderType.IOCSell, "sell-ioc"),
            new KeyValuePair<HuobiOrderType, string>(HuobiOrderType.LimitMakerBuy, "buy-limit-maker"),
            new KeyValuePair<HuobiOrderType, string>(HuobiOrderType.LimitMakerSell, "sell-limit-maker"),
            new KeyValuePair<HuobiOrderType, string>(HuobiOrderType.StopLimitBuy, "buy-stop-limit"),
            new KeyValuePair<HuobiOrderType, string>(HuobiOrderType.StopLimitSell, "sell-stop-limit"),

            new KeyValuePair<HuobiOrderType, string>(HuobiOrderType.FillOrKillLimitBuy, "buy-limit-fok"),
            new KeyValuePair<HuobiOrderType, string>(HuobiOrderType.FillOrKillLimitSell, "sell-limit-fok"),
            new KeyValuePair<HuobiOrderType, string>(HuobiOrderType.FillOrKillStopLimitBuy, "buy-stop-limit-fok"),
            new KeyValuePair<HuobiOrderType, string>(HuobiOrderType.FillOrKillStopLimitSell, "sell-stop-limit-fok")
        };
    }
}
