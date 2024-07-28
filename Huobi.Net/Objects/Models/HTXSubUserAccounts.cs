using System;
using System.Collections.Generic;


namespace HTX.Net.Objects.Models
{
    /// <summary>
    /// HTX sub-user account info
    /// </summary>
    public record HTXSubUserAccounts
    {
        /// <summary>
        /// The id of the sub-user
        /// </summary>
        [JsonPropertyName("uid")]
        public long UserId { get; set; }

        /// <summary>
        /// Deduct mode
        /// </summary>
        public string DeductMode { get; set; } = string.Empty;

        /// <summary>
        /// List of accounts for the sub-user
        /// </summary>
        [JsonPropertyName("list")]
        public IEnumerable<HTXSubUserAccount> Accounts { get; set; } = Array.Empty<HTXSubUserAccount>();
    }
}
