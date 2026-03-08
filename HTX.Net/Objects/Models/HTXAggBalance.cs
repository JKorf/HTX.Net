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
        /// ["<c>currency</c>"] The asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>type</c>"] The type of the balance
        /// </summary>

        [JsonPropertyName("type")]
        public AccountType Type { get; set; }
        /// <summary>
        /// ["<c>balance</c>"] The balance
        /// </summary>
        [JsonPropertyName("balance")]
        public decimal Balance { get; set; }
    }
}
