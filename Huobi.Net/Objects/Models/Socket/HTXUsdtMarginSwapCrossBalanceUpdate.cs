using HTX.Net.Objects.Sockets;
using System;
using System.Collections.Generic;
using System.Text;
using HTX.Net.Enums;

namespace HTX.Net.Objects.Models.Socket
{
    /// <summary>
    /// Cross margin balance update
    /// </summary>
    public record HTXUsdtMarginSwapCrossBalanceUpdate : HTXOpMessage
    {
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonPropertyName("ts")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Event
        /// </summary>
        [JsonPropertyName("event")]
        public EventTrigger Event { get; set; }
        /// <summary>
        /// Data
        /// </summary>
        [JsonPropertyName("data")]
        public IEnumerable<HTXUsdtMarginSwapCrossBalanceUpdateData> Data { get; set; } = Array.Empty<HTXUsdtMarginSwapCrossBalanceUpdateData>();
        /// <summary>
        /// User id
        /// </summary>
        [JsonPropertyName("uid")]
        public decimal UserId { get; set; }
    }

    /// <summary>
    /// 
    /// </summary>
    public record HTXUsdtMarginSwapCrossBalanceUpdateData
    {
        /// <summary>
        /// Margin mode
        /// </summary>
        [JsonPropertyName("margin_mode")]
        public MarginMode MarginMode { get; set; }
        /// <summary>
        /// Margin account
        /// </summary>
        [JsonPropertyName("margin_account")]
        public string MarginAccount { get; set; } = string.Empty;
        /// <summary>
        /// Margin asset
        /// </summary>
        [JsonPropertyName("margin_asset")]
        public string MarginAsset { get; set; } = string.Empty;
        /// <summary>
        /// Margin balance
        /// </summary>
        [JsonPropertyName("margin_balance")]
        public decimal MarginBalance { get; set; }
        /// <summary>
        /// Static margin
        /// </summary>
        [JsonPropertyName("margin_static")]
        public decimal MarginStatic { get; set; }
        /// <summary>
        /// Margin position
        /// </summary>
        [JsonPropertyName("margin_position")]
        public decimal MarginPosition { get; set; }
        /// <summary>
        /// Margin frozen
        /// </summary>
        [JsonPropertyName("margin_frozen")]
        public decimal MarginFrozen { get; set; }
        /// <summary>
        /// Realized profit and loss
        /// </summary>
        [JsonPropertyName("profit_real")]
        public decimal RealizedPnl { get; set; }
        /// <summary>
        /// Unrealized profit and loss
        /// </summary>
        [JsonPropertyName("profit_unreal")]
        public decimal UnrealizedPnl { get; set; }
        /// <summary>
        /// Withdraw available
        /// </summary>
        [JsonPropertyName("withdraw_available")]
        public decimal WithdrawAvailable { get; set; }
        /// <summary>
        /// Risk rate
        /// </summary>
        [JsonPropertyName("risk_rate")]
        public decimal? RiskRate { get; set; }
        /// <summary>
        /// Position mode
        /// </summary>
        [JsonPropertyName("position_mode")]
        public PositionMode PositionMode { get; set; }
        /// <summary>
        /// Contract detail
        /// </summary>
        [JsonPropertyName("contract_detail")]
        public IEnumerable<HTXUsdtMarginSwapCrossBalanceUpdateContract> ContractDetail { get; set; } = Array.Empty<HTXUsdtMarginSwapCrossBalanceUpdateContract>();
        /// <summary>
        /// Futures contract detail
        /// </summary>
        [JsonPropertyName("futures_contract_detail")]
        public IEnumerable<HTXUsdtMarginSwapCrossBalanceUpdateFutures> FuturesContractDetail { get; set; } = Array.Empty<HTXUsdtMarginSwapCrossBalanceUpdateFutures>();
    }

    /// <summary>
    /// Contract info
    /// </summary>
    public record HTXUsdtMarginSwapCrossBalanceUpdateContract
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// Margin position
        /// </summary>
        [JsonPropertyName("margin_position")]
        public decimal MarginPosition { get; set; }
        /// <summary>
        /// Margin frozen
        /// </summary>
        [JsonPropertyName("margin_frozen")]
        public decimal MarginFrozen { get; set; }
        /// <summary>
        /// Margin available
        /// </summary>
        [JsonPropertyName("margin_available")]
        public decimal MarginAvailable { get; set; }
        /// <summary>
        /// Unrealized profit and loss
        /// </summary>
        [JsonPropertyName("profit_unreal")]
        public decimal UnrealizedPnl { get; set; }
        /// <summary>
        /// Liquidation price
        /// </summary>
        [JsonPropertyName("liquidation_price")]
        public decimal LiquidationPrice { get; set; }
        /// <summary>
        /// Leverage rate
        /// </summary>
        [JsonPropertyName("lever_rate")]
        public decimal LeverageRate { get; set; }
        /// <summary>
        /// Adjust factor
        /// </summary>
        [JsonPropertyName("adjust_factor")]
        public decimal AdjustFactor { get; set; }
        /// <summary>
        /// Contract type
        /// </summary>
        [JsonPropertyName("contract_type")]
        public ContractType ContractType { get; set; }
        /// <summary>
        /// Pair
        /// </summary>
        [JsonPropertyName("pair")]
        public string Pair { get; set; } = string.Empty;
        /// <summary>
        /// Business type
        /// </summary>
        [JsonPropertyName("business_type")]
        public BusinessType BusinessType { get; set; }
    }

    /// <summary>
    /// Futures contract
    /// </summary>
    public record HTXUsdtMarginSwapCrossBalanceUpdateFutures
    {
        /// <summary>
        /// Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// Margin position
        /// </summary>
        [JsonPropertyName("margin_position")]
        public decimal MarginPosition { get; set; }
        /// <summary>
        /// Margin frozen
        /// </summary>
        [JsonPropertyName("margin_frozen")]
        public decimal MarginFrozen { get; set; }
        /// <summary>
        /// Margin available
        /// </summary>
        [JsonPropertyName("margin_available")]
        public decimal MarginAvailable { get; set; }
        /// <summary>
        /// Unrealized profit and loss
        /// </summary>
        [JsonPropertyName("profit_unreal")]
        public decimal UnrealizedPnl { get; set; }
        /// <summary>
        /// Liquidation price
        /// </summary>
        [JsonPropertyName("liquidation_price")]
        public decimal LiquidationPrice { get; set; }
        /// <summary>
        /// Leverage rate
        /// </summary>
        [JsonPropertyName("lever_rate")]
        public decimal LeverageRate { get; set; }
        /// <summary>
        /// Adjust factor
        /// </summary>
        [JsonPropertyName("adjust_factor")]
        public decimal AdjustFactor { get; set; }
        /// <summary>
        /// Contract type
        /// </summary>
        [JsonPropertyName("contract_type")]
        public ContractType ContractType { get; set; }
        /// <summary>
        /// Pair
        /// </summary>
        [JsonPropertyName("pair")]
        public string Pair { get; set; } = string.Empty;
        /// <summary>
        /// Business type
        /// </summary>
        [JsonPropertyName("business_type")]
        public BusinessType BusinessType { get; set; }
    }


}
