﻿using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Filter direction
    /// </summary>
    public enum FilterDirection
    {
        /// <summary>
        /// Get results after
        /// </summary>
        [Map("next")]
        Next,
        /// <summary>
        /// Get results before
        /// </summary>
        [Map("prev")]
        Previous
    }
}
