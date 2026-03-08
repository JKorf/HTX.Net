namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Deduction info
    /// </summary>
    [SerializationModel]
    public record HTXDeductInfo
    {
        /// <summary>
        /// ["<c>pointSwitch</c>"] Point switch
        /// </summary>
        [JsonPropertyName("pointSwitch")]
        public bool PointSwitch { get; set; }
        /// <summary>
        /// ["<c>htxSwitch</c>"] Htx switch
        /// </summary>
        [JsonPropertyName("htxSwitch")]
        public bool HtxSwitch { get; set; }
        /// <summary>
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
    }


}
