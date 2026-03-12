using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Point account status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<PointAccountStatus>))]
    public enum PointAccountStatus
    {
        /// <summary>
        /// ["<c>working</c>"] Working
        /// </summary>
        [Map("working")]
        Working,
        /// <summary>
        /// ["<c>lock</c>"] Lock
        /// </summary>
        [Map("lock")]
        Lock,
        /// <summary>
        /// ["<c>fl-sys</c>"] Fl sys
        /// </summary>
        [Map("fl-sys")]
        FlSys,
        /// <summary>
        /// ["<c>fl-mgt</c>"] Fl mgt
        /// </summary>
        [Map("fl-mgt")]
        FlMgt,
        /// <summary>
        /// ["<c>fl-end</c>"] Fl end
        /// </summary>
        [Map("fl-end")]
        FlEnd,
        /// <summary>
        /// ["<c>fl-negative</c>"] Fl negative
        /// </summary>
        [Map("fl-negative")]
        FlNegative,
    }

}
