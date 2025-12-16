using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Maintenance status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<MaintenanceStatus>))]
    public enum MaintenanceStatus
    {
        /// <summary>
        /// Scheduled
        /// </summary>
        [Map("scheduled")]
        Scheduled,
        /// <summary>
        /// In progress
        /// </summary>
        [Map("in progress")]
        InProgress,
        /// <summary>
        /// Verifying
        /// </summary>
        [Map("verifying")]
        Verifying,
        /// <summary>
        /// Completed
        /// </summary>
        [Map("completed")]
        Completed
    }
}
