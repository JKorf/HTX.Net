using System;
using System.Collections.Generic;

namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Order page
    /// </summary>
    public record HTXIsolatedMarginOrderPage : HTXPage
    {
        /// <summary>
        /// Orders
        /// </summary>
        public IEnumerable<HTXIsolatedMarginOrder> Orders { get; set; } = Array.Empty<HTXIsolatedMarginOrder>();
    }
}
