using CryptoExchange.Net.Converters.SystemTextJson;

namespace HTX.Net.Objects.Models.Socket
{
    /// <summary>
    /// Basis update
    /// </summary>
    [SerializationModel]
    public record HTXUsdtMarginSwapBasisUpdate
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// Index price
        /// </summary>
        [JsonPropertyName("index_price")]
        public decimal IndexPrice { get; set; }
        /// <summary>
        /// Contract price
        /// </summary>
        [JsonPropertyName("contract_price")]
        public decimal ContractPrice { get; set; }
        /// <summary>
        /// Basis
        /// </summary>
        [JsonPropertyName("basis")]
        public decimal Basis { get; set; }
        /// <summary>
        /// Basis rate
        /// </summary>
        [JsonPropertyName("basis_rate")]
        public decimal BasisRate { get; set; }
    }
}
