using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Price type
    /// </summary>
    public enum LightningPriceType
    {
        /// <summary>
        /// Market
        /// </summary>
        [Map("market")]
        Market,
        /// <summary>
        /// Fill or kill
        /// </summary>
        [Map("lightning_fok")]
        LightningFok,
        /// <summary>
        /// Immediate or cancel
        /// </summary>
        [Map("lightning_ioc")]
        LightningIoc
    }
}
