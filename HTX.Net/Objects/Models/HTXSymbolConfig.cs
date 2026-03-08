using HTX.Net.Enums;

namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Symbol configuration
    /// </summary>
    [SerializationModel]
    public record HTXSymbolConfig
    {
        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>state</c>"] Status
        /// </summary>
        [JsonPropertyName("state")]
        public SymbolStatus Status { get; set; }
        /// <summary>
        /// ["<c>bc</c>"] Base asset
        /// </summary>
        [JsonPropertyName("bc")]
        public string BaseAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>qc</c>"] Quote asset
        /// </summary>
        [JsonPropertyName("qc")]
        public string QuoteAsset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>pp</c>"] Price precision
        /// </summary>
        [JsonPropertyName("pp")]
        public int PricePrecision { get; set; }
        /// <summary>
        /// ["<c>ap</c>"] Quantity precision
        /// </summary>
        [JsonPropertyName("ap")]
        public int QuantityPrecision { get; set; }
        /// <summary>
        /// ["<c>sp</c>"] Partition
        /// </summary>
        [JsonPropertyName("sp")]
        public string Partition { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>vp</c>"] Value precision
        /// </summary>
        [JsonPropertyName("vp")]
        public int ValuePrecision { get; set; }
        /// <summary>
        /// ["<c>minoa</c>"] Minimal order quantity
        /// </summary>
        [JsonPropertyName("minoa")]
        public decimal? MinOrderQuantity { get; set; }
        /// <summary>
        /// ["<c>maxoa</c>"] Max order quantity
        /// </summary>
        [JsonPropertyName("maxoa")]
        public decimal? MaxOrderQuantity { get; set; }
        /// <summary>
        /// ["<c>minov</c>"] Minimal order value
        /// </summary>
        [JsonPropertyName("minov")]
        public decimal? MinOrderValue { get; set; }
        /// <summary>
        /// ["<c>lominoa</c>"] Minimal quantity of limit order
        /// </summary>
        [JsonPropertyName("lominoa")]
        public decimal? MinQuantityLimitOrder { get; set; }
        /// <summary>
        /// ["<c>lomaxoa</c>"] Max quantity of limit order
        /// </summary>
        [JsonPropertyName("lomaxoa")]
        public decimal? MaxQuantityLimitOrder { get; set; }
        /// <summary>
        /// ["<c>lomaxba</c>"] Max quantity of limit buy order
        /// </summary>
        [JsonPropertyName("lomaxba")]
        public decimal? MaxQuantityLimitBuyOrder { get; set; }
        /// <summary>
        /// ["<c>lomaxsa</c>"] Max quantity of limit sell order
        /// </summary>
        [JsonPropertyName("lomaxsa")]
        public decimal? MaxQuantityLimitSellOrder { get; set; }
        /// <summary>
        /// ["<c>smminoa</c>"] Minimal quantity of market sell order
        /// </summary>
        [JsonPropertyName("smminoa")]
        public decimal? MinQuantityMarketSellOrder { get; set; }
        /// <summary>
        /// ["<c>blmlt</c>"] Buy limit order must be less than this
        /// </summary>
        [JsonPropertyName("blmlt")]
        public decimal? BuyLimitLessThan { get; set; }
        /// <summary>
        /// ["<c>slmgt</c>"] Sell limit order must be less than this
        /// </summary>
        [JsonPropertyName("slmgt")]
        public decimal? SellLimitLessThan { get; set; }
        /// <summary>
        /// ["<c>smmaxoa</c>"] Max quantity of market sell order
        /// </summary>
        [JsonPropertyName("smmaxoa")]
        public decimal? MaxQuantityMarketSellOrder { get; set; }
        /// <summary>
        /// ["<c>bmmaxov</c>"] Max quantity of market buy order
        /// </summary>
        [JsonPropertyName("bmmaxov")]
        public decimal? MaxQuantityMarketBuyOrder { get; set; }
        /// <summary>
        /// ["<c>msormlt</c>"] Sell market order rate must be less than this
        /// </summary>
        [JsonPropertyName("msormlt")]
        public decimal? SellMarketLessThan { get; set; }
        /// <summary>
        /// ["<c>mbormlt</c>"] Buy market order rate must be less than this
        /// </summary>
        [JsonPropertyName("mbormlt")]
        public decimal? BuyMarketLessThan { get; set; }
        /// <summary>
        /// ["<c>maxov</c>"] Max value of a market order
        /// </summary>
        [JsonPropertyName("maxov")]
        public decimal? MaxValueMarketOrder { get; set; }
        /// <summary>
        /// ["<c>u</c>"] Etp symbol
        /// </summary>
        [JsonPropertyName("u")]
        public string? EtpSymbol { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>mfr</c>"] Mfr
        /// </summary>
        [JsonPropertyName("mfr")]
        public decimal? Mfr { get; set; }
        /// <summary>
        /// ["<c>ct</c>"] ETP charge time
        /// </summary>
        [JsonPropertyName("ct")]
        public string? ChareTime { get; set; }
        /// <summary>
        /// ["<c>rt</c>"] ETP rebal time
        /// </summary>
        [JsonPropertyName("rt")]
        public string? RebalTime { get; set; }
        /// <summary>
        /// ["<c>rthr</c>"] ETP rebal threshold
        /// </summary>
        [JsonPropertyName("rthr")]
        public decimal? RebalThreshold { get; set; }
        /// <summary>
        /// ["<c>in</c>"] ETP initial NAV
        /// </summary>
        [JsonPropertyName("in")]
        public decimal InitialNav { get; set; }
        /// <summary>
        /// ["<c>at</c>"] ApiTrading
        /// </summary>
        [JsonPropertyName("at")]
        public string ApiTrading { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>tags</c>"] Tags
        /// </summary>
        [JsonPropertyName("tags")]
        public string Tags { get; set; } = string.Empty;
    }


}
