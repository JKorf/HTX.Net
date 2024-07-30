using System;
using CryptoExchange.Net.Converters;

using HTX.Net.Enums;


namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Trade info
    /// </summary>
    public record HTXOrderTrade
    {
        /// <summary>
        /// The id of the trade
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// The symbol of the trade
        /// </summary>
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// The timestamp in milliseconds when this record is created
        /// </summary>
        [JsonPropertyName("created-at"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// The quantity which has been filled
        /// </summary>
        [JsonPropertyName("filled-amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Transaction fee (positive value). If maker rebate applicable, revert maker rebate value per trade (negative value).
        /// </summary>
        [JsonPropertyName("filled-fees")]
        public decimal Fee { get; set; }
        /// <summary>
        /// Deduction amount (unit: in ht or hbpoint).
        /// </summary>
        [JsonPropertyName("filled-points")]
        public decimal FilledPoints { get; set; }
        /// <summary>
        /// The id of the trade
        /// </summary>
        [JsonPropertyName("trade-id")]
        public long TradeId { get; set; }
        /// <summary>
        /// The id of the match
        /// </summary>
        [JsonPropertyName("match-id")]
        public long MatchId { get; set; }
        /// <summary>
        /// The id of the order
        /// </summary>
        [JsonPropertyName("order-id")]
        public long OrderId { get; set; }
        /// <summary>
        /// The limit price of limit order
        /// </summary>
        [JsonPropertyName("price")]
        public decimal Price { get; set; }
        /// <summary>
        /// The source where the order was triggered, possible values: sys, web, api, app
        /// </summary>
        [JsonPropertyName("source")]
        public string Source { get; set; } = string.Empty;

        /// <summary>
        /// The raw type string
        /// </summary>
        [JsonPropertyName("type")]
        public string RawType { get; set; } = string.Empty;

        /// <summary>
        /// The type of the order
        /// </summary>
        [JsonIgnore]
        public OrderType Type => EnumConverter.ParseString<OrderType>(RawType);

        /// <summary>
        /// The side of the order
        /// </summary>
        [JsonIgnore]
        public OrderSide Side => EnumConverter.ParseString<OrderSide>(RawType);
        /// <summary>
        /// The role in the transaction: taker or maker
        /// </summary>
        [JsonPropertyName("role"), JsonConverter(typeof(EnumConverter))]
        public OrderRole Role { get; set; }
        /// <summary>
        /// Asset of transaction fee or transaction fee rebate (transaction fee of buy order is based on base asset, transaction fee of sell order is based on quote asset; transaction fee rebate of buy order is based on quote asset, transaction fee rebate of sell order is based on base asset)
        /// </summary>
        [JsonPropertyName("fee-currency")]
        public string FeeAsset { get; set; } = string.Empty;
        /// <summary>
        /// Deduction type: ht or hbpoint.
        /// </summary>
        [JsonPropertyName("fee-deduct-currency")]
        public string FeeDeductAsset { get; set; } = string.Empty;
        /// <summary>
        /// Fee deduction status.
        /// </summary>
        [JsonPropertyName("fee-deduct-state"), JsonConverter(typeof(EnumConverter))]
        public FeeDeductStatus FeeDeductStatus { get; set; }
    }
}
