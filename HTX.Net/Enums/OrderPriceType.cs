using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Order price type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<OrderPriceType>))]
    public enum OrderPriceType
    {
		/// <summary>
		/// ["<c>market</c>"] Market
		/// </summary>
		[Map("market")]
		Market,
		/// <summary>
		/// ["<c>limit</c>"] Limit
		/// </summary>
		[Map("limit")]
        Limit,
        /// <summary>
        /// ["<c>opponent</c>"] Best offer
        /// </summary>
        [Map("opponent")]
        BestOffer,
        /// <summary>
        /// ["<c>post_only</c>"] Post only
        /// </summary>
        [Map("post_only")]
        PostOnly,
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
        Optimal20,
        /// <summary>
        /// ["<c>ioc</c>"] Immediate or cancel
        /// </summary>
        [Map("ioc")]
        ImmediateOrCancel,
        /// <summary>
        /// ["<c>fok</c>"] Fill or kill
        /// </summary>
        [Map("fok")]
        FillOrKill,
        /// <summary>
        /// ["<c>opponent_ioc</c>"] Immediate or cancel at best bid
        /// </summary>
        [Map("opponent_ioc")]
        ImmediateOrCancelBestBid,
        /// <summary>
        /// ["<c>optimal_5_ioc</c>"] Immediate or cancel optimal 5
        /// </summary>
        [Map("optimal_5_ioc")]
        ImmediateOrCancelOptimal5,
        /// <summary>
        /// ["<c>optimal_10_ioc</c>"] Immediate or cancel optimal 10
        /// </summary>
        [Map("optimal_10_ioc")]
        ImmediateOrCancelOptimal10,
        /// <summary>
        /// ["<c>optimal_20_ioc</c>"] Immediate or cancel optimal 20
        /// </summary>
        [Map("optimal_20_ioc")]
        ImmediateOrCancelOptimal20,
        /// <summary>
        /// ["<c>opponent_fok</c>"] Fill or kill best offer
        /// </summary>
        [Map("opponent_fok")]
        FillOrKillBestBid,
        /// <summary>
        /// ["<c>optimal_5_fok</c>"] Fill or kill optimal 5
        /// </summary>
        [Map("optimal_5_fok")]
        FillOrKillOptimal5,
        /// <summary>
        /// ["<c>optimal_10_fok</c>"] Fill or kill optimal 10
        /// </summary>
        [Map("optimal_10_fok")]
        FillOrKillOptimal10,
        /// <summary>
        /// ["<c>optimal_20_fok</c>"] Fill or kill optimal 20
        /// </summary>
        [Map("optimal_20_fok")]
        FillOrKillOptimal20,
        /// <summary>
        /// ["<c>formula_price</c>"] Formula price
        /// </summary>
        [Map("formula_price")]
        FormulaPrice
    }
}
