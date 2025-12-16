namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// 
    /// </summary>
    [SerializationModel]
    public record HTXDeductionAssets
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Assets { get; set; } = string.Empty;
    }
}
