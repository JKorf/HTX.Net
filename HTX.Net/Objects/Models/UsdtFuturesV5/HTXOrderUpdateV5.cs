namespace HTX.Net.Objects.Models.UsdtFuturesV5
{
    /// <summary>
    /// Order update
    /// </summary>
    [SerializationModel]
    public record HTXOrderUpdateV5 : HTXOrderV5
    {
        /// <summary>
        /// ["<c>amend_origin_volume</c>"] Amend origin quantity
        /// </summary>
        [JsonPropertyName("amend_origin_volume")]
        public decimal? AmendOriginQuantity { get; set; }
        /// <summary>
        /// ["<c>amend_source</c>"] Amend source
        /// </summary>
        [JsonPropertyName("amend_source")]
        public string? AmendSource { get; set; }
        /// <summary>
        /// ["<c>amend_result</c>"] Amend result
        /// </summary>
        [JsonPropertyName("amend_result")]
        public string? AmendResult { get; set; }
    }
}
