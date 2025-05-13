using CryptoExchange.Net.Converters.SystemTextJson;
using HTX.Net.Enums;

namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// HTX aggregated sub account balance
    /// </summary>
    [SerializationModel]
    public record HTXAggBalance
    {
        /// <summary>
        /// The asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// The type of the balance
        /// </summary>

        [JsonPropertyName("type")]
        public AccountType Type { get; set; }
        /// <summary>
        /// The balance
        /// </summary>
        [JsonPropertyName("balance")]
        public decimal Balance { get; set; }
    }
}
