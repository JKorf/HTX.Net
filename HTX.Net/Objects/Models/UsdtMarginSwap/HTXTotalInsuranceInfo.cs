namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Insurance value
    /// </summary>
    [SerializationModel]
    public record HTXTotalInsuranceInfo
    {
        /// <summary>
        /// ["<c>insurance_fund</c>"] Insurance fund
        /// </summary>
        [JsonPropertyName("insurance_fund")]
        public decimal InsuranceFund { get; set; }
        /// <summary>
        /// ["<c>quote_currency</c>"] Quote asset
        /// </summary>
        [JsonPropertyName("quote_currency")]
        public string Asset { get; set; } = string.Empty;
    }
}
