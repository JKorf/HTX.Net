using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Type of network
    /// </summary>
    public enum NetworkType
    {
        /// <summary>
        /// Plain
        /// </summary>
        [Map("plain")]
        Plain,
        /// <summary>
        /// Live
        /// </summary>
        [Map("live")]
        Live,
        /// <summary>
        /// Old
        /// </summary>
        [Map("old")]
        Old,
        /// <summary>
        /// New
        /// </summary>
        [Map("new")]
        New,
        /// <summary>
        /// Legal
        /// </summary>
        [Map("legal")]
        Legal,
        /// <summary>
        /// Too old
        /// </summary>
        [Map("tooold")]
        TooOld
    }
}
