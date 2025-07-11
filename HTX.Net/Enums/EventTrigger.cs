using System.Text.Json.Serialization;
using CryptoExchange.Net.Converters.SystemTextJson;
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
        /// Initial update
        /// </summary>
        [Map("init")]
        Initial,
        /// <summary>
        /// Snapshot
        /// </summary>
        [Map("snapshot")]
        Snapshot
    }
}
