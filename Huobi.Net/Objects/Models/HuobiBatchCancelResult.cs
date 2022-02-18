using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models
{
    /// <summary>
    /// Result of a batch cancel
    /// </summary>
    public class HuobiBatchCancelResult
    {
        /// <summary>
        /// Orders that were successfully canceled
        /// </summary>
        [JsonProperty("success")]
        public IEnumerable<long> Successful { get; set; } = Array.Empty<long>();
        /// <summary>
        /// Orders that failed to cancel
        /// </summary>
        public IEnumerable<HuobiFailedCancelResult> Failed { get; set; } = Array.Empty<HuobiFailedCancelResult>();
    }

    /// <summary>
    /// Cancel result
    /// </summary>
    public class HuobiFailedCancelResult
    {
        /// <summary>
        /// The error code
        /// </summary>
        [JsonProperty("err-code")]
        public string? ErrorCode { get; set; }
        /// <summary>
        /// The error message
        /// </summary>
        [JsonProperty("err-msg")]
        public string? ErrorMessage { get; set; }
        /// <summary>
        /// The id of the failed order
        /// </summary>
        [JsonProperty("order-id")]
        public long OrderId { get; set; }
        /// <summary>
        /// The state of the order
        /// </summary>
        [JsonProperty("order-state")]
        public string? OrderState { get; set; }
        /// <summary>
        /// The id of the failed order
        /// </summary>
        [JsonProperty("client-order-id")]
        public string? ClientOrderId { get; set; }
    }
}
