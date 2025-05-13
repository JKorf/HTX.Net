using CryptoExchange.Net.Converters.SystemTextJson;
namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Trade permissions result
    /// </summary>
    [SerializationModel]
    public record HTXSubTradePermissions
    {
        /// <summary>
        /// Errors
        /// </summary>
        [JsonPropertyName("errors")]
        public HTXSubTradePermissionsError[] Errors { get; set; } = Array.Empty<HTXSubTradePermissionsError>();
        /// <summary>
        /// Successes
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
        /// Sub user id
        /// </summary>
        [JsonPropertyName("sub_uid")]
        public decimal SubUserId { get; set; }
        /// <summary>
        /// Error code
        /// </summary>
        [JsonPropertyName("err_code")]
        public decimal ErrorCode { get; set; }
        /// <summary>
        /// Error msg
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
        /// Query id
        /// </summary>
        [JsonPropertyName("query_id")]
        public long QueryId { get; set; }
        /// <summary>
        /// Sub uid
        /// </summary>
        [JsonPropertyName("sub_uid")]
        public string SubUid { get; set; } = string.Empty;
        /// <summary>
        /// Sub auth
        /// </summary>
        [JsonPropertyName("sub_auth")]
        public bool SubAuth { get; set; }
    }


}
