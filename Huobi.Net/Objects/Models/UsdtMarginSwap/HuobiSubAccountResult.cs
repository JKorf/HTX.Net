using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Huobi.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// sub account results
    /// </summary>
    public class HuobiSubAccountResult
    {
        /// <summary>
        /// Successfully updated ids
        /// </summary>
        public string Successes { get; set; } = string.Empty;
        /// <summary>
        /// Errors
        /// </summary>
        public IEnumerable<HuobiSubAccountError> Errors { get; set; } = Array.Empty<HuobiSubAccountError>();
    }

    /// <summary>
    /// Sub account error info
    /// </summary>
    public class HuobiSubAccountError
    {
        /// <summary>
        /// Sub uid
        /// </summary>
        [JsonProperty("sub_uid")]
        public string SubUid { get; set; } = string.Empty;
        /// <summary>
        /// Error code
        /// </summary>
        [JsonProperty("err_code")]
        public string ErrorCode { get; set; } = string.Empty;
        /// <summary>
        /// Error message
        /// </summary>
        [JsonProperty("err_msg")]
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
