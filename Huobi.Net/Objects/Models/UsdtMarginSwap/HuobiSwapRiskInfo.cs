using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Swap risk info
    /// </summary>
    public class HuobiSwapRiskInfo
    {
        /// <summary>
        /// Contract code
        /// </summary>
        [JsonProperty("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// Insurance fund
        /// </summary>
        [JsonProperty("insurance_fund")]
        public decimal InsuranceFund { get; set; }
        /// <summary>
        /// Estimated clawback
        /// </summary>
        [JsonProperty("estimated_clawback")]
        public decimal EstimatedClawback { get; set; }
        /// <summary>
        /// Business type
        /// </summary>
        [JsonProperty("business_type")]
        public BusinessType BusinuessType { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonProperty("pair")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Trade partition
        /// </summary>
        public string TradePartition { get; set; } = string.Empty;
    }
}
