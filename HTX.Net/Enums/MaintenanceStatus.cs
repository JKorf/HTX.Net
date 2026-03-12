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
        /// ["<c>scheduled</c>"] Scheduled
        /// </summary>
        [Map("scheduled")]
        Scheduled,
        /// <summary>
        /// ["<c>in progress</c>"] In progress
        /// </summary>
        [Map("in progress")]
        InProgress,
        /// <summary>
        /// ["<c>verifying</c>"] Verifying
        /// </summary>
        [Map("verifying")]
        Verifying,
        /// <summary>
        /// ["<c>completed</c>"] Completed
        /// </summary>
        [Map("completed")]
        Completed
    }
}
