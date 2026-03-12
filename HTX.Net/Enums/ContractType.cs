using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Contract type
    /// </summary>
    [JsonConverter(typeof(EnumConverter<ContractType>))]
    public enum ContractType
    {
        /// <summary>
        /// ["<c>Swap</c>"] Swap
        /// </summary>
        [Map("Swap")]
        Swap,
        /// <summary>
        /// ["<c>this_week</c>"] This week
        /// </summary>
        [Map("this_week")]
        ThisWeek,
        /// <summary>
        /// ["<c>next_week</c>"] Next week
        /// </summary>
        [Map("next_week")]
        NextWeek,
        /// <summary>
        /// ["<c>quarter</c>"] Quarter
        /// </summary>
        [Map("quarter")]
        Quarter,
        /// <summary>
        /// ["<c>next_quarter</c>"] Next quarter
        /// </summary>
        [Map("next_quarter")]
        NextQuarter
    }
}
