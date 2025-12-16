using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Lock 
    /// </summary>
    [JsonConverter(typeof(EnumConverter<LockAction>))]
    public enum LockAction
    {
        /// <summary>
        /// Lock
        /// </summary>
        [Map("lock")]
        Lock,
        /// <summary>
        /// Normal
        /// </summary>
        [Map("normal")]
        Normal
    }
}
