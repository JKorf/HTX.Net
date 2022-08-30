using CryptoExchange.Net.Converters;
using Newtonsoft.Json;
using System;

namespace Huobi.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Funding rate
    /// </summary>
    public class HuobiFundingRate
    {
        /// <summary>
        /// Estimated rate
        /// </summary>
        [JsonProperty("estimated_rate")]
        public decimal? EstimatedRate { get; set; }
        /// <summary>
        /// Funding rate
        /// </summary>
        [JsonProperty("funding_rate")]
        public decimal? FundingRate { get; set; }
        /// <summary>
        /// Contract code
        /// </summary>
        [JsonProperty("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonProperty("symbol")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Fee asset
        /// </summary>
        [JsonProperty("fee_asset")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// Funding rate
        /// </summary>
        [JsonProperty("funding_time")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime FundingTime { get; set; }
        /// <summary>
        /// Next funding time
        /// </summary>
        [JsonProperty("next_funding_time")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime NextFundingTime { get; set; }
    }
}
