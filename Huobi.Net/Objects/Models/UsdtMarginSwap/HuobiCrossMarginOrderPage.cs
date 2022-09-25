using System;
using System.Collections.Generic;

namespace Huobi.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Order page
    /// </summary>
    public class HuobiCrossMarginOrderPage : HuobiPage
    {
        /// <summary>
        /// Orders
        /// </summary>
        public IEnumerable<HuobiCrossMarginOrder> Orders { get; set; } = Array.Empty<HuobiCrossMarginOrder>();
    }
}
