using HTX.Net.Enums;

namespace HTX.Net.Objects.Models.UsdtFuturesV5
{
    /// <summary>
    /// Financial record
    /// </summary>
    [SerializationModel]
    public record HTXBillV5
    {
        /// <summary>
        /// ["<c>id</c>"] Query id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// ["<c>contract_code</c>"] Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>margin_mode</c>"] Margin mode
        /// </summary>
        [JsonPropertyName("margin_mode")]
        public MarginMode MarginMode { get; set; }
        /// <summary>
        /// ["<c>type</c>"] Financial type
        /// </summary>
        [JsonPropertyName("type")]
        public FinancialRecordType Type { get; set; }
        /// <summary>
        /// ["<c>currency</c>"] Currency
        /// </summary>
        [JsonPropertyName("currency")]
        public string Currency { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>amount</c>"] Amount
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>created_time</c>"] Created time
        /// </summary>
        [JsonPropertyName("created_time"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime CreateTime { get; set; }
    }
}
