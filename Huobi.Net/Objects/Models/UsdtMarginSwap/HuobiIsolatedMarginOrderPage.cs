using System;
using System.Collections.Generic;

namespace Huobi.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Order page
    /// </summary>
    public class HuobiIsolatedMarginOrderPage : HuobiPage
    {
        /// <summary>
        /// Orders
        /// </summary>
        public IEnumerable<HuobiIsolatedMarginOrder> Orders { get; set; } = Array.Empty<HuobiIsolatedMarginOrder>();
    }
}
