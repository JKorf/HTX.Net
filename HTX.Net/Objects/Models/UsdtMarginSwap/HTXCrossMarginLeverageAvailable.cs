using HTX.Net.Enums;


namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Available leverage info
    /// </summary>
    [SerializationModel]
    public record HTXCrossMarginLeverageAvailable: HTXIsolatedMarginLeverageAvailable
    {
        /// <summary>
        /// ["<c>contract_type</c>"] Contract type
        /// </summary>
        [JsonPropertyName("contract_type")]

        public ContractType ContractType { get; set; }
        /// <summary>
        /// ["<c>pair</c>"] Symbol
        /// </summary>
        [JsonPropertyName("pair")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>business_type</c>"] Business type
        /// </summary>
        [JsonPropertyName("business_type")]

        public BusinessType BusinessType { get; set; }
    }
}
