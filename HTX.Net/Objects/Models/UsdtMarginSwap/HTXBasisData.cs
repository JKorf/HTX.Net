namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Basis data
    /// </summary>
    [SerializationModel]
    public record HTXBasisData
    {
        /// <summary>
        /// ["<c>basis</c>"] Basis
        /// </summary>
        [JsonPropertyName("basis")]
        public decimal Basis { get; set; }
        /// <summary>
        /// ["<c>basis_rate</c>"] Basis rate
        /// </summary>
        [JsonPropertyName("basis_rate")]
        public decimal BasisRate { get; set; }
        /// <summary>
        /// ["<c>contract_price</c>"] Contract price
        /// </summary>
        [JsonPropertyName("contract_price")]
        public decimal ContractPrice { get; set; }
        /// <summary>
        /// ["<c>index_price</c>"] Index price
        /// </summary>
        [JsonPropertyName("index_price")]
        public decimal IndexPrice { get; set; }
        /// <summary>
        /// ["<c>id</c>"] Unique id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
    }
}
