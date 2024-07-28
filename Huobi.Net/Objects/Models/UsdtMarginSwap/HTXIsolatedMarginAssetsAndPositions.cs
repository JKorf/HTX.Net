using System;
using System.Collections.Generic;

namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Isolated margin assets and positions info
    /// </summary>
    public record HTXIsolatedMarginAssetsAndPositions: HTXIsolatedMarginAccountInfo
    {
        /// <summary>
        /// Positions
        /// </summary>
        public IEnumerable<HTXPosition>? Positions { get; set; } = Array.Empty<HTXPosition>();
    }
}
