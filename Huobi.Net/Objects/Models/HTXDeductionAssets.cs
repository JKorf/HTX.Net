using System;
using System.Collections.Generic;
using System.Text;

namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// 
    /// </summary>
    public record HTXDeductionAssets
    {
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Assets { get; set; } = string.Empty;
    }
}
