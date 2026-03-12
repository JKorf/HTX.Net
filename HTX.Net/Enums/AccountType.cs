using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Account type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AccountType>))]
    public enum AccountType
    {
        /// <summary>
        /// ["<c>spot</c>"] Spot account
        /// </summary>
        [Map("spot")]
        Spot,
        /// <summary>
        /// ["<c>margin</c>"] Margin account
        /// </summary>
        [Map("margin")]
        Margin,
        /// <summary>
        /// ["<c>super-margin</c>"] Super margin account
        /// </summary>
        [Map("super-margin")]
        SuperMargin,
        /// <summary>
        /// ["<c>otc</c>"] Otc account
        /// </summary>
        [Map("otc")]
        Otc,
        /// <summary>
        /// ["<c>point</c>"] Point account
        /// </summary>
        [Map("point")]
        Point,
        /// <summary>
        /// ["<c>investment</c>"] Investment account
        /// </summary>
        [Map("investment")]
        Investment,
        /// <summary>
        /// ["<c>borrow</c>"] Borrow account
        /// </summary>
        [Map("borrow")]
        Borrow,
        /// <summary>
        /// ["<c>grid-trading</c>"] Grid trading
        /// </summary>
        [Map("grid-trading")]
        GridTrading,
        /// <summary>
        /// ["<c>deposit-earning</c>"] Deposit earning
        /// </summary>
        [Map("deposit-earning")]
        DepositEarning,
        /// <summary>
        /// ["<c>otc-options</c>"] Otc options
        /// </summary>
        [Map("otc-options")]
        OtcOptions,
        /// <summary>
        /// ["<c>minepool</c>"] Minepool
        /// </summary>
        [Map("minepool")]
        Minepool,
        /// <summary>
        /// ["<c>etf</c>"] Etf
        /// </summary>
        [Map("etf")]
        Etf,
        /// <summary>
        /// ["<c>crypto-loans</c>"] Crypto loans
        /// </summary>
        [Map("crypto-loans")]
        CryptoLoans
    }
}
