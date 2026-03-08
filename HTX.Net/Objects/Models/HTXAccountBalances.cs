namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Account and balance info
    /// </summary>
    [SerializationModel]
    public record HTXAccountBalances: HTXAccount
    {
        /// <summary>
        /// ["<c>list</c>"] The list of balances
        /// </summary>
        [JsonPropertyName("list")]
        public HTXBalance[] Data { get; set; } = Array.Empty<HTXBalance>();

        /// <summary>
        /// ["<c>symbol</c>"] Symbol
        /// </summary>
        [JsonPropertyName("symbol")]
        public string? Symbol { get; set; }
    }
}
