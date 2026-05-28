using HTX.Net.Enums;

namespace HTX.Net.Objects.Models.UsdtFuturesV5
{
    /// <summary>
    /// Position update
    /// </summary>
    [SerializationModel]
    public record HTXPositionUpdateV5 : HTXPositionV5
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>position_mode</c>"] Position mode
        /// </summary>
        [JsonPropertyName("position_mode")]
        public PositionMode PositionMode { get; set; }
        /// <summary>
        /// ["<c>fee</c>"] Fee
        /// </summary>
        [JsonPropertyName("fee")]
        public decimal Fee { get; set; }
        /// <summary>
        /// ["<c>profit</c>"] Profit
        /// </summary>
        [JsonPropertyName("profit")]
        public decimal Profit { get; set; }
        /// <summary>
        /// ["<c>state</c>"] State
        /// </summary>
        [JsonPropertyName("state")]
        public string State { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>funding_fee</c>"] Funding fee
        /// </summary>
        [JsonPropertyName("funding_fee")]
        public decimal FundingFee { get; set; }
        /// <summary>
        /// ["<c>mark_price</c>"] Mark price
        /// </summary>
        [JsonPropertyName("mark_price")]
        public decimal MarkPrice { get; set; }
        /// <summary>
        /// ["<c>version</c>"] Version
        /// </summary>
        [JsonPropertyName("version")]
        public long Version { get; set; }
        /// <summary>
        /// ["<c>total_trade_fee</c>"] Total trade fee
        /// </summary>
        [JsonPropertyName("total_trade_fee")]
        public decimal TotalTradeFee { get; set; }
        /// <summary>
        /// ["<c>break_even_price</c>"] Break even price
        /// </summary>
        [JsonPropertyName("break_even_price")]
        public decimal? BreakEvenPrice { get; set; }
    }
}
