using System;
using CryptoExchange.Net.Converters;

using HTX.Net.Enums;


namespace HTX.Net.Objects.Models.Socket
{
    /// <summary>
    /// Account update
    /// </summary>
    public record HTXAccountUpdate
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Account id
        /// </summary>
        public long AccountId { get; set; }
        /// <summary>
        /// Total balance
        /// </summary>
        public decimal? Balance { get; set; }
        /// <summary>
        /// Available balance
        /// </summary>
        public decimal? Available { get; set; }
        /// <summary>
        /// Type of change
        /// </summary>
        [JsonConverter(typeof(EnumConverter))]
        public AccountEventType? ChangeType { get; set; }
        /// <summary>
        /// Account type
        /// </summary>
        [JsonConverter(typeof(EnumConverter))]
        public BalanceType AccountType { get; set; }
        /// <summary>
        /// Change time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime? ChangeTime { get; set; }
        /// <summary>
        /// Update sequence number
        /// </summary>
        [JsonPropertyName("seqNum")]
        public long SequenceNumber { get; set; }
    }
}
