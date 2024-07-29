using System;
using System.Collections.Generic;

namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Cross margin assets and positions info
    /// </summary>
    public record HTXCrossMarginAssetsAndPositions : HTXCrossMarginAccountInfo
    {
        /// <summary>
        /// Positions
        /// </summary>
        [JsonPropertyName("positions")]
        public IEnumerable<HTXPosition>? Positions { get; set; } = Array.Empty<HTXPosition>();
    }
}
