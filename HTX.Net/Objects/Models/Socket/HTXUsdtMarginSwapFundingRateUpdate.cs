using HTX.Net.Objects.Sockets;

namespace HTX.Net.Objects.Models.Socket
{
    /// <summary>
    /// 
    /// </summary>
    [SerializationModel]
    internal record HTXUsdtMarginSwapFundingRateUpdateWrapper : HTXOpMessage
    {
        /// <summary>
        /// ["<c>ts</c>"] Timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>data</c>"] Data
        /// </summary>
        [JsonPropertyName("data")]
        public HTXUsdtMarginSwapFundingRateUpdate[] Data { get; set; } = Array.Empty<HTXUsdtMarginSwapFundingRateUpdate>();
    }

    /// <summary>
    /// Funding rate update
    /// </summary>
    [SerializationModel]
    public record HTXUsdtMarginSwapFundingRateUpdate
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>contract_code</c>"] Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>fee_asset</c>"] Fee asset
        /// </summary>
        [JsonPropertyName("fee_asset")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>funding_time</c>"] Funding time
        /// </summary>
        [JsonPropertyName("funding_time")]
        public DateTime FundingTime { get; set; }
        /// <summary>
        /// ["<c>funding_rate</c>"] Funding rate
        /// </summary>
        [JsonPropertyName("funding_rate")]
        public decimal FundingRate { get; set; }
        /// <summary>
        /// ["<c>settlement_time</c>"] Settlement time
        /// </summary>
        [JsonPropertyName("settlement_time")]
        public DateTime SettlementTime { get; set; }
        /// <summary>
        /// ["<c>estimated_rate</c>"] Estimated rate
        /// </summary>
        [JsonPropertyName("estimated_rate")]
        public decimal? EstimatedRate { get; set; }
    }


}
