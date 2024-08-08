﻿using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Fee type
    /// </summary>
    public enum FeeType
    {
        /// <summary>
        /// Fixed
        /// </summary>
        [Map("fixed")]
        Fixed,
        /// <summary>
        /// Circulated
        /// </summary>
        [Map("circulated")]
        Circulated,
        /// <summary>
        /// Ratio
        /// </summary>
        [Map("ratio")]
        Ratio
    }
}
