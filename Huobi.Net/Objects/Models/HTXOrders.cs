using System;
using System.Collections.Generic;

namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Orders info
    /// </summary>
    public record HTXOrders
	{
        /// <summary>
        /// Timestamp for pagination
        /// </summary>
        public DateTime NextTime { get; set; }
        /// <summary>
        /// List of ticks for symbols
        /// </summary>
        public IEnumerable<HTXOrder> Orders { get; set; } = Array.Empty<HTXOrder>();
    }
}
