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
        /// ["<c>query_id</c>"] Query id
        /// </summary>
        [JsonPropertyName("query_id")]
        public long QueryId { get; set; }
        /// <summary>
        /// ["<c>id</c>"] Record id
        /// </summary>
        [JsonPropertyName("id")]
        public long Id { get; set; }
        /// <summary>
        /// ["<c>type</c>"] Record type
        /// </summary>

        [JsonPropertyName("type")]
        public FinancialRecordType Type { get; set; }
        /// <summary>
        /// ["<c>amount</c>"] Amount
        /// </summary>
        [JsonPropertyName("amount")]
        public decimal Quantity { get; set; }
        /// <summary>
        /// ["<c>ts</c>"] Timestamp
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("ts")]
        public DateTime Timestamp { get; set; }
        /// <summary>
        /// ["<c>contract_code</c>"] Contract code
        /// </summary>
        [JsonPropertyName("contract_code")]
        public string ContractCode { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>asset</c>"] Asset
        /// </summary>
        [JsonPropertyName("asset")]
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>margin_account</c>"] Margin account
        /// </summary>
        [JsonPropertyName("margin_account")]
        public string MarginAccount { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>face_margin_account</c>"] Face margin account
        /// </summary>
        [JsonPropertyName("face_margin_account")]
        public string FaceMarginAccount { get; set; } = string.Empty;

    }
}
