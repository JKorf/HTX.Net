namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Account and balance info
    /// </summary>
    public record HTXAccountBalances: HTXAccount
    {
        /// <summary>
        /// The list of balances
        /// </summary>
        [JsonPropertyName("list")]
        public IEnumerable<HTXBalance> Data { get; set; } = Array.Empty<HTXBalance>();
    }
}
