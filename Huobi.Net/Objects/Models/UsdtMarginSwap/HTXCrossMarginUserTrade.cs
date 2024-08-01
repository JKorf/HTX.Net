using HTX.Net.Enums;

namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Margin user trade page
    /// </summary>
    public record HTXCrossMarginUserTradePage : HTXPage
    {
        /// <summary>
        /// Trades
        /// </summary>
        [JsonPropertyName("trades")]
        public IEnumerable<HTXCrossMarginUserTrade> Trades { get; set; } = Array.Empty<HTXCrossMarginUserTrade>();
    }

    /// <summary>
    /// User trade info
    /// </summary>
    public record HTXCrossMarginUserTrade : HTXIsolatedMarginUserTrade
    {
        /// <summary>
        /// Contract type
        /// </summary>
        [JsonPropertyName("contract_type")]
        [JsonConverter(typeof(EnumConverter))]
        public ContractType ContractType { get; set; }
        /// <summary>
        /// Business type
        /// </summary>
        [JsonPropertyName("business_type")]
        [JsonConverter(typeof(EnumConverter))]
        public BusinessType BusinessType { get; set; }
        /// <summary>
        /// Pair
        /// </summary>
        [JsonPropertyName("pair")]
        public string Pair { get; set; } = string.Empty;
    }
}
