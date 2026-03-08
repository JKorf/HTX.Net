namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Repayment result
    /// </summary>
    [SerializationModel]
    public record HTXRepaymentResult
    {
        /// <summary>
        /// ["<c>repayId</c>"] Repayment id
        /// </summary>
        [JsonPropertyName("repayId")]
        public long RepayId { get; set; }
        /// <summary>
        /// ["<c>repayTime</c>"] Repay time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("repayTime")]
        public DateTime RepayTime { get; set; }
    }
}
