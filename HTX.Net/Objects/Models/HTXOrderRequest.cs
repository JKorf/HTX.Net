using CryptoExchange.Net.Converters.SystemTextJson;
using HTX.Net.Enums;

namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Order request
    /// </summary>
    [SerializationModel]
    public record HTXOrderRequest
    {
        /// <summary>
        /// Account id
        /// </summary>
        public string AccountId { get; set; } = string.Empty;
        /// <summary>
        /// Symbol
        /// </summary>
        public string Symbol { get; set; } = string.Empty;
        /// <summary>
        /// Order side
        /// </summary>
        public OrderSide Side { get; set; }
        /// <summary>
        /// Order type
        /// </summary>
        public OrderType Type { get; set; }
        /// <summary>
        /// Order quantity. For Buy Market orders this is in quote asset.
        /// </summary>
        public decimal Quantity { get; set; }
        /// <summary>
        /// Limit price
        /// </summary>
        public decimal? Price { get; set; }
        /// <summary>
        /// Order source
        /// </summary>
        public SourceType? Source { get; set; }
        /// <summary>
        /// Client order id
        /// </summary>
        public string? ClientOrderId { get; set; }
        /// <summary>
        /// Stop price
        /// </summary>
        public decimal? StopPrice { get; set; }
        /// <summary>
        /// Stop operator
        /// </summary>
        public Operator? StopOperator { get; set; }
    }
}
