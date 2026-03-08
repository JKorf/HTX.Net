namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Asset valuation
    /// </summary>
    [SerializationModel]
    public  record HTXAssetValue
    {
        /// <summary>
        /// ["<c>valuation_asset</c>"] Asset name
        /// </summary>
        [JsonPropertyName("valuation_asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>balance</c>"] Balance
        /// </summary>
        [JsonPropertyName("balance")]
        public decimal Balance { get; set; }
    }
}
