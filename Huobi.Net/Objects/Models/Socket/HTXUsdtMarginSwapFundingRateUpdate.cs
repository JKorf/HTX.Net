using HTX.Net.Objects.Sockets;
using System;
using System.Collections.Generic;
using System.Text;

namespace HTX.Net.Objects.Models.Socket
{
    /// <summary>
    /// 
    /// </summary>
    internal record HTXUsdtMarginSwapFundingRateUpdateWrapper : HTXOpMessage
    {
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Data
        /// </summary>
        [JsonPropertyName("data")]
        public IEnumerable<HTXUsdtMarginSwapFundingRateUpdate> Data { get; set; } = Array.Empty<HTXUsdtMarginSwapFundingRateUpdate>();
    }

    /// <summary>
    /// Funding rate update
    /// </summary>
    public record HTXUsdtMarginSwapFundingRateUpdate
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// Fee asset
        /// </summary>
        [JsonPropertyName("fee_asset")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// Funding time
        /// </summary>
        [JsonPropertyName("funding_time")]
        public DateTime FundingTime { get; set; }
        /// <summary>
        /// Funding rate
        /// </summary>
        [JsonPropertyName("funding_rate")]
        public decimal FundingRate { get; set; }
        /// <summary>
        /// Settlement time
        /// </summary>
        [JsonPropertyName("settlement_time")]
        public DateTime SettlementTime { get; set; }
        /// <summary>
        /// Estimated rate
        /// </summary>
        [JsonPropertyName("estimated_rate")]
        public decimal? EstimatedRate { get; set; }
    }


}
