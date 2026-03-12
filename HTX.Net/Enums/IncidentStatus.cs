using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Incident status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<IncidentStatus>))]
    public enum IncidentStatus
    {
        /// <summary>
        /// ["<c>investigating</c>"] Investigating
        /// </summary>
        [Map("investigating")]
        Investigating,
        /// <summary>
        /// ["<c>identified</c>"] Identified
        /// </summary>
        [Map("identified")]
        Identified,
        /// <summary>
        /// ["<c>monitoring</c>"] Monitoring
        /// </summary>
        [Map("monitoring")]
        Monitoring,
        /// <summary>
        /// ["<c>resolved</c>"] Resolved
        /// </summary>
        [Map("resolved")]
        Resolved
    }
}
