using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Network action status
    /// </summary>
    public enum NetworkStatus
    {
        /// <summary>
        /// Allowed
        /// </summary>
        [Map("allowed")]
        Allowed,
        /// <summary>
        /// Prohibited
        /// </summary>
        [Map("prohibited")]
        Prohibited
    }
}
