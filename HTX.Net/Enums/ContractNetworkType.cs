using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Contract network type
    /// </summary>
    public enum ContractNetworkType
    {
        /// <summary>
        /// Coin
        /// </summary>
        [Map("0")]
        Coin,
        /// <summary>
        /// Token
        /// </summary>
        [Map("1")]
        Token
    }
}
