using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Event trigger
    /// </summary>
    [JsonConverter(typeof(EnumConverter<EventTrigger>))]
    public enum EventTrigger
    {
        /// <summary>
        /// ["<c>order.open</c>"] Open order
        /// </summary>
        [Map("order.open")]
        Open,
        /// <summary>
        /// ["<c>order.match</c>"] Order match
        /// </summary>
        [Map("order.match")]
        Match,
        /// <summary>
        /// ["<c>settlement</c>"] Settlement and delivery
        /// </summary>
        [Map("settlement")]
        Settlement,
        /// <summary>
        /// ["<c>order.liquidation</c>"] Order liquidation
        /// </summary>
        [Map("order.liquidation")]
        Liquidation,
        /// <summary>
        /// ["<c>order.cancel</c>"] Order cancel
        /// </summary>
        [Map("order.cancel")]
        Cancel,
        /// <summary>
        /// ["<c>contract.transfer</c>"] Asset transfer
        /// </summary>
        [Map("contract.transfer")]
        Transfer,
        /// <summary>
        /// ["<c>contract.system</c>"] System
        /// </summary>
        [Map("contract.system")]
        System,
        /// <summary>
        /// ["<c>other</c>"] Other
        /// </summary>
        [Map("other")]
        Other,
        /// <summary>
        /// ["<c>switch_lever_rate</c>"] Switch leverage
        /// </summary>
        [Map("switch_lever_rate")]
        SwitchLeverageRate,
        /// <summary>
        /// ["<c>init</c>"] Initial update
        /// </summary>
        [Map("init")]
        Initial,
        /// <summary>
        /// ["<c>snapshot</c>"] Snapshot
        /// </summary>
        [Map("snapshot")]
        Snapshot,
        /// <summary>
        /// ["<c>order.close</c>"] Close order
        /// </summary>
        [Map("order.close")]
        Close
    }
}
