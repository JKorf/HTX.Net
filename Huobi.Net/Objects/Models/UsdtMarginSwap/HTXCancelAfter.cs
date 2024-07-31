using System;
using System.Collections.Generic;
using System.Text;

namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Cancel after status
    /// </summary>
    public record HTXCancelAfter
    {
        /// <summary>
        /// Current time
        /// </summary>
        [JsonPropertyName("current_time")]
        public DateTime CurrentTime { get; set; }
        /// <summary>
        /// Trigger time
        /// </summary>
        [JsonPropertyName("trigger_time")]
        public DateTime TriggerTime { get; set; }
    }
}
