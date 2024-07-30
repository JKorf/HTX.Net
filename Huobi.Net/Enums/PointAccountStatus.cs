using CryptoExchange.Net.Attributes;
using System;
using System.Collections.Generic;
using System.Text;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Point account status
    /// </summary>
    public enum PointAccountStatus
    {
        /// <summary>
        /// Working
        /// </summary>
        [Map("working")]
        Working,
        /// <summary>
        /// Lock
        /// </summary>
        [Map("lock")]
        Lock,
        /// <summary>
        /// Fl sys
        /// </summary>
        [Map("fl-sys")]
        FlSys,
        /// <summary>
        /// Fl mgt
        /// </summary>
        [Map("fl-mgt")]
        FlMgt,
        /// <summary>
        /// Fl end
        /// </summary>
        [Map("fl-end")]
        FlEnd,
        /// <summary>
        /// Fl negative
        /// </summary>
        [Map("fl-negative")]
        FlNegative,
    }

}
