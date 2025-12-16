using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Balance type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<ValuationBalanceType>))]
    public enum ValuationBalanceType
    {
        /// <summary>
        /// Spot
        /// </summary>
        [Map("1")]
        Spot,
        /// <summary>
        /// Isolated 
        /// </summary>
        [Map("2")]
        IsolatedMargin,
        /// <summary>
        /// Cross 
        /// </summary>
        [Map("3")]
        CrossMargin,
        /// <summary>
        /// Coin futures
        /// </summary>
        [Map("4")]
        CoinFutures,
        /// <summary>
        /// Flat 
        /// </summary>
        [Map("5")]
        Flat,
        /// <summary>
        /// Minepool 
        /// </summary>
        [Map("6", "16")]
        Minepool,
        /// <summary>
        /// Coin swaps
        /// </summary>
        [Map("7")]
        CoinSwaps,
        /// <summary>
        /// Investment
        /// </summary>
        [Map("8")]
        Investment,
        /// <summary>
        /// Borrow 
        /// </summary>
        [Map("9")]
        Borrow,
        /// <summary>
        /// Earn 
        /// </summary>
        [Map("10")]
        Earn,
        /// <summary>
        /// Usdt swaps
        /// </summary>
        [Map("11")]
        UsdtSwaps,
        /// <summary>
        /// Option
        /// </summary>
        [Map("12")]
        Option,
        /// <summary>
        /// Otc-options
        /// </summary>
        [Map("13")]
        OtcOptions,
        /// <summary>
        /// Crypto loans
        /// </summary>
        [Map("14")]
        CryptoLoans,
        /// <summary>
        /// Grid trading
        /// </summary>
        [Map("15")]
        GridTrading,
    }
}
