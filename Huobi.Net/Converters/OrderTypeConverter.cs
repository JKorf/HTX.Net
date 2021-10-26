using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using System.Collections.Generic;

namespace Huobi.Net.Converters
{
    internal class OrderTypeConverter : BaseConverter<OrderType>
    {
        public OrderTypeConverter() : this(true) { }
        public OrderTypeConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OrderType, string>> Mapping => new List<KeyValuePair<OrderType, string>>
        {
            new KeyValuePair<OrderType, string>(OrderType.LimitBuy, "buy-limit"),
            new KeyValuePair<OrderType, string>(OrderType.LimitSell, "sell-limit"),
            new KeyValuePair<OrderType, string>(OrderType.MarketBuy, "buy-market"),
            new KeyValuePair<OrderType, string>(OrderType.MarketSell, "sell-market"),
            new KeyValuePair<OrderType, string>(OrderType.IOCBuy, "buy-ioc"),
            new KeyValuePair<OrderType, string>(OrderType.IOCSell, "sell-ioc"),
            new KeyValuePair<OrderType, string>(OrderType.LimitMakerBuy, "buy-limit-maker"),
            new KeyValuePair<OrderType, string>(OrderType.LimitMakerSell, "sell-limit-maker"),
            new KeyValuePair<OrderType, string>(OrderType.StopLimitBuy, "buy-stop-limit"),
            new KeyValuePair<OrderType, string>(OrderType.StopLimitSell, "sell-stop-limit"),

            new KeyValuePair<OrderType, string>(OrderType.FillOrKillLimitBuy, "buy-limit-fok"),
            new KeyValuePair<OrderType, string>(OrderType.FillOrKillLimitSell, "sell-limit-fok"),
            new KeyValuePair<OrderType, string>(OrderType.FillOrKillStopLimitBuy, "buy-stop-limit-fok"),
            new KeyValuePair<OrderType, string>(OrderType.FillOrKillStopLimitSell, "sell-stop-limit-fok")
        };
    }
}
