using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Trigger type
    /// </summary>
    public enum TriggerType
    {
        /// <summary>
        /// Greater than or equal to price
        /// </summary>
        [Map("ge")]
        GreaterThanOrEqual,
        /// <summary>
        /// Lesser than or equal to price
        /// </summary>
        [Map("le")]
        LesserThanOrEqual
    }
}
