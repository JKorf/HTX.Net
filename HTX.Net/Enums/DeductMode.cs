using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Sub account deduct mode
    /// </summary>
    [JsonConverter(typeof(EnumConverter<DeductMode>))]
    public enum DeductMode
    {
        /// <summary>
        /// ["<c>master</c>"] Deduct from master
        /// </summary>
        [Map("master")]
        Master,
        /// <summary>
        /// ["<c>sub</c>"] Deduct from sub
        /// </summary>
        [Map("sub")]
        Sub
    }
}
