namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Placed conditional order
    /// </summary>
    public record HTXPlacedConditionalOrder
    {
        /// <summary>
        /// The id
        /// </summary>
        public string ClientOrderId { get; set; } = string.Empty;
    }
}
