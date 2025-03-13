using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Business type
    /// </summary>
    public enum BusinessType
    {
        /// <summary>
        /// Futures
        /// </summary>
        [Map("futures")]
        Futures,
        /// <summary>
        /// Swap
        /// </summary>
        [Map("swap")]
        Swap,
        /// <summary>
        /// All (for filtering)
        /// </summary>
        [Map("all")]
        All
    }
}
