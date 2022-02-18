using System;
using System.Collections.Generic;

namespace Huobi.Net.Objects.Models
{
    /// <summary>
    /// Orders info
    /// </summary>
    public class HuobiOrders
	{
        /// <summary>
        /// Timestamp for pagination
        /// </summary>
        public DateTime NextTime { get; set; }
        /// <summary>
        /// List of ticks for symbols
        /// </summary>
        public IEnumerable<HuobiOrder> Orders { get; set; } = Array.Empty<HuobiOrder>();
    }
}
