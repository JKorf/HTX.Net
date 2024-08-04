using HTX.Net.Enums;

namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    internal record HTXLastTradeWrapper
    {
        [JsonPropertyName("data")]
        public HTXLastTrade[] Data { get; set; } = Array.Empty<HTXLastTrade>();
    }

    internal record HTXTradeWrapper
    {
        [JsonPropertyName("data")]
        public HTXTrade[] Data { get; set; } = Array.Empty<HTXTrade>();
    }

    /// <summary>
    /// Last trade data
    /// </summary>
    public record HTXTrade
    {
        /// <summary>
        /// Amount of contracts
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }
        /// <summary>
        /// Quantity
        /// </summary>
        [JsonPropertyName("quantity")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("ts")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// Direction
        /// </summary>
        [JsonPropertyName("direction")]
        public OrderSide Direction { get; set; }
        /// <summary>
        /// Value
        /// </summary>
        [JsonPropertyName("trade_turnover")]
        public decimal QuoteQuantity { get; set; }
    }

    /// <summary>
    /// Last trade info
    /// </summary>
    public record HTXLastTrade: HTXTrade
    {
        /// <summary>
        /// Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = String.Empty;
        /// <summary>
        /// Business type
        /// </summary>
        [JsonConverter(typeof(EnumConverter))]
        [JsonPropertyName("business_type")]
        public BusinessType BusinessType { get; set; }
    }
}
