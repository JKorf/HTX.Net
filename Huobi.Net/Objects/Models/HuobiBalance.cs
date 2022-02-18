using Huobi.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models
{
    /// <summary>
    /// Balance data
    /// </summary>
    public class HuobiBalance
    {
        /// <summary>
        /// The asset
        /// </summary>
        [JsonProperty("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// The type of the balance
        /// </summary>
        [JsonConverter(typeof(BalanceTypeConverter))]
        public BalanceType Type { get; set; }
        /// <summary>
        /// The balance
        /// </summary>
        public decimal Balance { get; set; }
        /// <summary>
        /// Sequence number of the update
        /// </summary>
        [JsonProperty("seq-num")]
        public string SequenceNumber { get; set; } = string.Empty;
    }
}
