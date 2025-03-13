using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
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
        /// Spot account
        /// </summary>
        [Map("spot")]
        Spot,
        /// <summary>
        /// Margin account
        /// </summary>
        [Map("margin")]
        Margin,
        /// <summary>
        /// Super margin account
        /// </summary>
        [Map("super-margin")]
        SuperMargin,
        /// <summary>
        /// Otc account
        /// </summary>
        [Map("otc")]
        Otc,
        /// <summary>
        /// Point account
        /// </summary>
        [Map("point")]
        Point,
        /// <summary>
        /// Investment account
        /// </summary>
        [Map("investment")]
        Investment,
        /// <summary>
        /// Borrow account
        /// </summary>
        [Map("borrow")]
        Borrow,
        /// <summary>
        /// Grid trading
        /// </summary>
        [Map("grid-trading")]
        GridTrading,
        /// <summary>
        /// Deposit earning
        /// </summary>
        [Map("deposit-earning")]
        DepositEarning,
        /// <summary>
        /// Otc options
        /// </summary>
        [Map("otc-options")]
        OtcOptions,
        /// <summary>
        /// Minepool
        /// </summary>
        [Map("minepool")]
        Minepool,
        /// <summary>
        /// Etf
        /// </summary>
        [Map("etf")]
        Etf,
        /// <summary>
        /// Crypto loans
        /// </summary>
        [Map("crypto-loans")]
        CryptoLoans
    }
}
