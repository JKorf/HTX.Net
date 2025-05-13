using CryptoExchange.Net.Converters.SystemTextJson;
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
        /// Tags
        /// </summary>
        [JsonPropertyName("tags")]
        public string? Tags { get; set; }
        /// <summary>
        /// State
        /// </summary>
        [JsonPropertyName("state")]
        public SymbolStatus SymbolStatus { get; set; }
        /// <summary>
        /// WithdrawRisk
        /// </summary>
        [JsonPropertyName("wr")]
        public decimal? WithdrawRisk { get; set; }
        /// <summary>
        /// Symbol name
        /// </summary>
        [JsonPropertyName("sc")]
        public string Name { get; set; } = string.Empty;
        /// <summary>
        /// Base asset display name
        /// </summary>
        [JsonPropertyName("bcdn")]
        public string BaseAssetName { get; set; } = string.Empty;
        /// <summary>
        /// Quote asset display name
        /// </summary>
        [JsonPropertyName("qcdn")]
        public string QuoteAssetName { get; set; } = string.Empty;
        /// <summary>
        /// Etp leverage ratio
        /// </summary>
        [JsonPropertyName("elr")]
        public string? EtpLeverageRatio { get; set; }
        /// <summary>
        /// Price precision
        /// </summary>
        [JsonPropertyName("tpp")]
        public decimal PricePrecision { get; set; }
        /// <summary>
        /// Quantity precision
        /// </summary>
        [JsonPropertyName("tap")]
        public decimal QuantityPrecision { get; set; }
        /// <summary>
        /// Fee precision
        /// </summary>
        [JsonPropertyName("fp")]
        public decimal FeePrecision { get; set; }
        /// <summary>
        /// Super margin leverage ratio
        /// </summary>
        [JsonPropertyName("smlr")]
        public string? SuperMarginLeverageRatio { get; set; }
        /// <summary>
        /// C2C leverage ratio
        /// </summary>
        [JsonPropertyName("flr")]
        public string? C2CLeverageRatio { get; set; }
        /// <summary>
        /// White enabled
        /// </summary>
        [JsonPropertyName("whe")]
        public bool WhiteEnabled { get; set; }
        /// <summary>
        /// Country disabled
        /// </summary>
        [JsonPropertyName("cd")]
        public bool CountryDisabled { get; set; }
        /// <summary>
        /// Trade enabled
        /// </summary>
        [JsonPropertyName("te")]
        public bool TradeEnabled { get; set; }
        /// <summary>
        /// Symbol partition
        /// </summary>
        [JsonPropertyName("sp")]
        public string Partition { get; set; } = string.Empty;
        /// <summary>
        /// Direction
        /// </summary>
        [JsonPropertyName("d")]
        public string? Direction { get; set; }
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
        /// Time trading starts
        /// </summary>
        [JsonPropertyName("toa")]
        public DateTime TradeOpenTime { get; set; }
        /// <summary>
        /// trade total precision
        /// </summary>
        [JsonPropertyName("ttp")]
        public decimal TradeTotalPrecision { get; set; }
        /// <summary>
        /// Weight sort
        /// </summary>
        [JsonPropertyName("w")]
        public long WeightSort { get; set; }
        /// <summary>
        /// Leverage ratio
        /// </summary>
        [JsonPropertyName("lr")]
        public decimal? LeverageRatio { get; set; }
        /// <summary>
        /// DisplayName
        /// </summary>
        [JsonPropertyName("dn")]
        public string DisplayName { get; set; } = string.Empty;
    }
}
