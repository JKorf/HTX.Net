using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Self match prevention
    /// </summary>
    [JsonConverter(typeof(EnumConverter<SelfMatchPrevent>))]
    public enum SelfMatchPrevent
    {
        /// <summary>
        /// ["<c>cancel_taker</c>"] Cancel taker order
        /// </summary>
        [Map("cancel_taker")]
        CancelTaker,
        /// <summary>
        /// ["<c>cancel_maker</c>"] Cancel maker order
        /// </summary>
        [Map("cancel_maker")]
        CancelMaker,
        /// <summary>
        /// ["<c>cancel_both</c>"] Cancel both orders
        /// </summary>
        [Map("cancel_both")]
        CancelBoth,
        /// <summary>
        /// ["<c>allow</c>"] Allow self-trading
        /// </summary>
        [Map("allow")]
        Allow
    }
}
