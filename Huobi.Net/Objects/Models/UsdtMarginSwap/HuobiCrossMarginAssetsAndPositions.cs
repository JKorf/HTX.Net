using System;
using System.Collections.Generic;

namespace Huobi.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Cross margin assets and positions info
    /// </summary>
    public class HuobiCrossMarginAssetsAndPositions : HuobiCrossMarginAccountInfo
    {
        /// <summary>
        /// Positions
        /// </summary>
        public IEnumerable<HuobiPosition>? Positions { get; set; } = Array.Empty<HuobiPosition>();
    }
}
