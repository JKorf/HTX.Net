using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Price match
    /// </summary>
    [JsonConverter(typeof(EnumConverter<PriceMatch>))]
    public enum PriceMatch
    {
        /// <summary>
        /// ["<c>opponent</c>"] Opponent price
        /// </summary>
        [Map("opponent")]
        Opponent,
        /// <summary>
        /// ["<c>optimal_5</c>"] Optimal 5
        /// </summary>
        [Map("optimal_5")]
        Optimal5,
        /// <summary>
        /// ["<c>optimal_10</c>"] Optimal 10
        /// </summary>
        [Map("optimal_10")]
        Optimal10,
        /// <summary>
        /// ["<c>optimal_20</c>"] Optimal 20
        /// </summary>
        [Map("optimal_20")]
        Optimal20
    }
}
