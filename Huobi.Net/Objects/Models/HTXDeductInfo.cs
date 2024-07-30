using System;
using System.Collections.Generic;
using System.Text;

namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Deduction info
    /// </summary>
    public record HTXDeductInfo
    {
        /// <summary>
        /// Point switch
        /// </summary>
        [JsonPropertyName("pointSwitch")]
        public bool PointSwitch { get; set; }
        /// <summary>
        /// Htx switch
        /// </summary>
        [JsonPropertyName("htxSwitch")]
        public bool HtxSwitch { get; set; }
        /// <summary>
        /// Asset
        /// </summary>
        [JsonPropertyName("currency")]
        public string Asset { get; set; } = string.Empty;
    }


}
