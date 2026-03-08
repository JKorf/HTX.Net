using HTX.Net.Enums;

namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Network info
    /// </summary>
    [SerializationModel]
    public record HTXAssetNetworkInfo
    {
        /// <summary>
        /// ["<c>chain</c>"] Network
        /// </summary>
        [JsonPropertyName("chain")]
        public string Network { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>currency</c>"] Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>code</c>"] Code
        /// </summary>
        [JsonPropertyName("code")]
        public string Code { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>ct</c>"] Network type
        /// </summary>
        [JsonPropertyName("ct")]
        public NetworkType NetworkType { get; set; }
        /// <summary>
        /// ["<c>ac</c>"] Network of the contract
        /// </summary>
        [JsonPropertyName("ac")]
        public string ContractNetwork { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>default</c>"] Whether this is the default network
        /// </summary>
        [JsonPropertyName("default")]
        public bool Default { get; set; }
        /// <summary>
        /// ["<c>dma</c>"] Minimal deposit quantity
        /// </summary>
        [JsonPropertyName("dma")]
        public decimal MinDeposit { get; set; }
        /// <summary>
        /// ["<c>wma</c>"] Minimal withdrawal quantity
        /// </summary>
        [JsonPropertyName("wma")]
        public decimal MinWithdrawal { get; set; }
        /// <summary>
        /// ["<c>de</c>"] Is deposit enabled
        /// </summary>
        [JsonPropertyName("de")]
        public bool DepositEnabled { get; set; }
        /// <summary>
        /// ["<c>we</c>"] Is withdrawal enabled
        /// </summary>
        [JsonPropertyName("we")]
        public bool WithdrawalEnabled { get; set; }
        /// <summary>
        /// ["<c>wp</c>"] Withdrawal quantity precision
        /// </summary>
        [JsonPropertyName("wp")]
        public int WithdrawalPrecision { get; set; }
        /// <summary>
        /// ["<c>ft</c>"] Fee asset type
        /// </summary>
        [JsonPropertyName("ft")]
        public string FeeType { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>dn</c>"] Display name
        /// </summary>
        [JsonPropertyName("dn")]
        public string DisplayName { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>fn</c>"] Formal name
        /// </summary>
        [JsonPropertyName("fn")]
        public string? FormalName { get; set; }
        /// <summary>
        /// ["<c>awt</c>"] Withdrawal needs tag
        /// </summary>
        [JsonPropertyName("awt")]
        public bool AddressWithdrawalTag { get; set; }
        /// <summary>
        /// ["<c>adt</c>"] Deposit needs tag
        /// </summary>
        [JsonPropertyName("adt")]
        public bool AddressDepositTag { get; set; }
        /// <summary>
        /// ["<c>ao</c>"] Address is single use
        /// </summary>
        [JsonPropertyName("ao")]
        public bool AddressIsOneOff { get; set; }
        /// <summary>
        /// ["<c>fc</c>"] FastConfirms
        /// </summary>
        [JsonPropertyName("fc")]
        public decimal FastConfirms { get; set; }
        /// <summary>
        /// ["<c>sc</c>"] SafeConfirms
        /// </summary>
        [JsonPropertyName("sc")]
        public decimal SafeConfirms { get; set; }
        /// <summary>
        /// ["<c>ca</c>"] Contract address
        /// </summary>
        [JsonPropertyName("ca")]
        public string ContractAddress { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>cct</c>"] Contract network type
        /// </summary>
        [JsonPropertyName("cct")]
        public ContractNetworkType ContractNetworkType { get; set; }
        /// <summary>
        /// ["<c>v</c>"] Visible
        /// </summary>
        [JsonPropertyName("v")]
        public bool Visible { get; set; }
        /// <summary>
        /// ["<c>sda</c>"] Suspend deposit announcement
        /// </summary>
        [JsonPropertyName("sda")]
        public string? DepositSuspendAnnouncement { get; set; }
        /// <summary>
        /// ["<c>swa</c>"] Suspend withdrawal announcement
        /// </summary>
        [JsonPropertyName("swa")]
        public string? WithdrawalSuspendAnnouncement { get; set; }
        /// <summary>
        /// ["<c>deposit-desc</c>"] Deposit description
        /// </summary>
        [JsonPropertyName("deposit-desc")]
        public string DepositDescription { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>withdraw-desc</c>"] Withdrawal description
        /// </summary>
        [JsonPropertyName("withdraw-desc")]
        public string WithdrawDescription { get; set; } = string.Empty;
    }


}
