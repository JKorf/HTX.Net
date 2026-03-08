namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Swap index
    /// </summary>
    [SerializationModel]
    public record HTXSwapIndex
    {
        /// <summary>
        /// ["<c>contract_code</c>"] Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>index_price</c>"] Index price
        /// </summary>
        [JsonPropertyName("index_price")]
        public decimal IndexPrice { get; set; }
        /// <summary>
        /// ["<c>index_ts</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("index_ts")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
    }
}
