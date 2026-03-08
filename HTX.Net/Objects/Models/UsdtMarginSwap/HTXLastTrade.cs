using HTX.Net.Enums;

namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    [SerializationModel]
    internal record HTXLastTradeWrapper
    {
        [JsonPropertyName("data")]
        public HTXLastTrade[] Data { get; set; } = Array.Empty<HTXLastTrade>();
    }

    [SerializationModel]
    internal record HTXTradeWrapper
    {
        [JsonPropertyName("data")]
        public HTXTrade[] Data { get; set; } = Array.Empty<HTXTrade>();
    }

    /// <summary>
    /// Last trade data
    /// </summary>
    [SerializationModel]
    public record HTXTrade
    {
        /// <summary>
        /// ["<c>amount</c>"] Amount of contracts
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Amount { get; set; }
        /// <summary>
        /// ["<c>quantity</c>"] Quantity
        /// </summary>
        [JsonPropertyName("quantity")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>price</c>"] Price
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// ["<c>ts</c>"] Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("ts")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>id</c>"] Id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// ["<c>direction</c>"] Direction
        /// </summary>
        [JsonPropertyName("direction")]
        public OrderSide Direction { get; set; }
        /// <summary>
        /// ["<c>trade_turnover</c>"] Value
        /// </summary>
        [JsonPropertyName("trade_turnover")]
        public decimal QuoteQuantity { get; set; }
    }

    /// <summary>
    /// Last trade info
    /// </summary>
    [SerializationModel]
    public record HTXLastTrade: HTXTrade
    {
        /// <summary>
        /// ["<c>contract_code</c>"] Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = String.Empty;
        /// <summary>
        /// ["<c>business_type</c>"] Business type
        /// </summary>

        [JsonPropertyName("business_type")]
        public BusinessType BusinessType { get; set; }
    }
}
