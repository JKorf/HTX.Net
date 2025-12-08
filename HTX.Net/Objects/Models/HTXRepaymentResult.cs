namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Repayment result
    /// </summary>
    [SerializationModel]
    public record HTXRepaymentResult
    {
        /// <summary>
        /// Repayment id
        /// </summary>
        [JsonPropertyName("repayId")]
        public long RepayId { get; set; }
        /// <summary>
        /// Repay time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("repayTime")]
        public DateTime RepayTime { get; set; }
    }
}
