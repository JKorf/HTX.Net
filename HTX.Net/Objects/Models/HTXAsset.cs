using HTX.Net.Enums;

namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Asset information
    /// </summary>
    [SerializationModel]
    public record HTXAsset
    {
        /// <summary>
        /// Tags
        /// </summary>
        [JsonPropertyName("tags")]
        public string? Tags { get; set; }
        /// <summary>
        /// Asset address with tag
        /// </summary>
        [JsonPropertyName("cawt")]
        public bool AssetAddressTag { get; set; }
        /// <summary>
        /// Fast confirms
        /// </summary>
        [JsonPropertyName("fc")]
        public int FastConfirms { get; set; }
        /// <summary>
        /// Safe confirms
        /// </summary>
        [JsonPropertyName("sc")]
        public int SafeConfirms { get; set; }
        /// <summary>
        /// Minimal deposit quantity
        /// </summary>
        [JsonPropertyName("dma")]
        public decimal DepositMinQuantity { get; set; }
        /// <summary>
        /// Minimal withdrawal quantity
        /// </summary>
        [JsonPropertyName("wma")]
        public decimal WithdrawMinQuantity { get; set; }
        /// <summary>
        /// Fee type
        /// </summary>
        [JsonPropertyName("ft")]
        public string FeeType { get; set; } = string.Empty;
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
        /// Is this a quote asset
        /// </summary>
        [JsonPropertyName("qc")]
        public bool IsQuoteAsset { get; set; }
        /// <summary>
        /// Precision for displaying
        /// </summary>
        [JsonPropertyName("sp")]
        public decimal DisplayPrecision { get; set; }
        /// <summary>
        /// Precision for withdrawing
        /// </summary>
        [JsonPropertyName("wp")]
        public decimal WithdrawPrecision { get; set; }
        /// <summary>
        /// Full asset name
        /// </summary>
        [JsonPropertyName("fn")]
        public string FullName { get; set; } = string.Empty;
        /// <summary>
        /// Asset type
        /// </summary>
        [JsonPropertyName("at")]
        public AssetType AssetType { get; set; }
        /// <summary>
        /// Asset code
        /// </summary>
        [JsonPropertyName("cc")]
        public string AssetCode { get; set; } = string.Empty;
        /// <summary>
        /// Visible
        /// </summary>
        [JsonPropertyName("v")]
        public bool Visible { get; set; }
        /// <summary>
        /// Is deposit enabled
        /// </summary>
        [JsonPropertyName("de")]
        public bool DepositEnabled { get; set; }
        /// <summary>
        /// Is withdraw enabled
        /// </summary>
        [JsonPropertyName("wed")]
        public bool WithdrawEnabled { get; set; }
        /// <summary>
        /// Weight
        /// </summary>
        [JsonPropertyName("w")]
        public long Weight { get; set; }
        /// <summary>
        /// Status
        /// </summary>
        [JsonPropertyName("state")]
        public string AssetStatus { get; set; } = string.Empty;
        /// <summary>
        /// Display name
        /// </summary>
        [JsonPropertyName("dn")]
        public string DisplayName { get; set; } = string.Empty;
        /// <summary>
        /// Info regarding deposits
        /// </summary>
        [JsonPropertyName("dd")]
        public string DepositDescription { get; set; } = string.Empty;
        /// <summary>
        /// Suspend description
        /// </summary>
        [JsonPropertyName("svd")]
        public string? SuspendDescription { get; set; }
        /// <summary>
        /// Suspend withdraw description
        /// </summary>
        [JsonPropertyName("swd")]
        public string? SuspendWithdrawDescription { get; set; }
        /// <summary>
        /// Suspend deposit description
        /// </summary>
        [JsonPropertyName("sdd")]
        public string? SuspendDepositDescription { get; set; }
        /// <summary>
        /// Withdrawal description
        /// </summary>
        [JsonPropertyName("wd")]
        public string WithdrawDescription { get; set; } = string.Empty;
    }


}
