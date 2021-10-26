namespace Huobi.Net.Enums
{
    /// <summary>
    /// Order state
    /// </summary>
    public enum OrderState
    {
        /// <summary>
        /// Pre-submitted
        /// </summary>
        PreSubmitted,
        /// <summary>
        /// Submitted, nothing filled yet
        /// </summary>
        Submitted,
        /// <summary>
        /// Partially filled
        /// </summary>
        PartiallyFilled,
        /// <summary>
        /// Partially filled, then canceled
        /// </summary>
        PartiallyCanceled,
        /// <summary>
        /// Filled completely
        /// </summary>
        Filled,
        /// <summary>
        /// Canceled without fill
        /// </summary>
        Canceled,
        /// <summary>
        /// Created
        /// </summary>
        Created,
        /// <summary>
        /// Rejected
        /// </summary>
        Rejected
    }
}
