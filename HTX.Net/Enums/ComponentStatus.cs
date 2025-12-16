using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Component status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<ComponentStatus>))]
    public enum ComponentStatus
    {
        /// <summary>
        /// Operational
        /// </summary>
        [Map("operational")]
        Operational,
        /// <summary>
        /// Degraded performance
        /// </summary>
        [Map("degraded_performance")]
        DegradedPerformance,
        /// <summary>
        /// Partial outage
        /// </summary>
        [Map("partial_outage")]
        PartialOutage,
        /// <summary>
        /// Major outage
        /// </summary>
        [Map("major_outage")]
        MajorOutage,
        /// <summary>
        /// Under maintance
        /// </summary>
        [Map("under maintenance")]
        UnderMaintenance
    }
}
