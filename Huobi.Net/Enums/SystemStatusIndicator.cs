using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace HTX.Net.Enums
{
    /// <summary>
    /// System status indicator
    /// </summary>
    public enum SystemStatusIndicator
    {
        /// <summary>
        /// None
        /// </summary>
        [Map("none")]
        None,
        /// <summary>
        /// Minor
        /// </summary>
        [Map("minor")]
        Minor,
        /// <summary>
        /// Major
        /// </summary>
        [Map("major")]
        Major,
        /// <summary>
        /// Critical
        /// </summary>
        [Map("critical")]
        Critical,
        /// <summary>
        /// Maintenance
        /// </summary>
        [Map("maintenance")]
        Maintenance
    }
}
