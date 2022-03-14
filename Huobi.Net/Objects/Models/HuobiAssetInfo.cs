using System;
using System.Collections.Generic;
using Huobi.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models
{
    /// <summary>
    /// Info on an asset
    /// </summary>
    public class HuobiAssetInfo
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Status of the asset
        /// </summary>
        [JsonProperty("instStatus")]
        public InstrumentStatus Status { get; set; }
        /// <summary>
        /// Networks
        /// </summary>
        [JsonProperty("chains")]
        public IEnumerable<HuobiChain> Networks { get; set; } = Array.Empty<HuobiChain>();
    }

    /// <summary>
    /// Info on an asset network
    /// </summary>
    public class HuobiChain
    {
        /// <summary>
        /// Chain
        /// </summary>
        public string Chain { get; set; } = string.Empty;
        /// <summary>
        /// Display name
        /// </summary>
        public string DisplayName { get; set; } = string.Empty;
        /// <summary>
        /// Base chain
        /// </summary>
        public string BaseChain { get; set; } = string.Empty;
        /// <summary>
        /// Protocol of the base chain
        /// </summary>
        public string BaseChainProtocol { get; set; } = string.Empty;
        /// <summary>
        /// Is dynamic fee type or not (only applicable to withdrawFeeType = fixed)
        /// </summary>
        public bool IsDynamic { get; set; }
        /// <summary>
        /// Deposit status
        /// </summary>
        [JsonConverter(typeof(CurrencyStatusConverter))]
        public CurrencyStatus DepositStatus { get; set; }
        /// <summary>
        /// Maximum withdraw fee in each request (only applicable to withdrawFeeType = circulated or ratio)	
        /// </summary>
        public decimal MaxTransactFeeWithdraw { get; set; }
        /// <summary>
        /// Max withdraw quantity per request
        /// </summary>
        [JsonProperty("maxWithdrawAmt")]
        public decimal MaxWithdrawQuantity { get; set; }
        /// <summary>
        /// Min deposit quantity per request
        /// </summary>
        [JsonProperty("minDepositAmt")]
        public decimal MinDepositQuantity { get; set; }
        /// <summary>
        /// Min withdraw quantity per request
        /// </summary>
        [JsonProperty("minWithdrawAmt")]
        public decimal MinWithdrawQuantity { get; set; }
        /// <summary>
        /// Withdraw fee in each request (only applicable to withdrawFeeType = fixed)
        /// </summary>
        public decimal TransactFeeWithdraw { get; set; }
        /// <summary>
        /// Withdraw fee in each request (only applicable to withdrawFeeType = ratio)
        /// </summary>
        public decimal? TransactFeeRateWithdraw { get; set; }
        /// <summary>
        /// Minimal withdraw fee in each request (only applicable to withdrawFeeType = circulated or ratio)
        /// </summary>
        public decimal MinTransactFeeWithdraw { get; set; }
        /// <summary>
        /// Number of confirmations required for deposit
        /// </summary>
        [JsonProperty("numOfConfirmations")]
        public int NumberOfConfirmations { get; set; }
        /// <summary>
        /// Number of confirmations required for quick success (trading allowed but withdrawal disallowed)
        /// </summary>
        [JsonProperty("numOfFastConfirmations")]
        public int NumberOfFastConfirmations { get; set; }
        /// <summary>
        /// Type of withdraw fee
        /// </summary>
        [JsonConverter(typeof(FeeTypeConverter))]
        public FeeType WithdrawFeeType { get; set; }
        /// <summary>
        /// Precision of withdrawing
        /// </summary>
        public int WithdrawPrecision { get; set; }
        /// <summary>
        /// Withdraw quota per day
        /// </summary>
        public decimal? WithdrawQuotaPerDay { get; set; }
        /// <summary>
        /// Withdraw quota per year
        /// </summary>
        public decimal? WithdrawQuotaPerYear { get; set; }
        /// <summary>
        /// Withdraw quota in total
        /// </summary>
        public decimal? WithdrawQuotaTotal { get; set; }

        /// <summary>
        /// Withdraw status
        /// </summary>
        [JsonConverter(typeof(CurrencyStatusConverter))]
        public CurrencyStatus WithdrawStatus { get; set; }
    }
}
