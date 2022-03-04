using System.Collections.Generic;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models.Futures
{
    /// <summary>
    /// Position data
    /// </summary>
    public class HuobiFuturesBalance
    {
        /// <summary>
        /// The product code
        /// </summary>
        [JsonProperty("symbol")]
        public string Symbol { get; set; } = string.Empty;

        /// <summary>
        /// The margin balance
        /// </summary>
        [JsonProperty("margin_balance")]
        public decimal MarginBalance { get; set; }

        /// <summary>
        /// The margin position
        /// </summary>
        [JsonProperty("margin_position")]
        public decimal MarginPosition { get; set; }

        /// <summary>
        /// The margin frozen
        /// </summary>
        [JsonProperty("margin_frozen")]
        public decimal MarginFrozen { get; set; }

        /// <summary>
        /// The margin available
        /// </summary>
        [JsonProperty("margin_available")]
        public decimal MarginAvailable { get; set; }

        /// <summary>
        /// Realized profit and loss
        /// </summary>
        [JsonProperty("profit_real")]
        public decimal ProfitReal { get; set; }

        /// <summary>
        /// Unrealized profit and loss
        /// </summary>
        [JsonProperty("profit_unreal")]
        public decimal ProfitUnreal { get; set; }

        /// <summary>
        /// The risk_rate
        /// </summary>
        [JsonProperty("risk_rate")]
        public decimal? RiskRate { get; set; }

        /// <summary>
        /// The liquidation_price
        /// </summary>
        [JsonProperty("liquidation_price")]
        public decimal? LiquidationPrice { get; set; }

        /// <summary>
        /// Available withdrawal
        /// </summary>
        [JsonProperty("withdraw_available")]
        public decimal WithdrawAvailable { get; set; }

        /// <summary>
        /// The leverage rate
        /// </summary>
        [JsonProperty("lever_rate")]
        public decimal LeverRate { get; set; }

        /// <summary>
        /// The adjustment factor
        /// </summary>
        [JsonProperty("adjust_factor")]
        public decimal AdjustmentFactor { get; set; }

        /// <summary>
        /// Static margin
        /// </summary>
        [JsonProperty("margin_static")]
        public decimal Marginstatic { get; set; }
    }

    public class HuobiFuturesUsdtBalance : HuobiFuturesBalance
    {
        /// <summary>
        /// The product code
        /// </summary>
        [JsonProperty("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        
        /// <summary>
        /// The static margin
        /// </summary>
        [JsonProperty("margin_static")]
        public decimal MarginStatic { get; set; }

        /// <summary>
        /// The margin mode
        /// </summary>
        [JsonProperty("margin_mode")]
        public string MarginMode { get; set; } = string.Empty;

        /// <summary>
        /// The margin account
        /// </summary>
        [JsonProperty("margin_account")]
        public string MarginAccount { get; set; } = string.Empty;
    }

    public class HuobiFuturesUsdtCrossBalance
    {
        /// <summary>
        /// The futures balances on the account
        /// </summary>
        [JsonProperty("futures_contract_detail")]
        public IEnumerable<HuobiFuturesUsdtBalance> FuturesBalances { get; set; }

        /// <summary>
        /// The margin mode
        /// </summary>
        [JsonProperty("margin_mode")]
        public string MarginMode { get; set; } = string.Empty;

        /// <summary>
        /// The margin account
        /// </summary>
        [JsonProperty("margin_account")]
        public string MarginAccount { get; set; } = string.Empty;

        /// <summary>
        /// The margin asset
        /// </summary>
        [JsonProperty("margin_asset")]
        public string MarginAsset { get; set; } = string.Empty;

        /// <summary>
        /// The margin balance
        /// </summary>
        [JsonProperty("margin_balance")]
        public decimal MarginBalance { get; set; }

        /// <summary>
        /// Static margin
        /// </summary>
        [JsonProperty("margin_static")]
        public decimal Marginstatic { get; set; }

        /// <summary>
        /// The margin position
        /// </summary>
        [JsonProperty("margin_position")]
        public decimal MarginPosition { get; set; }

        /// <summary>
        /// The margin frozen
        /// </summary>
        [JsonProperty("margin_frozen")]
        public decimal MarginFrozen { get; set; }

        /// <summary>
        /// Realized profit and loss
        /// </summary>
        [JsonProperty("profit_real")]
        public decimal ProfitReal { get; set; }

        /// <summary>
        /// Unrealized profit and loss
        /// </summary>
        [JsonProperty("profit_unreal")]
        public decimal ProfitUnreal { get; set; }

        /// <summary>
        /// Available withdrawal
        /// </summary>
        [JsonProperty("withdraw_available")]
        public decimal WithdrawAvailable { get; set; }

        /// <summary>
        /// The risk_rate
        /// </summary>
        [JsonProperty("risk_rate")]
        public decimal? RiskRate { get; set; }

        /// <summary>
        /// The position mode, single_side，dual_side
        /// </summary>
        [JsonProperty("position_mode")]
        public string PositionMode { get; set; } = string.Empty;

        /// <summary>
        /// The contract details
        /// </summary>
        [JsonProperty("contract_detail")]
        public IEnumerable<HuobiFuturesUsdtBalance> ContractDetail { get; set; }
    }
}
