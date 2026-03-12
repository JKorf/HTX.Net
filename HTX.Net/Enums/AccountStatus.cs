using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Account state
    /// </summary>
    [JsonConverter(typeof(EnumConverter<AccountStatus>))]
    public enum AccountStatus
    {
        /// <summary>
        /// ["<c>working</c>"] Working
        /// </summary>
        [Map("working", "normal")]
        Working,
        /// <summary>
        /// ["<c>lock</c>"] Locked
        /// </summary>
        [Map("lock")]
        Locked
    }
}
