using HTX.Net.Enums;

namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Order limit info
    /// </summary>
    [SerializationModel]
    public record HTXOrderLimit
    {
        /// <summary>
        /// Order price type
        /// </summary>
        [JsonPropertyName("order_price_type")]
        public OrderPriceType? OrderType { get; set; }
        /// <summary>
        /// Limits
        /// </summary>
        [JsonPropertyName("list")]
        public HTXOrderTypeLimit[] Limits { get; set; } = Array.Empty<HTXOrderTypeLimit>();
    }

    /// <summary>
    /// Limit info
    /// </summary>
    [SerializationModel]
    public record HTXOrderTypeLimit
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// Max open order limit
        /// </summary>
        [JsonPropertyName("open_limit")]
        public decimal? OpenLimit { get; set; }
        /// <summary>
        /// Max close order limit
        /// </summary>
        [JsonPropertyName("close_limit")]
        public decimal? CloseLimit { get; set; }
        /// <summary>
        /// Business type
        /// </summary>
        [JsonPropertyName("business_type")]
        public BusinessType? BusinessType { get; set; }
        /// <summary>
        /// Contract type
        /// </summary>
        [JsonPropertyName("contract_type")]
        public ContractType? ContractType { get; set; }
        /// <summary>
        /// Pair
        /// </summary>
        [JsonPropertyName("pair")]
        public string Pair { get; set; } = string.Empty;
    }


}
