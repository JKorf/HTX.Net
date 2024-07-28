namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Balance info
    /// </summary>
    public record HTXBalanceWrapper
    {
        /// <summary>
        /// Asset
        /// </summary>
        public string Asset { get; set; } = string.Empty;
        /// <summary>
        /// Frozen
        /// </summary>
        public decimal Frozen { get; set; }
        /// <summary>
        /// Trade
        /// </summary>
        public decimal Trade { get; set; }
    }
}
