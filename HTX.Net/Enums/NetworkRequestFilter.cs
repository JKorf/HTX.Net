using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace HTX.Net.Enums
{
    /// <summary>
    /// GetNetworksAsync filter
    /// </summary>
    public enum NetworkRequestFilter
    {
        /// <summary>
        /// Do not return descriptions
        /// </summary>
        [Map("0")]
        NoDescriptions,
        /// <summary>
        /// Include all descriptions
        /// </summary>
        [Map("1")]
        AllDescriptions,
        /// <summary>
        /// Only include suspended withdrawal/deposit descriptions
        /// </summary>
        [Map("2")]
        SuspendedDescriptions
    }
}
