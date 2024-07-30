using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Deduction switch type
    /// </summary>
    public enum DeductionSwitchType
    {
        /// <summary>
        /// Point card deduction
        /// </summary>
        [Map("0")]
        PointCardDeduction,
        /// <summary>
        /// Asset deduction
        /// </summary>
        [Map("1")]
        AssetDeduction,
        /// <summary>
        /// Close deduction
        /// </summary>
        [Map("2")]
        CloseDeduction
    }
}
