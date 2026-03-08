namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Trade permissions result
    /// </summary>
    [SerializationModel]
    public record HTXSubTradePermissions
    {
        /// <summary>
        /// ["<c>errors</c>"] Errors
        /// </summary>
        [JsonPropertyName("errors")]
        public HTXSubTradePermissionsError[] Errors { get; set; } = Array.Empty<HTXSubTradePermissionsError>();
        /// <summary>
        /// ["<c>successes</c>"] Successes
        /// </summary>
        [JsonPropertyName("successes")]
        public HTXSubTradePermissionsStatus[] Successes { get; set; } = Array.Empty<HTXSubTradePermissionsStatus>();
    }

    /// <summary>
    /// Error
    /// </summary>
    [SerializationModel]
    public record HTXSubTradePermissionsError
    {
        /// <summary>
        /// ["<c>sub_uid</c>"] Sub user id
        /// </summary>
        [JsonPropertyName("sub_uid")]
        public decimal SubUserId { get; set; }
        /// <summary>
        /// ["<c>err_code</c>"] Error code
        /// </summary>
        [JsonPropertyName("err_code")]
        public decimal ErrorCode { get; set; }
        /// <summary>
        /// ["<c>err_msg</c>"] Error msg
        /// </summary>
        [JsonPropertyName("err_msg")]
        public string ErrorMsg { get; set; } = string.Empty;
    }

    /// <summary>
    /// Status
    /// </summary>
    [SerializationModel]
    public record HTXSubTradePermissionsStatus
    {
        /// <summary>
        /// ["<c>query_id</c>"] Query id
        /// </summary>
        [JsonPropertyName("query_id")]
        public long QueryId { get; set; }
        /// <summary>
        /// ["<c>sub_uid</c>"] Sub uid
        /// </summary>
        [JsonPropertyName("sub_uid")]
        public string SubUid { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>sub_auth</c>"] Sub auth
        /// </summary>
        [JsonPropertyName("sub_auth")]
        public bool SubAuth { get; set; }
    }


}
