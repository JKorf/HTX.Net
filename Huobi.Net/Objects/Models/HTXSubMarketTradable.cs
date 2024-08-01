using HTX.Net.Enums;

namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Sub account tradable market status
    /// </summary>
    public record HTXSubMarketTradable
    {
        /// <summary>
        /// Sub user id
        /// </summary>
        [JsonPropertyName("subUid")]
        public long SubUserId { get; set; }
        /// <summary>
        /// Account type
        /// </summary>
        [JsonPropertyName("accountType")]
        public SubAccountMarketType AccountType { get; set; }
        /// <summary>
        /// Activation
        /// </summary>
        [JsonPropertyName("activation")]
        public string Activation { get; set; } = string.Empty;
    }


}
