using HTX.Net.Enums;


namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Trading fee info
    /// </summary>
    [SerializationModel]
    public record HTXTradingFee
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
        /// ["<c>open_maker_fee</c>"] Open position maker fee
        /// </summary>
        [JsonPropertyName("open_maker_fee")]
        public decimal OpenMakerFee { get; set; }
        /// <summary>
        /// ["<c>open_taker_fee</c>"] Open position taker fee
        /// </summary>
        [JsonPropertyName("open_taker_fee")]
        public decimal OpenTakerFee { get; set; }
        /// <summary>
        /// ["<c>close_maker_fee</c>"] Close position maker fee
        /// </summary>
        [JsonPropertyName("close_maker_fee")]
        public decimal CloseMakerfee { get; set; }
        /// <summary>
        /// ["<c>close_taker_fee</c>"] Close position taker fee
        /// </summary>
        [JsonPropertyName("close_taker_fee")]
        public decimal CloseTakerFee { get; set; }
        /// <summary>
        /// ["<c>fee_asset</c>"] Fee asset
        /// </summary>
        [JsonPropertyName("fee_asset")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>delivery_fee</c>"] Delivery fee
        /// </summary>
        [JsonPropertyName("delivery_fee")]
        public decimal DeliveryFee { get; set; }
        /// <summary>
        /// ["<c>business_type</c>"] Business type
        /// </summary>
        [JsonPropertyName("business_type")]

        public BusinessType BusinessType { get; set; }
        /// <summary>
        /// ["<c>contract_type</c>"] Contract type
        /// </summary>
        [JsonPropertyName("contract_type")]

        public ContractType ContractType { get; set; }
        /// <summary>
        /// ["<c>pair</c>"] Symbol
        /// </summary>
        [JsonPropertyName("pair")]
        public string Symbol { get; set; } = string.Empty;
    }
}
