using Newtonsoft.Json;

namespace Huobi.Net.Objects
{
    public class HuobiBatchCancelResult
    {
        /// <summary>
        /// Orders that were successfully canceled
        /// </summary>
        [JsonProperty("success")]
        public long[] Successful { get; set; }
        /// <summary>
        /// Orders that failed to cancel
        /// </summary>
        public HuobiFailedCancelResult[] Failed { get; set; }
    }

    public class HuobiFailedCancelResult
    {
        /// <summary>
        /// The error code
        /// </summary>
        [JsonProperty("err-code")]
        public string ErrorCode { get; set; }
        /// <summary>
        /// The error message
        /// </summary>
        [JsonProperty("err-msg")]
        public string ErrorMessage { get; set; }
        /// <summary>
        /// The id of the failed order
        /// </summary>
        [JsonProperty("order-id")]
        public long OrderId { get; set; }
    }
}
