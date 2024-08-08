﻿using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Stop price operator
    /// </summary>
    public enum Operator
    {
        /// <summary>
        /// Greater than or equal to stop price
        /// </summary>
        [Map("gte")]
        GreaterThanOrEqual,
        /// <summary>
        /// Less than or equal to stop price
        /// </summary>
        [Map("lte")]
        LesserThanOrEqual
    }
}
