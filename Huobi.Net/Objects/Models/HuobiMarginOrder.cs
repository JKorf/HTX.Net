using CryptoExchange.Net.Converters;
using Huobi.Net.Enums;
using Newtonsoft.Json;
using System;

namespace Huobi.Net.Objects.Models
{
    /// <summary>
    /// Margin order info
    /// </summary>
    public class HuobiMarginOrder
    {
        /// <summary>
        /// Order id
        /// </summary>
        public long Id { get; set; }
        /// <summary>
        /// Account id
        /// </summary>
        [JsonProperty("account-id")]
        public long AccountId { get; set; }
        /// <summary>
        /// User id
        /// </summary>
        [JsonProperty("user-id")]
        public long UserId { get; set; }
        /// <summary>
        /// Symbol
        /// </summary>
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Asset
        /// </summary>
        [JsonProperty("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Creation time
        /// </summary>
        [JsonProperty("created-at")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreateTime { get; set; }
        /// <summary>
        /// Accrue time
        /// </summary>
        [JsonProperty("accrued-at")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? AccrueTime { get; set; }
        /// <summary>
        /// Loan quantity
        /// </summary>
        [JsonProperty("loan-amount")]
        public decimal LoanQuantity { get; set; }
        /// <summary>
        /// Loan balance left
        /// </summary>
        [JsonProperty("loan-balance")]
        public decimal LoanBalance { get; set; }
        /// <summary>
        /// Interst rate
        /// </summary>
        [JsonProperty("interest-rate")]
        public decimal? InterestRate { get; set; }
        /// <summary>
        /// Interest quantity
        /// </summary>
        [JsonProperty("interest-amount")]
        public decimal InterestQuantity { get; set; }
        /// <summary>
        /// Interest left
        /// </summary>
        [JsonProperty("interest-balance")]
        public decimal InterestBalance { get; set; }
        /// <summary>
        /// State
        /// </summary>
        [JsonConverter(typeof(EnumConverter))]
        public MarginOrderStatus State { get; set; }
        /// <summary>
        /// Paid Huobi points
        /// </summary>
        [JsonProperty("paid-point")]
        public decimal PaidPoints { get; set; }
        /// <summary>
        /// Paid asset
        /// </summary>
        [JsonProperty("paid-coin")]
        public decimal PaidAsset { get; set; }
        /// <summary>
        /// Filled Huobi points
        /// </summary>
        [JsonProperty("filled-points")]
        public decimal FilledPoints { get; set; }
        /// <summary>
        /// HT deduction amount
        /// </summary>
        [JsonProperty("filled-ht")]
        public decimal FilledHt { get; set; }
        /// <summary>
        /// Deduct rate
        /// </summary>
        [JsonProperty("deduct-rate")]
        public decimal? DeductRate { get; set; }
        /// <summary>
        /// Deduct asset
        /// </summary>
        [JsonProperty("deduct-currency")]
        public string? DeductAsset { get; set; }
        /// <summary>
        /// Deduct quantity
        /// </summary>
        [JsonProperty("deduct-amount")]
        public decimal? DeductQuantity { get; set; }
        /// <summary>
        /// Last updated
        /// </summary>
        [JsonProperty("updated-at")]
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? UpdateTime { get; set; }
        /// <summary>
        /// Hourly interest rate
        /// </summary>
        [JsonProperty("hour-interest-rate")]
        public decimal? HourInterestRate { get; set; }
        /// <summary>
        /// Daily interest rate
        /// </summary>
        [JsonProperty("day-interest-rate")]
        public decimal? DayInterestRate { get; set; }
    }
}
