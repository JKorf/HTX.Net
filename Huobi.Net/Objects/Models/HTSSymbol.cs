using HTX.Net.Enums;

namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Symbol data
    /// </summary>
    public record HTXSymbol
    {
        /// <summary>
        /// The symbol name
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// The base asset
        /// </summary>
        [JsonPropertyName("base-currency")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// The quote asset
        /// </summary>
        [JsonPropertyName("quote-currency")]
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// The precision of the price in decimal numbers
        /// </summary>
        [JsonPropertyName("price-precision")]
        public int PricePrecision { get; set; }
        /// <summary>
        /// The precision of the quantity in decimal numbers
        /// </summary>
        [JsonPropertyName("amount-precision")]
        public int QuantityPrecision { get; set; }
        /// <summary>
        /// Partition
        /// </summary>
        [JsonPropertyName("symbol-partition")]
        public string SymbolPartition { get; set; } = string.Empty;
        /// <summary>
        /// The state of the symbol
        /// </summary>
        [JsonPropertyName("state"), JsonConverter(typeof(EnumConverter))]
        public SymbolState State { get; set; }
        /// <summary>
        /// Minimum value of the quantity
        /// </summary>
        [Obsolete("Use MinLimitOrderQuantity instead")]
        [JsonPropertyName("min-order-amt")]
        public decimal MinOrderQuantity { get; set; }
        /// <summary>
        /// Maximum value of the quantity
        /// </summary>
        [Obsolete("Use MaxLimitOrderQuantity instead")]
        [JsonPropertyName("max-order-amt")]
        public decimal MaxOrderQuantity { get; set; }
        /// <summary>
        /// Minimum order quantity of limit order in base asset
        /// </summary>
        [JsonPropertyName("limit-order-min-order-amt")]
        public decimal MinLimitOrderQuantity { get; set; }
        /// <summary>
        /// Max buy order quantity of limit order in base asset
        /// </summary>
        [JsonPropertyName("limit-order-max-buy-amt")]
        public decimal MaxLimitOrderBuyQuantity { get; set; }
        /// <summary>
        /// Max sell order quantity of limit order in base asset
        /// </summary>
        [JsonPropertyName("limit-order-max-sell-amt")]
        public decimal MaxLimitOrderSellQuantity { get; set; }
        /// <summary>
        /// Max order quantity of limit order in base asset
        /// </summary>
        [JsonPropertyName("limit-order-max-order-amt")]
        public decimal MaxLimitOrderQuantity { get; set; }
        /// <summary>
        /// Minimum order quantity of sell-market order in base asset
        /// </summary>
        [JsonPropertyName("sell-market-min-order-amt")]
        public decimal MinMarketSellOrderQuantity { get; set; }
        /// <summary>
        /// Max order quantity of sell-market order in base asset
        /// </summary>
        [JsonPropertyName("sell-market-max-order-amt")]
        public decimal MaxMarketSellOrderQuantity { get; set; }
        /// <summary>
        /// Max order value of buy-market order in quote asset
        /// </summary>
        [JsonPropertyName("buy-market-max-order-value")]
        public decimal MaxMarketBuyOrderValue { get; set; }
        /// <summary>
        /// Minimum value of the order quantity in quote asset
        /// </summary>
        [JsonPropertyName("min-order-value")]
        public decimal MinOrderValue { get; set; }
        /// <summary>
        /// Max order value of limit order and buy-market order in usdt
        /// </summary>
        [JsonPropertyName("max-order-value")]
        public decimal MaxOrderValue { get; set; }
        /// <summary>
        /// The precision of the order quantity in quote asset
        /// </summary>
        [JsonPropertyName("value-precision")]
        public int ValuePrecision { get; set; }
        /// <summary>
        /// Api trading status, enabled or disabled
        /// </summary>
        [JsonPropertyName("api-trading")]
        public string ApiTrading { get; set; } = string.Empty;
    }
}
