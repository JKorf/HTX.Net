using CryptoExchange.Net.Converters;
using Huobi.Net.Converters;
using Newtonsoft.Json;
using System;
using CryptoExchange.Net.ExchangeInterfaces;

namespace Huobi.Net.Objects
{
    /// <summary>
    /// Trade info
    /// </summary>
    public class HuobiOrderTrade : ICommonRecentTrade, ICommonTrade
    {
        /// <summary>
        /// The id of the trade
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// The symbol of the trade
        /// </summary>
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// The timestamp in milliseconds when this record is created
        /// </summary>
        [JsonProperty("created-at"), JsonConverter(typeof(TimestampConverter))]
        public DateTime CreatedAt { get; set; }
        /// <summary>
        /// The amount which has been filled
        /// </summary>
        [JsonProperty("filled-amount")]
        public decimal FilledQuantity { get; set; }
        /// <summary>
        /// Transaction fee (positive value). If maker rebate applicable, revert maker rebate value per trade (negative value).
        /// </summary>
        [JsonProperty("filled-fees")]
        public decimal FilledFees { get; set; }
        /// <summary>
        /// Deduction amount (unit: in ht or hbpoint).
        /// </summary>
        [JsonProperty("filled-points")]
        public decimal FilledPoints { get; set; }
        /// <summary>
        /// The id of the trade
        /// </summary>
        [JsonProperty("trade-id")]
        public long TradeId { get; set; }
        /// <summary>
        /// The id of the match
        /// </summary>
        [JsonProperty("match-id")]
        public long MatchId { get; set; }
        /// <summary>
        /// The id of the order
        /// </summary>
        [JsonProperty("order-id")]
        public long OrderId { get; set; }
        /// <summary>
        /// The limit price of limit order
        /// </summary>
        public decimal Price { get; set; }
        /// <summary>
        /// The source where the order was triggered, possible values: sys, web, api, app
        /// </summary>
        public string Source { get; set; } = string.Empty;
        /// <summary>
        /// The type of the order
        /// </summary>
        [JsonProperty("type"), JsonConverter(typeof(OrderTypeConverter))]
        public HuobiOrderType OrderType { get; set; }
        /// <summary>
        /// The role in the transaction: taker or maker
        /// </summary>
        [JsonProperty("role"), JsonConverter(typeof(OrderRoleConverter))]
        public HuobiOrderRole Role { get; set; }
        /// <summary>
        /// Currency of transaction fee or transaction fee rebate (transaction fee of buy order is based on base currency, transaction fee of sell order is based on quote currency; transaction fee rebate of buy order is based on quote currency, transaction fee rebate of sell order is based on base currency)
        /// </summary>
        [JsonProperty("fee-currency")]
        public string FeeCurrency { get; set; } = string.Empty;
        /// <summary>
        /// Deduction type: ht or hbpoint.
        /// </summary>
        [JsonProperty("fee-deduct-currency")]
        public string FeeDeductCurrency { get; set; } = string.Empty;
        /// <summary>
        /// Fee deduction status.
        /// </summary>
        [JsonProperty("fee-deduct-state"), JsonConverter(typeof(FeeDeductStateConverter))]
        public HuobiFeeDeductState FeeDeductState { get; set; }


        string ICommonTrade.CommonId => Id.ToString();
        decimal ICommonTrade.CommonPrice => Price;
        decimal ICommonTrade.CommonQuantity => FilledQuantity;
        decimal ICommonTrade.CommonFee => FilledFees;
        string? ICommonTrade.CommonFeeAsset => null;
        DateTime ICommonTrade.CommonTradeTime => CreatedAt;
        decimal ICommonRecentTrade.CommonPrice => Price;
        decimal ICommonRecentTrade.CommonQuantity => FilledQuantity;
        DateTime ICommonRecentTrade.CommonTradeTime => CreatedAt;
    }
}
