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
        /// ["<c>tags</c>"] Tags
        /// </summary>
        [JsonPropertyName("tags")]
        public string? Tags { get; set; }
        /// <summary>
        /// ["<c>cawt</c>"] Asset address with tag
        /// </summary>
        [JsonPropertyName("cawt")]
        public bool AssetAddressTag { get; set; }
        /// <summary>
        /// ["<c>fc</c>"] Fast confirms
        /// </summary>
        [JsonPropertyName("fc")]
        public int FastConfirms { get; set; }
        /// <summary>
        /// ["<c>sc</c>"] Safe confirms
        /// </summary>
        [JsonPropertyName("sc")]
        public int SafeConfirms { get; set; }
        /// <summary>
        /// ["<c>dma</c>"] Minimal deposit quantity
        /// </summary>
        [JsonPropertyName("dma")]
        public decimal DepositMinQuantity { get; set; }
        /// <summary>
        /// ["<c>wma</c>"] Minimal withdrawal quantity
        /// </summary>
        [JsonPropertyName("wma")]
        public decimal WithdrawMinQuantity { get; set; }
        /// <summary>
        /// ["<c>ft</c>"] Fee type
        /// </summary>
        [JsonPropertyName("ft")]
        public string FeeType { get; set; } = string.Empty;
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
        /// ["<c>qc</c>"] Is this a quote asset
        /// </summary>
        [JsonPropertyName("qc")]
        public bool IsQuoteAsset { get; set; }
        /// <summary>
        /// ["<c>sp</c>"] Precision for displaying
        /// </summary>
        [JsonPropertyName("sp")]
        public decimal DisplayPrecision { get; set; }
        /// <summary>
        /// ["<c>wp</c>"] Precision for withdrawing
        /// </summary>
        [JsonPropertyName("wp")]
        public decimal WithdrawPrecision { get; set; }
        /// <summary>
        /// ["<c>fn</c>"] Full asset name
        /// </summary>
        [JsonPropertyName("fn")]
        public string FullName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>at</c>"] Asset type
        /// </summary>
        [JsonPropertyName("at")]
        public AssetType AssetType { get; set; }
        /// <summary>
        /// ["<c>cc</c>"] Asset code
        /// </summary>
        [JsonPropertyName("cc")]
        public string AssetCode { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>v</c>"] Visible
        /// </summary>
        [JsonPropertyName("v")]
        public bool Visible { get; set; }
        /// <summary>
        /// ["<c>de</c>"] Is deposit enabled
        /// </summary>
        [JsonPropertyName("de")]
        public bool DepositEnabled { get; set; }
        /// <summary>
        /// ["<c>wed</c>"] Is withdraw enabled
        /// </summary>
        [JsonPropertyName("wed")]
        public bool WithdrawEnabled { get; set; }
        /// <summary>
        /// ["<c>w</c>"] Weight
        /// </summary>
        [JsonPropertyName("w")]
        public long Weight { get; set; }
        /// <summary>
        /// ["<c>state</c>"] Status
        /// </summary>
        [JsonPropertyName("state")]
        public string AssetStatus { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>dn</c>"] Display name
        /// </summary>
        [JsonPropertyName("dn")]
        public string DisplayName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>dd</c>"] Info regarding deposits
        /// </summary>
        [JsonPropertyName("dd")]
        public string DepositDescription { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>svd</c>"] Suspend description
        /// </summary>
        [JsonPropertyName("svd")]
        public string? SuspendDescription { get; set; }
        /// <summary>
        /// ["<c>swd</c>"] Suspend withdraw description
        /// </summary>
        [JsonPropertyName("swd")]
        public string? SuspendWithdrawDescription { get; set; }
        /// <summary>
        /// ["<c>sdd</c>"] Suspend deposit description
        /// </summary>
        [JsonPropertyName("sdd")]
        public string? SuspendDepositDescription { get; set; }
        /// <summary>
        /// ["<c>wd</c>"] Withdrawal description
        /// </summary>
        [JsonPropertyName("wd")]
        public string WithdrawDescription { get; set; } = string.Empty;
    }


}
