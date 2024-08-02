﻿namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// sub account results
    /// </summary>
    public record HTXSubAccountResult
    {
        /// <summary>
        /// Successfully updated ids
        /// </summary>
        [JsonPropertyName("successes")]
        public string Successes { get; set; } = string.Empty;
        /// <summary>
        /// Errors
        /// </summary>
        [JsonPropertyName("errors")]
        public IEnumerable<HTXSubAccountError> Errors { get; set; } = Array.Empty<HTXSubAccountError>();
    }

    /// <summary>
    /// Sub account error info
    /// </summary>
    public record HTXSubAccountError
    {
        /// <summary>
        /// Sub uid
        /// </summary>
        [JsonPropertyName("sub_uid")]
        public string SubUid { get; set; } = string.Empty;
        /// <summary>
        /// Error code
        /// </summary>
        [JsonPropertyName("err_code")]
        public string ErrorCode { get; set; } = string.Empty;
        /// <summary>
        /// Error message
        /// </summary>
        [JsonPropertyName("err_msg")]
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
