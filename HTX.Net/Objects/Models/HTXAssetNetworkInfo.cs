using HTX.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Network info
    /// </summary>
    public record HTXAssetNetworkInfo
    {
        /// <summary>
        /// Network
        /// </summary>
        [JsonPropertyName("chain")]
        public string Network { get; set; } = string.Empty;
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Code
        /// </summary>
        [JsonPropertyName("code")]
        public string Code { get; set; } = string.Empty;
        /// <summary>
        /// Network type
        /// </summary>
        [JsonPropertyName("ct")]
        public NetworkType NetworkType { get; set; }
        /// <summary>
        /// Network of the contract
        /// </summary>
        [JsonPropertyName("ac")]
        public string ContractNetwork { get; set; } = string.Empty;
        /// <summary>
        /// Whether this is the default network
        /// </summary>
        [JsonPropertyName("default")]
        public bool Default { get; set; }
        /// <summary>
        /// Minimal deposit quantity
        /// </summary>
        [JsonPropertyName("dma")]
        public decimal MinDeposit { get; set; }
        /// <summary>
        /// Minimal withdrawal quantity
        /// </summary>
        [JsonPropertyName("wma")]
        public decimal MinWithdrawal { get; set; }
        /// <summary>
        /// Is deposit enabled
        /// </summary>
        [JsonPropertyName("de")]
        public bool DepositEnabled { get; set; }
        /// <summary>
        /// Is withdrawal enabled
        /// </summary>
        [JsonPropertyName("we")]
        public bool WithdrawalEnabled { get; set; }
        /// <summary>
        /// Withdrawal quantity precision
        /// </summary>
        [JsonPropertyName("wp")]
        public int WithdrawalPrecision { get; set; }
        /// <summary>
        /// Fee asset type
        /// </summary>
        [JsonPropertyName("ft")]
        public string FeeType { get; set; } = string.Empty;
        /// <summary>
        /// Display name
        /// </summary>
        [JsonPropertyName("dn")]
        public string DisplayName { get; set; } = string.Empty;
        /// <summary>
        /// Formal name
        /// </summary>
        [JsonPropertyName("fn")]
        public string? FormalName { get; set; }
        /// <summary>
        /// Withdrawal needs tag
        /// </summary>
        [JsonPropertyName("awt")]
        public bool AddressWithdrawalTag { get; set; }
        /// <summary>
        /// Deposit needs tag
        /// </summary>
        [JsonPropertyName("adt")]
        public bool AddressDepositTag { get; set; }
        /// <summary>
        /// Address is single use
        /// </summary>
        [JsonPropertyName("ao")]
        public bool AddressIsOneOff { get; set; }
        /// <summary>
        /// FastConfirms
        /// </summary>
        [JsonPropertyName("fc")]
        public decimal FastConfirms { get; set; }
        /// <summary>
        /// SafeConfirms
        /// </summary>
        [JsonPropertyName("sc")]
        public decimal SafeConfirms { get; set; }
        /// <summary>
        /// Contract address
        /// </summary>
        [JsonPropertyName("ca")]
        public string ContractAddress { get; set; } = string.Empty;
        /// <summary>
        /// Contract network type
        /// </summary>
        [JsonPropertyName("cct")]
        public ContractNetworkType ContractNetworkType { get; set; }
        /// <summary>
        /// Visible
        /// </summary>
        [JsonPropertyName("v")]
        public bool Visible { get; set; }
        /// <summary>
        /// Suspend deposit announcement
        /// </summary>
        [JsonPropertyName("sda")]
        public string? DepositSuspendAnnouncement { get; set; }
        /// <summary>
        /// Suspend withdrawal announcement
        /// </summary>
        [JsonPropertyName("swa")]
        public string? WithdrawalSuspendAnnouncement { get; set; }
        /// <summary>
        /// Deposit description
        /// </summary>
        [JsonPropertyName("deposit-desc")]
        public string DepositDescription { get; set; } = string.Empty;
        /// <summary>
        /// Withdrawal description
        /// </summary>
        [JsonPropertyName("withdraw-desc")]
        public string WithdrawDescription { get; set; } = string.Empty;
    }


}
