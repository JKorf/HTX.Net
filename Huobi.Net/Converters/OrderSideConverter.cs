using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using System.Collections.Generic;

namespace Huobi.Net.Converters
{
    internal class OrderSideConverter : BaseConverter<OrderSide>
    {
        public OrderSideConverter() : this(true) { }
        public OrderSideConverter(bool quotes) : base(quotes) { }

        protected override List<KeyValuePair<OrderSide, string>> Mapping => new List<KeyValuePair<OrderSide, string>>
        {
            new KeyValuePair<OrderSide, string>(OrderSide.Buy, "buy"),
            new KeyValuePair<OrderSide, string>(OrderSide.Sell, "sell"),

            new KeyValuePair<OrderSide, string>(OrderSide.Buy, "buy-market"),
            new KeyValuePair<OrderSide, string>(OrderSide.Sell, "sell-market"),
            new KeyValuePair<OrderSide, string>(OrderSide.Buy, "buy-limit"),
            new KeyValuePair<OrderSide, string>(OrderSide.Sell, "sell-limit"),
            new KeyValuePair<OrderSide, string>(OrderSide.Buy, "buy-ioc"),
            new KeyValuePair<OrderSide, string>(OrderSide.Sell, "sell-ioc"),
            new KeyValuePair<OrderSide, string>(OrderSide.Buy, "buy-limit-maker,"),
            new KeyValuePair<OrderSide, string>(OrderSide.Sell, "sell-limit-maker"),
            new KeyValuePair<OrderSide, string>(OrderSide.Buy, "buy-stop-limit"),
            new KeyValuePair<OrderSide, string>(OrderSide.Sell, "sell-stop-limit"),
            new KeyValuePair<OrderSide, string>(OrderSide.Buy, "buy-limit-fok"),
            new KeyValuePair<OrderSide, string>(OrderSide.Sell, "sell-limit-fok"),
            new KeyValuePair<OrderSide, string>(OrderSide.Buy, "buy-stop-limit-fok"),
            new KeyValuePair<OrderSide, string>(OrderSide.Sell, "sell-stop-limit-fok"),
        };
    }
}
