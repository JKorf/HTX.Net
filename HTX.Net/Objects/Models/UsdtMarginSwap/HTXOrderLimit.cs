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
        /// ["<c>order_price_type</c>"] Order price type
        /// </summary>
        [JsonPropertyName("order_price_type")]
        public OrderPriceType? OrderType { get; set; }
        /// <summary>
        /// ["<c>list</c>"] Limits
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
        /// ["<c>symbol</c>"] Asset
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>contract_code</c>"] Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>open_limit</c>"] Max open order limit
        /// </summary>
        [JsonPropertyName("open_limit")]
        public decimal? OpenLimit { get; set; }
        /// <summary>
        /// ["<c>close_limit</c>"] Max close order limit
        /// </summary>
        [JsonPropertyName("close_limit")]
        public decimal? CloseLimit { get; set; }
        /// <summary>
        /// ["<c>business_type</c>"] Business type
        /// </summary>
        [JsonPropertyName("business_type")]
        public BusinessType? BusinessType { get; set; }
        /// <summary>
        /// ["<c>contract_type</c>"] Contract type
        /// </summary>
        [JsonPropertyName("contract_type")]
        public ContractType? ContractType { get; set; }
        /// <summary>
        /// ["<c>pair</c>"] Pair
        /// </summary>
        [JsonPropertyName("pair")]
        public string Pair { get; set; } = string.Empty;
    }


}
