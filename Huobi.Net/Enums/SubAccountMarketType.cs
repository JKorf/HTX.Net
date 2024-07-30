using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Market type
    /// </summary>
    public enum SubAccountMarketType
    {
        /// <summary>
        /// Isolated margin
        /// </summary>
        [Map("isolated-margin")]
        IsolatedMargin,
        /// <summary>
        /// Cross margin
        /// </summary>
        [Map("cross-margin")]
        CrossMargin
    }
}
