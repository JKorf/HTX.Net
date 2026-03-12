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
        /// ["<c>1</c>"] Spot
        /// </summary>
        [Map("1")]
        Spot,
        /// <summary>
        /// ["<c>2</c>"] Isolated 
        /// </summary>
        [Map("2")]
        IsolatedMargin,
        /// <summary>
        /// ["<c>3</c>"] Cross 
        /// </summary>
        [Map("3")]
        CrossMargin,
        /// <summary>
        /// ["<c>4</c>"] Coin futures
        /// </summary>
        [Map("4")]
        CoinFutures,
        /// <summary>
        /// ["<c>5</c>"] Flat 
        /// </summary>
        [Map("5")]
        Flat,
        /// <summary>
        /// ["<c>6</c>"] Minepool 
        /// </summary>
        [Map("6", "16")]
        Minepool,
        /// <summary>
        /// ["<c>7</c>"] Coin swaps
        /// </summary>
        [Map("7")]
        CoinSwaps,
        /// <summary>
        /// ["<c>8</c>"] Investment
        /// </summary>
        [Map("8")]
        Investment,
        /// <summary>
        /// ["<c>9</c>"] Borrow 
        /// </summary>
        [Map("9")]
        Borrow,
        /// <summary>
        /// ["<c>10</c>"] Earn 
        /// </summary>
        [Map("10")]
        Earn,
        /// <summary>
        /// ["<c>11</c>"] Usdt swaps
        /// </summary>
        [Map("11")]
        UsdtSwaps,
        /// <summary>
        /// ["<c>12</c>"] Option
        /// </summary>
        [Map("12")]
        Option,
        /// <summary>
        /// ["<c>13</c>"] Otc-options
        /// </summary>
        [Map("13")]
        OtcOptions,
        /// <summary>
        /// ["<c>14</c>"] Crypto loans
        /// </summary>
        [Map("14")]
        CryptoLoans,
        /// <summary>
        /// ["<c>15</c>"] Grid trading
        /// </summary>
        [Map("15")]
        GridTrading,
    }
}
