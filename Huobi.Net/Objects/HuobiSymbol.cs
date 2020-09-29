using System;
using Huobi.Net.Converters;
using Newtonsoft.Json;

namespace Huobi.Net.Objects
{
    /// <summary>
    /// Symbol data
    /// </summary>
    public class HuobiSymbol
    {
        /// <summary>
        /// The symbol name
        /// </summary>
        public string Symbol { get; set; } = "";
        /// <summary>
        /// The base currency
        /// </summary>
        [JsonProperty("base-currency")]
        public string BaseCurrency { get; set; } = "";
        /// <summary>
        /// The quote currency
        /// </summary>
        [JsonProperty("quote-currency")]
        public string QuoteCurrency { get; set; } = "";
        /// <summary>
        /// The precision of the price in decimal numbers
        /// </summary>
        [JsonProperty("price-precision")]
        public int PricePrecision { get; set; }
        /// <summary>
        /// The precision of the amount in decimal numbers
        /// </summary>
        [JsonProperty("amount-precision")]
        public int AmountPrecision { get; set; }
        /// <summary>
        /// Partition
        /// </summary>
        [JsonProperty("symbol-partition")]
        public string SymbolPartition { get; set; } = "";
        /// <summary>
        /// The state of the symbol
        /// </summary>
        [JsonProperty("state"), JsonConverter(typeof(SymbolStateConverter))]
        public HuobiSymbolState State { get; set; }
        /// <summary>
        /// Minimum value of the amount
        /// </summary>
        [Obsolete]
        [JsonProperty("min-order-amt")]
        public decimal MinOrderAmount { get; set; }
        /// <summary>
        /// Maximum value of the amount
        /// </summary>
        [Obsolete]
        [JsonProperty("max-order-amt")]
        public decimal MaxOrderAmount { get; set; }
        /// <summary>
        /// Minimum order amount of limit order in base currency
        /// </summary>
        [JsonProperty("limit-order-min-order-amt")]
        public decimal MinLimitOrderAmount { get; set; }
        /// <summary>
        /// Max order amount of limit order in base currency
        /// </summary>
        [JsonProperty("limit-order-max-order-amt")]
        public decimal MaxLimitOrderAmount { get; set; }
        /// <summary>
        /// Minimum order amount of sell-market order in base currency
        /// </summary>
        [JsonProperty("sell-market-min-order-amt")]
        public decimal MinMarketSellOrderAmount { get; set; }
        /// <summary>
        /// Max order amount of sell-market order in base currency
        /// </summary>
        [JsonProperty("sell-market-max-order-amt")]
        public decimal MaxMarketSellOrderAmount { get; set; }
        /// <summary>
        /// Max order value of buy-market order in quote currency
        /// </summary>
        [JsonProperty("buy-market-max-order-value")]
        public decimal MaxMarketBuyOrderValue { get; set; }
        /// <summary>
        /// Minimum value of the order amount in quote currency
        /// </summary>
        [JsonProperty("min-order-value")]
        public decimal MinOrderValue { get; set; }
        /// <summary>
        /// Max order value of limit order and buy-market order in usdt
        /// </summary>
        [JsonProperty("max-order-value")]
        public decimal MaxOrderValue { get; set; }
        /// <summary>
        /// The precision of the order amount in quote currency
        /// </summary>
        [JsonProperty("value-precision")]
        public int ValuePrecision { get; set; }
    }
}
