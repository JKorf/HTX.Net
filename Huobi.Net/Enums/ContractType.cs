using CryptoExchange.Net.Attributes;

namespace Huobi.Net.Enums
{
    /// <summary>
    /// Contract type
    /// </summary>
    public enum ContractType
    {
        /// <summary>
        /// Swap
        /// </summary>
        [Map("Swap")]
        Swap,
        /// <summary>
        /// This week
        /// </summary>
        [Map("this_week")]
        ThisWeek,
        /// <summary>
        /// Next week
        /// </summary>
        [Map("next_week")]
        NextWeek,
        /// <summary>
        /// Quarter
        /// </summary>
        [Map("quarter")]
        Quarter,
        /// <summary>
        /// Next quarter
        /// </summary>
        [Map("next_quarter")]
        NextQuarter
    }
}
