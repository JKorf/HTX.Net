using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Sub account deduct mode
    /// </summary>
    public enum DeductMode
    {
        /// <summary>
        /// Deduct from master
        /// </summary>
        [Map("master")]
        Master,
        /// <summary>
        /// Deduct from sub
        /// </summary>
        [Map("sub")]
        Sub
    }
}
