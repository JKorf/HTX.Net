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
        /// ["<c>operational</c>"] Operational
        /// </summary>
        [Map("operational")]
        Operational,
        /// <summary>
        /// ["<c>degraded_performance</c>"] Degraded performance
        /// </summary>
        [Map("degraded_performance")]
        DegradedPerformance,
        /// <summary>
        /// ["<c>partial_outage</c>"] Partial outage
        /// </summary>
        [Map("partial_outage")]
        PartialOutage,
        /// <summary>
        /// ["<c>major_outage</c>"] Major outage
        /// </summary>
        [Map("major_outage")]
        MajorOutage,
        /// <summary>
        /// ["<c>under maintenance</c>"] Under maintance
        /// </summary>
        [Map("under maintenance")]
        UnderMaintenance
    }
}
