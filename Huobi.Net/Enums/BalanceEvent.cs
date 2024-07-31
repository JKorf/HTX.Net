using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Order event
    /// </summary>
    public enum BalanceEvent
    {
        /// <summary>
        /// Open order
        /// </summary>
        [Map("order.open")]
        Open,
        /// <summary>
        /// Order match
        /// </summary>
        [Map("order.match")]
        Match,
        /// <summary>
        /// Settlement and delivery
        /// </summary>
        [Map("settlement")]
        Settlement,
        /// <summary>
        /// Order liquidation
        /// </summary>
        [Map("order.liquidation")]
        Liquidation,
        /// <summary>
        /// Order cancel
        /// </summary>
        [Map("order.cancel")]
        Cancel,
        /// <summary>
        /// Asset transfer
        /// </summary>
        [Map("contract.transfer")]
        Transfer,
        /// <summary>
        /// System
        /// </summary>
        [Map("contract.system")]
        System,
        /// <summary>
        /// Other
        /// </summary>
        [Map("other")]
        Other,
        /// <summary>
        /// Switch leverage
        /// </summary>
        [Map("switch_lever_rate")]
        SwitchLeverageRate,
        /// <summary>
        /// Initial margin
        /// </summary>
        [Map("init")]
        InitialMargin,
        /// <summary>
        /// Snapshot
        /// </summary>
        [Map("snapshot")]
        Snapshot
    }
}
