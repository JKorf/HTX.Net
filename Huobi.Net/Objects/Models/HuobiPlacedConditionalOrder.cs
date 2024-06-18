namespace Huobi.Net.Objects.Models
{
    /// <summary>
    /// Placed conditional order
    /// </summary>
    public record HuobiPlacedConditionalOrder
    {
        /// <summary>
        /// The id
        /// </summary>
        public string ClientOrderId { get; set; } = string.Empty;
    }
}
