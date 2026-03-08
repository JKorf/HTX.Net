namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Transfer result
    /// </summary>
    [SerializationModel]
    public record HTXPointTransfer
    {
        /// <summary>
        /// ["<c>transactId</c>"] Transact id
        /// </summary>
        [JsonPropertyName("transactId")]
        public string TransactId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>transactTime</c>"] Transact time
        /// </summary>
        [JsonPropertyName("transactTime")]
        public DateTime? TransactTime { get; set; }
    }


}
