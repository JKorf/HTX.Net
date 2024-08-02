namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Symbol configuration
    /// </summary>
    public record HTXSymbolConfig
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("state")]
        public string Status { get; set; } = string.Empty;
        /// <summary>
        /// Base asset
        /// </summary>
        [JsonPropertyName("bc")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// Quote asset
        /// </summary>
        [JsonPropertyName("qc")]
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// Price precision
        /// </summary>
        [JsonPropertyName("pp")]
        public int PricePrecision { get; set; }
        /// <summary>
        /// Quantity precision
        /// </summary>
        [JsonPropertyName("ap")]
        public int QuantityPrecision { get; set; }
        /// <summary>
        /// Partition
        /// </summary>
        [JsonPropertyName("sp")]
        public string Partition { get; set; } = string.Empty;
        /// <summary>
        /// Value precision
        /// </summary>
        [JsonPropertyName("vp")]
        public int ValuePrecision { get; set; }
        /// <summary>
        /// Minimal order quantity
        /// </summary>
        [JsonPropertyName("minoa")]
        public decimal? MinOrderQuantity { get; set; }
        /// <summary>
        /// Max order quantity
        /// </summary>
        [JsonPropertyName("maxoa")]
        public decimal? MaxOrderQuantity { get; set; }
        /// <summary>
        /// Minimal order value
        /// </summary>
        [JsonPropertyName("minov")]
        public decimal? MinOrderValue { get; set; }
        /// <summary>
        /// Minimal quantity of limit order
        /// </summary>
        [JsonPropertyName("lominoa")]
        public decimal? MinQuantityLimitOrder { get; set; }
        /// <summary>
        /// Max quantity of limit order
        /// </summary>
        [JsonPropertyName("lomaxoa")]
        public decimal? MaxQuantityLimitOrder { get; set; }
        /// <summary>
        /// Max quantity of limit buy order
        /// </summary>
        [JsonPropertyName("lomaxba")]
        public decimal? MaxQuantityLimitBuyOrder { get; set; }
        /// <summary>
        /// Max quantity of limit sell order
        /// </summary>
        [JsonPropertyName("lomaxsa")]
        public decimal? MaxQuantityLimitSellOrder { get; set; }
        /// <summary>
        /// Minimal quantity of market sell order
        /// </summary>
        [JsonPropertyName("smminoa")]
        public decimal? MinQuantityMarketSellOrder { get; set; }
        /// <summary>
        /// Buy limit order must be less than this
        /// </summary>
        [JsonPropertyName("blmlt")]
        public decimal? BuyLimitLessThan { get; set; }
        /// <summary>
        /// Sell limit order must be less than this
        /// </summary>
        [JsonPropertyName("slmgt")]
        public decimal? SellLimitLessThan { get; set; }
        /// <summary>
        /// Max quantity of market sell order
        /// </summary>
        [JsonPropertyName("smmaxoa")]
        public decimal? MaxQuantityMarketSellOrder { get; set; }
        /// <summary>
        /// Max quantity of market buy order
        /// </summary>
        [JsonPropertyName("bmmaxov")]
        public decimal? MaxQuantityMarketBuyOrder { get; set; }
        /// <summary>
        /// Sell market order rate must be less than this
        /// </summary>
        [JsonPropertyName("msormlt")]
        public decimal? SellMarketLessThan { get; set; }
        /// <summary>
        /// Buy market order rate must be less than this
        /// </summary>
        [JsonPropertyName("mbormlt")]
        public decimal? BuyMarketLessThan { get; set; }
        /// <summary>
        /// Max value of a market order
        /// </summary>
        [JsonPropertyName("maxov")]
        public decimal? MaxValueMarketOrder { get; set; }
        /// <summary>
        /// Etp symbol
        /// </summary>
        [JsonPropertyName("u")]
        public string? EtpSymbol { get; set; } = string.Empty;
        /// <summary>
        /// Mfr
        /// </summary>
        [JsonPropertyName("mfr")]
        public decimal? Mfr { get; set; }
        /// <summary>
        /// ETP charge time
        /// </summary>
        [JsonPropertyName("ct")]
        public string? ChareTime { get; set; }
        /// <summary>
        /// ETP rebal time
        /// </summary>
        [JsonPropertyName("rt")]
        public string? RebalTime { get; set; }
        /// <summary>
        /// ETP rebal threshold
        /// </summary>
        [JsonPropertyName("rthr")]
        public decimal? RebalThreshold { get; set; }
        /// <summary>
        /// ETP initial NAV
        /// </summary>
        [JsonPropertyName("in")]
        public decimal InitialNav { get; set; }
        /// <summary>
        /// ApiTrading
        /// </summary>
        [JsonPropertyName("at")]
        public string ApiTrading { get; set; } = string.Empty;
        /// <summary>
        /// Tags
        /// </summary>
        [JsonPropertyName("tags")]
        public string Tags { get; set; } = string.Empty;
    }


}
