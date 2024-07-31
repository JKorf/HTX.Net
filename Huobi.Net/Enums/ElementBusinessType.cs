using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Business type
    /// </summary>
    public enum ElementBusinessType
    {
        /// <summary>
        /// Perpetual futures
        /// </summary>
        [Map("1")]
        PerpetualFutures,
        /// <summary>
        /// Delivery futures
        /// </summary>
        [Map("2")]
        DeliveryFutures,
        /// <summary>
        /// Perpetual + Delivery futures
        /// </summary>
        [Map("3")]
        PerpetualAndDeliveryFutures
    }
}
