using CryptoExchange.Net.Converters.SystemTextJson;
using HTX.Net.Enums;

namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Financial records
    /// </summary>
    [SerializationModel]
    public record HTXFinancialRecord
    {
        /// <summary>
        /// Query id
        /// </summary>
        [JsonPropertyName("query_id")]
        public long QueryId { get; set; }
        /// <summary>
        /// Record id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// Record type
        /// </summary>

        [JsonPropertyName("type")]
        public FinancialRecordType Type { get; set; }
        /// <summary>
        /// Amount
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("ts")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Margin account
        /// </summary>
        [JsonPropertyName("margin_account")]
        public string MarginAccount { get; set; } = string.Empty;
        /// <summary>
        /// Face margin account
        /// </summary>
        [JsonPropertyName("face_margin_account")]
        public string FaceMarginAccount { get; set; } = string.Empty;

    }
}
