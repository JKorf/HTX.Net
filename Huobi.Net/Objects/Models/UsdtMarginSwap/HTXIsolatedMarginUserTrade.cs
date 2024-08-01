using HTX.Net.Enums;

namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Margin user trade page
    /// </summary>
    public record HTXIsolatedMarginUserTradePage : HTXPage
    {
        /// <summary>
        /// Trades
        /// </summary>
        [JsonPropertyName("trades")]
        public IEnumerable<HTXIsolatedMarginUserTrade> Trades { get; set; } = Array.Empty<HTXIsolatedMarginUserTrade>();
    }

    /// <summary>
    /// User trade info
    /// </summary>
    public record HTXIsolatedMarginUserTrade : HTXMarginTrade
    {
        /// <summary>
        /// Match id
        /// </summary>
        [JsonPropertyName("match_id")]
        public long MatchId { get; set; }
        /// <summary>
        /// Order id
        /// </summary>
        [JsonPropertyName("order_id")]
        public long OrderId { get; set; }
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// Side
        /// </summary>
        [JsonConverter(typeof(EnumConverter))]
        [JsonPropertyName("side")]
        public OrderSide Side { get; set; }
        /// <summary>
        /// Offset
        /// </summary>
        [JsonConverter(typeof(EnumConverter))]
        [JsonPropertyName("offset")]
        public Offset Offset { get; set; }
        /// <summary>
        /// Offset profit loss
        /// </summary>
        [JsonPropertyName("offset_profitloss")]
        public decimal OffsetProfitloss { get; set; }
        /// <summary>
        /// Reduce only
        /// </summary>
        [JsonPropertyName("reduce_only")]
        public bool ReduceOnly { get; set; }
    }
}
