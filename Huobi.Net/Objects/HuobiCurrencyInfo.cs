using System;
using System.Collections.Generic;
using System.Text;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects
{
    /// <summary>
    /// Info on a currency
    /// </summary>
    public class HuobiCurrencyInfo
    {
        /// <summary>
        /// Currency
        /// </summary>
        public string Currency { get; set; } = "";
        /// <summary>
        /// Status of the currency
        /// </summary>
        [JsonProperty("instStatus")]
        public InstrumentStatus Status { get; set; }
        /// <summary>
        /// Chains
        /// </summary>
        public IEnumerable<HuobiChain> Chains { get; set; } = new HuobiChain[0];
    }

    /// <summary>
    /// Info on a currency chain
    /// </summary>
    public class HuobiChain
    {
        /// <summary>
        /// Chain
        /// </summary>
        public string Chain { get; set; } = "";
        /// <summary>
        /// Display name
        /// </summary>
        public string DisplayName { get; set; } = "";
        /// <summary>
        /// Base chain
        /// </summary>
        public string BaseChain { get; set; } = "";
        /// <summary>
        /// Protocol of the base chain
        /// </summary>
        public string BaseChainProtocol { get; set; } = "";
        /// <summary>
        /// Is dynamic fee type or not (only applicable to withdrawFeeType = fixed)
        /// </summary>
        public bool IsDynamic { get; set; }
        /// <summary>
        /// Deposit status
        /// </summary>
        public CurrencyStatus DepositStatus { get; set; }
        /// <summary>
        /// Maximum withdraw fee in each request (only applicable to withdrawFeeType = circulated or ratio)	
        /// </summary>
        public decimal MaxTransactFeeWithdraw { get; set; }
        /// <summary>
        /// Max withdraw amount per request
        /// </summary>
        [JsonProperty("MaxWithdrawAmt")]
        public decimal MaxWithdrawAmount { get; set; }
        /// <summary>
        /// Min deposit amount per request
        /// </summary>
        [JsonProperty("MinDepositAmt")]
        public decimal MinDepositAmount { get; set; }
        /// <summary>
        /// Min withdraw amount per request
        /// </summary>
        [JsonProperty("MinWithdrawAmt")]
        public decimal MinWithdrawAmount { get; set; }
        /// <summary>
        /// Withdraw fee in each request (only applicable to withdrawFeeType = fixed)
        /// </summary>
        public decimal TransactFeeWithdraw { get; set; }
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
        public CurrencyStatus WithdrawStatus { get; set; }
    }
}
