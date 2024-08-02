namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Repayment result
    /// </summary>
    public record HTXRepaymentResult
    {
        /// <summary>
        /// Repayment id
        /// </summary>
        [JsonPropertyName("repayId")]
        public string RepayId { get; set; } = string.Empty;
        /// <summary>
        /// Repay time
        /// </summary>
        [JsonConverter(typeof(DateTimeConverter))]
        [JsonPropertyName("repayTime")]
        public DateTime RepayTime { get; set; }
    }
}
