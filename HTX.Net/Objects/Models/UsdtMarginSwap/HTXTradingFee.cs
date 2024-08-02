using HTX.Net.Enums;


namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Trading fee info
    /// </summary>
    public record HTXTradingFee
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
        /// Open position maker fee
        /// </summary>
        [JsonPropertyName("open_maker_fee")]
        public decimal OpenMakerFee { get; set; }
        /// <summary>
        /// Open position taker fee
        /// </summary>
        [JsonPropertyName("open_taker_fee")]
        public decimal OpenTakerFee { get; set; }
        /// <summary>
        /// Close position maker fee
        /// </summary>
        [JsonPropertyName("close_maker_fee")]
        public decimal CloseMakerfee { get; set; }
        /// <summary>
        /// Close position taker fee
        /// </summary>
        [JsonPropertyName("close_taker_fee")]
        public decimal CloseTakerFee { get; set; }
        /// <summary>
        /// Fee asset
        /// </summary>
        [JsonPropertyName("fee_asset")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// Delivery fee
        /// </summary>
        [JsonPropertyName("delivery_fee")]
        public decimal DeliveryFee { get; set; }
        /// <summary>
        /// Business type
        /// </summary>
        [JsonPropertyName("business_type")]
        [JsonConverter(typeof(EnumConverter))]
        public BusinessType BusinessType { get; set; }
        /// <summary>
        /// Contract type
        /// </summary>
        [JsonPropertyName("contract_type")]
        [JsonConverter(typeof(EnumConverter))]
        public ContractType ContractType { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("pair")]
        public string Symbol { get; set; } = string.Empty;
    }
}
