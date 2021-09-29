using System;
using CryptoExchange.Net.ExchangeInterfaces;
using Huobi.Net.Converters;
using Newtonsoft.Json;

namespace Huobi.Net.Objects
{
    /// <summary>
    /// Symbol data
    /// </summary>
    public class HuobiSymbol: ICommonSymbol
    {
        /// <summary>
        /// The symbol name
        /// </summary>
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// The base currency
        /// </summary>
        [JsonProperty("base-currency")]
        public string BaseCurrency { get; set; } = string.Empty;
        /// <summary>
        /// The quote currency
        /// </summary>
        [JsonProperty("quote-currency")]
        public string QuoteCurrency { get; set; } = string.Empty;
        /// <summary>
        /// The precision of the price in decimal numbers
        /// </summary>
        [JsonProperty("price-precision")]
        public int PricePrecision { get; set; }
        /// <summary>
        /// The precision of the amount in decimal numbers
        /// </summary>
        [JsonProperty("amount-precision")]
        public int QuantityPrecision { get; set; }
        /// <summary>
        /// Partition
        /// </summary>
        [JsonProperty("symbol-partition")]
        public string SymbolPartition { get; set; } = string.Empty;
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
        public decimal MinOrderQuantity { get; set; }
        /// <summary>
        /// Maximum value of the amount
        /// </summary>
        [Obsolete]
        [JsonProperty("max-order-amt")]
        public decimal MaxOrderQuantity { get; set; }
        /// <summary>
        /// Minimum order amount of limit order in base currency
        /// </summary>
        [JsonProperty("limit-order-min-order-amt")]
        public decimal MinLimitOrderQuantity { get; set; }
        /// <summary>
        /// Max order amount of limit order in base currency
        /// </summary>
        [JsonProperty("limit-order-max-order-amt")]
        public decimal MaxLimitOrderQuantity { get; set; }
        /// <summary>
        /// Minimum order amount of sell-market order in base currency
        /// </summary>
        [JsonProperty("sell-market-min-order-amt")]
        public decimal MinMarketSellOrderQuantity { get; set; }
        /// <summary>
        /// Max order amount of sell-market order in base currency
        /// </summary>
        [JsonProperty("sell-market-max-order-amt")]
        public decimal MaxMarketSellOrderQuantity { get; set; }
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

        string ICommonSymbol.CommonName => Symbol;
        decimal ICommonSymbol.CommonMinimumTradeSize => MinLimitOrderQuantity;
    }
}
