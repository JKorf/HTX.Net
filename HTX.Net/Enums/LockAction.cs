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
        /// ["<c>lock</c>"] Lock
        /// </summary>
        [Map("lock")]
        Lock,
        /// <summary>
        /// ["<c>normal</c>"] Normal
        /// </summary>
        [Map("normal")]
        Normal
    }
}
