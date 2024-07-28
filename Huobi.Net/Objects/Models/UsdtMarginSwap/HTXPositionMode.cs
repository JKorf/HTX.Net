using CryptoExchange.Net.Converters;
using HTX.Net.Enums;


namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Position mode
    /// </summary>
    public record HTXPositionMode
    {
        /// <summary>
        /// Margin account
        /// </summary>
        [JsonPropertyName("margin_account")]
        public string MarginAccount { get; set; } = string.Empty;
        /// <summary>
        /// Position mode
        /// </summary>
        [JsonPropertyName("position_mode")]
        [JsonConverter(typeof(EnumConverter))]
        public PositionMode PositionMode { get; set; }
    }
}
