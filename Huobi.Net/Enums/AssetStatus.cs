using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Asset status
    /// </summary>
    public enum AssetStatus
    {
        /// <summary>
        /// Normal
        /// </summary>
        [Map("normal")]
        Normal,
        /// <summary>
        /// Delisted
        /// </summary>
        [Map("delisted")]
        Delisted
    }
}
