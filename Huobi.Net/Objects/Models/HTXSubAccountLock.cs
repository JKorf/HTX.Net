using HTX.Net.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// Sub account lock
    /// </summary>
    public record HTXSubAccountLock
    {
        /// <summary>
        /// Sub user id
        /// </summary>
        [JsonPropertyName("subUid")]
        public long SubUserId { get; set; }
        /// <summary>
        /// User lock state
        /// </summary>
        [JsonPropertyName("userState")]
        public LockAction UserState { get; set; }
    }


}
