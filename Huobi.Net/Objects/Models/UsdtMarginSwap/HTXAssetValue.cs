

namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Asset valuation
    /// </summary>
    public  record HTXAssetValue
    {
        /// <summary>
        /// Asset name
        /// </summary>
        [JsonPropertyName("valuation_asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Balance
        /// </summary>
        public decimal Balance { get; set; }
    }
}
