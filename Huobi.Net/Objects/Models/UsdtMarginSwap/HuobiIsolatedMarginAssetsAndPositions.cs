using System;
using System.Collections.Generic;

namespace Huobi.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Isolated margin assets and positions info
    /// </summary>
    public record HuobiIsolatedMarginAssetsAndPositions: HuobiIsolatedMarginAccountInfo
    {
        /// <summary>
        /// Positions
        /// </summary>
        public IEnumerable<HuobiPosition>? Positions { get; set; } = Array.Empty<HuobiPosition>();
    }
}
