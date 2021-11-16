using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models
{
    /// <summary>
    /// Deposit address info
    /// </summary>
    public class HuobiDepositAddress 
    {
        /// <summary>
        /// User id
        /// </summary>
        public long UserId { get; set; }
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Deposit address
        /// </summary>
        public string Address { get; set; } = string.Empty;
        /// <summary>
        /// Deposit address tag
        /// </summary>
        public string AddressTag { get; set; } = string.Empty;
        /// <summary>
        /// Block chain name
        /// </summary>
        [JsonProperty("chain")]
        public string Network { get; set; } = string.Empty;
    }
}
