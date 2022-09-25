using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Huobi.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Batch result
    /// </summary>
    public class HuobiBatchResult
    {
        /// <summary>
        /// Errors in the batch
        /// </summary>
        public IEnumerable<HuobiBatchError> Errors { get; set; } = new List<HuobiBatchError>();
        /// <summary>
        /// Success
        /// </summary>
        public string Successes { get; set; } = string.Empty;
    }

    /// <summary>
    /// Batch operation error
    /// </summary>
    public class HuobiBatchError
    {
        /// <summary>
        /// Order id
        /// </summary>
        [JsonProperty("order_id")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// Error code
        /// </summary>
        [JsonProperty("err_code")]
        public int ErrorCode { get; set; }
        /// <summary>
        /// Error message
        /// </summary>
        [JsonProperty("err_msg")]
        public string ErrorMessage { get; set; } = string.Empty;
    }
}
