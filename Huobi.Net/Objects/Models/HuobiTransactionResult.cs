using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models
{
    /// <summary>
    /// Transaction result
    /// </summary>
    public class HuobiTransactionResult
    {
        /// <summary>
        /// Id
        /// </summary>
        [JsonProperty("transact-id")]
        public long TransactionId { get; set; }
        /// <summary>
        /// Time
        /// </summary>
        [JsonProperty("transact-time")]
        public long TransactionTime { get; set; }
    }
}
