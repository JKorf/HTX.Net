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
            new KeyValuePair<OrderType, string>(OrderType.Limit, "limit"),
            new KeyValuePair<OrderType, string>(OrderType.Market, "market"),
            new KeyValuePair<OrderType, string>(OrderType.IOC, "ioc"),
            new KeyValuePair<OrderType, string>(OrderType.LimitMaker, "limit-maker"),
            new KeyValuePair<OrderType, string>(OrderType.StopLimit, "stop-limit"),
            new KeyValuePair<OrderType, string>(OrderType.FillOrKillLimit, "limit-fok"),
            new KeyValuePair<OrderType, string>(OrderType.FillOrKillStopLimit, "stop-limit-fok"),

            new KeyValuePair<OrderType, string>(OrderType.Limit, "buy-limit"),
            new KeyValuePair<OrderType, string>(OrderType.Limit, "sell-limit"),
            new KeyValuePair<OrderType, string>(OrderType.Market, "buy-market"),
            new KeyValuePair<OrderType, string>(OrderType.Market, "sell-market"),
            new KeyValuePair<OrderType, string>(OrderType.IOC, "buy-ioc"),
            new KeyValuePair<OrderType, string>(OrderType.IOC, "sell-ioc"),
            new KeyValuePair<OrderType, string>(OrderType.LimitMaker, "buy-limit-maker"),
            new KeyValuePair<OrderType, string>(OrderType.LimitMaker, "sell-limit-maker"),
            new KeyValuePair<OrderType, string>(OrderType.StopLimit, "buy-stop-limit"),
            new KeyValuePair<OrderType, string>(OrderType.StopLimit, "sell-stop-limit"),

            new KeyValuePair<OrderType, string>(OrderType.FillOrKillLimit, "buy-limit-fok"),
            new KeyValuePair<OrderType, string>(OrderType.FillOrKillLimit, "sell-limit-fok"),
            new KeyValuePair<OrderType, string>(OrderType.FillOrKillStopLimit, "buy-stop-limit-fok"),
            new KeyValuePair<OrderType, string>(OrderType.FillOrKillStopLimit, "sell-stop-limit-fok")
        };
    }
}
