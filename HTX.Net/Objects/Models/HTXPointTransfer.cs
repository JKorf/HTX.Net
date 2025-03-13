using CryptoExchange.Net.Converters.SystemTextJson;
namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Transfer result
    /// </summary>
    [SerializationModel]
    public record HTXPointTransfer
    {
        /// <summary>
        /// Transact id
        /// </summary>
        [JsonPropertyName("transactId")]
        public string TransactId { get; set; } = string.Empty;
        /// <summary>
        /// Transact time
        /// </summary>
        [JsonPropertyName("transactTime")]
        public DateTime? TransactTime { get; set; }
    }


}
