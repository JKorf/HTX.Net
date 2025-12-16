namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Asset valuation
    /// </summary>
    [SerializationModel]
    public  record HTXAssetValue
    {
        /// <summary>
        /// Asset name
        /// </summary>
        [JsonPropertyName("valuation_asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Balance
        /// </summary>
        [JsonPropertyName("balance")]
        public decimal Balance { get; set; }
    }
}
