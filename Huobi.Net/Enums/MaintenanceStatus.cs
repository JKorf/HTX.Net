using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Maintenance status
    /// </summary>
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
