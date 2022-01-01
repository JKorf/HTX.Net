using System;
using Huobi.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models
{
    /// <summary>
    /// Symbol data
    /// </summary>
    public class HuobiSymbol
    {
        /// <summary>
        /// The symbol name
        /// </summary>
        [JsonProperty("symbol")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// The base asset
        /// </summary>
        [JsonProperty("base-currency")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// The quote asset
        /// </summary>
        [JsonProperty("quote-currency")]
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// The precision of the price in decimal numbers
        /// </summary>
        [JsonProperty("price-precision")]
        public int PricePrecision { get; set; }
        /// <summary>
        /// The precision of the quantity in decimal numbers
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
        public SymbolState State { get; set; }
        /// <summary>
        /// Minimum value of the quantity
        /// </summary>
        [Obsolete("Use MinLimitOrderQuantity instead")]
        [JsonProperty("min-order-amt")]
        public decimal MinOrderQuantity { get; set; }
        /// <summary>
        /// Maximum value of the quantity
        /// </summary>
        [Obsolete("Use MaxLimitOrderQuantity instead")]
        [JsonProperty("max-order-amt")]
        public decimal MaxOrderQuantity { get; set; }
        /// <summary>
        /// Minimum order quantity of limit order in base asset
        /// </summary>
        [JsonProperty("limit-order-min-order-amt")]
        public decimal MinLimitOrderQuantity { get; set; }
        /// <summary>
        /// Max buy order quantity of limit order in base asset
        /// </summary>
        [JsonProperty("limit-order-max-buy-amt")]
        public decimal MaxLimitOrderBuyQuantity { get; set; }
        /// <summary>
        /// Max sell order quantity of limit order in base asset
        /// </summary>
        [JsonProperty("limit-order-max-sell-amt")]
        public decimal MaxLimitOrderSellQuantity { get; set; }
        /// <summary>
        /// Max order quantity of limit order in base asset
        /// </summary>
        [JsonProperty("limit-order-max-order-amt")]
        public decimal MaxLimitOrderQuantity { get; set; }
        /// <summary>
        /// Minimum order quantity of sell-market order in base asset
        /// </summary>
        [JsonProperty("sell-market-min-order-amt")]
        public decimal MinMarketSellOrderQuantity { get; set; }
        /// <summary>
        /// Max order quantity of sell-market order in base asset
        /// </summary>
        [JsonProperty("sell-market-max-order-amt")]
        public decimal MaxMarketSellOrderQuantity { get; set; }
        /// <summary>
        /// Max order value of buy-market order in quote asset
        /// </summary>
        [JsonProperty("buy-market-max-order-value")]
        public decimal MaxMarketBuyOrderValue { get; set; }
        /// <summary>
        /// Minimum value of the order quantity in quote asset
        /// </summary>
        [JsonProperty("min-order-value")]
        public decimal MinOrderValue { get; set; }
        /// <summary>
        /// Max order value of limit order and buy-market order in usdt
        /// </summary>
        [JsonProperty("max-order-value")]
        public decimal MaxOrderValue { get; set; }
        /// <summary>
        /// The precision of the order quantity in quote asset
        /// </summary>
        [JsonProperty("value-precision")]
        public int ValuePrecision { get; set; }
        /// <summary>
        /// Api trading status, enabled or disabled
        /// </summary>
        [JsonProperty("api-trading")]
        public string ApiTrading { get; set; } = string.Empty;
    }
}
