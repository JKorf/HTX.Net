using HTX.Net.Enums;

namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Symbol information
    /// </summary>
    [SerializationModel]
    public record HTXSymbol
    {
        /// <summary>
        /// ["<c>tags</c>"] Tags
        /// </summary>
        [JsonPropertyName("tags")]
        public string? Tags { get; set; }
        /// <summary>
        /// ["<c>state</c>"] State
        /// </summary>
        [JsonPropertyName("state")]
        public SymbolStatus SymbolStatus { get; set; }
        /// <summary>
        /// ["<c>wr</c>"] WithdrawRisk
        /// </summary>
        [JsonPropertyName("wr")]
        public decimal? WithdrawRisk { get; set; }
        /// <summary>
        /// ["<c>sc</c>"] Symbol name
        /// </summary>
        [JsonPropertyName("sc")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>bcdn</c>"] Base asset display name
        /// </summary>
        [JsonPropertyName("bcdn")]
        public string BaseAssetName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>qcdn</c>"] Quote asset display name
        /// </summary>
        [JsonPropertyName("qcdn")]
        public string QuoteAssetName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>elr</c>"] Etp leverage ratio
        /// </summary>
        [JsonPropertyName("elr")]
        public string? EtpLeverageRatio { get; set; }
        /// <summary>
        /// ["<c>tpp</c>"] Price precision
        /// </summary>
        [JsonPropertyName("tpp")]
        public decimal PricePrecision { get; set; }
        /// <summary>
        /// ["<c>tap</c>"] Quantity precision
        /// </summary>
        [JsonPropertyName("tap")]
        public decimal QuantityPrecision { get; set; }
        /// <summary>
        /// ["<c>fp</c>"] Fee precision
        /// </summary>
        [JsonPropertyName("fp")]
        public decimal FeePrecision { get; set; }
        /// <summary>
        /// ["<c>smlr</c>"] Super margin leverage ratio
        /// </summary>
        [JsonPropertyName("smlr")]
        public string? SuperMarginLeverageRatio { get; set; }
        /// <summary>
        /// ["<c>flr</c>"] C2C leverage ratio
        /// </summary>
        [JsonPropertyName("flr")]
        public string? C2CLeverageRatio { get; set; }
        /// <summary>
        /// ["<c>whe</c>"] White enabled
        /// </summary>
        [JsonPropertyName("whe")]
        public bool WhiteEnabled { get; set; }
        /// <summary>
        /// ["<c>cd</c>"] Country disabled
        /// </summary>
        [JsonPropertyName("cd")]
        public bool CountryDisabled { get; set; }
        /// <summary>
        /// ["<c>te</c>"] Trade enabled
        /// </summary>
        [JsonPropertyName("te")]
        public bool TradeEnabled { get; set; }
        /// <summary>
        /// ["<c>sp</c>"] Symbol partition
        /// </summary>
        [JsonPropertyName("sp")]
        public string Partition { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>d</c>"] Direction
        /// </summary>
        [JsonPropertyName("d")]
        public string? Direction { get; set; }
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
        /// ["<c>toa</c>"] Time trading starts
        /// </summary>
        [JsonPropertyName("toa")]
        public DateTime TradeOpenTime { get; set; }
        /// <summary>
        /// ["<c>ttp</c>"] trade total precision
        /// </summary>
        [JsonPropertyName("ttp")]
        public decimal TradeTotalPrecision { get; set; }
        /// <summary>
        /// ["<c>w</c>"] Weight sort
        /// </summary>
        [JsonPropertyName("w")]
        public long WeightSort { get; set; }
        /// <summary>
        /// ["<c>lr</c>"] Leverage ratio
        /// </summary>
        [JsonPropertyName("lr")]
        public decimal? LeverageRatio { get; set; }
        /// <summary>
        /// ["<c>dn</c>"] DisplayName
        /// </summary>
        [JsonPropertyName("dn")]
        public string DisplayName { get; set; } = string.Empty;
    }
}
