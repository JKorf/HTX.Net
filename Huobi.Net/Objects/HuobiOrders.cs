using System;
using System.Collections.Generic;
using System.Text;

namespace Huobi.Net.Objects
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
        public IEnumerable<HuobiOrder> Orders { get; set; } = new List<HuobiOrder>();
    }
}
