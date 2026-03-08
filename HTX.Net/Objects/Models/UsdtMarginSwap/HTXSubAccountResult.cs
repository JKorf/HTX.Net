namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// sub account results
    /// </summary>
    [SerializationModel]
    public record HTXSubAccountResult
    {
        /// <summary>
        /// ["<c>successes</c>"] Successfully updated ids
        /// </summary>
        [JsonPropertyName("successes")]
        public string Successes { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>errors</c>"] Errors
        /// </summary>
        [JsonPropertyName("errors")]
        public HTXSubAccountError[] Errors { get; set; } = Array.Empty<HTXSubAccountError>();
    }

    /// <summary>
    /// Sub account error info
    /// </summary>
    [SerializationModel]
    public record HTXSubAccountError
    {
        /// <summary>
        /// ["<c>sub_uid</c>"] Sub uid
        /// </summary>
        [JsonPropertyName("sub_uid")]
        public string SubUid { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>err_code</c>"] Error code
        /// </summary>
        [JsonPropertyName("err_code")]
        public int ErrorCode { get; set; }
        /// <summary>
        /// ["<c>err_msg</c>"] Error message
        /// </summary>
        [JsonPropertyName("err_msg")]
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
