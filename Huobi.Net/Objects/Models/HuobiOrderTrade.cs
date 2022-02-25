using System;
using CryptoExchange.Net.Converters;
using Huobi.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;

namespace Huobi.Net.Objects.Models
{
    /// <summary>
    /// Trade info
    /// </summary>
    public class HuobiOrderTrade
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
        [JsonProperty("created-at"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// The quantity which has been filled
        /// </summary>
        [JsonProperty("filled-amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Transaction fee (positive value). If maker rebate applicable, revert maker rebate value per trade (negative value).
        /// </summary>
        [JsonProperty("filled-fees")]
        public decimal Fee { get; set; }
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
        [JsonProperty("type")]
        internal string TypeInternal { get; set; } = string.Empty;
        /// <summary>
        /// The type of the order
        /// </summary>
        [JsonIgnore]
        public OrderType Type => new OrderTypeConverter(false).ReadString(TypeInternal);
        /// <summary>
        /// The side of the order
        /// </summary>
        [JsonIgnore]
        public OrderSide Side => new OrderSideConverter(false).ReadString(TypeInternal);
        /// <summary>
        /// The role in the transaction: taker or maker
        /// </summary>
        [JsonProperty("role"), JsonConverter(typeof(OrderRoleConverter))]
        public OrderRole Role { get; set; }
        /// <summary>
        /// Asset of transaction fee or transaction fee rebate (transaction fee of buy order is based on base asset, transaction fee of sell order is based on quote asset; transaction fee rebate of buy order is based on quote asset, transaction fee rebate of sell order is based on base asset)
        /// </summary>
        [JsonProperty("fee-currency")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// Deduction type: ht or hbpoint.
        /// </summary>
        [JsonProperty("fee-deduct-currency")]
        public string FeeDeductAsset { get; set; } = string.Empty;
        /// <summary>
        /// Fee deduction status.
        /// </summary>
        [JsonProperty("fee-deduct-state"), JsonConverter(typeof(FeeDeductStateConverter))]
        public FeeDeductState FeeDeductState { get; set; }
    }
}
