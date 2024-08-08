using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Tp/Sl order status
    /// </summary>
    public enum TpSlStatus
    {
        /// <summary>
        /// All (for filtering)
        /// </summary>
        [Map("0")]
        All,
        /// <summary>
        /// Not activated
        /// </summary>
        [Map("1")]
        NotActivated,
        /// <summary>
        /// Ready to submit
        /// </summary>
        [Map("2")]
        ReadyToSubmit,
        /// <summary>
        /// Submitting orders
        /// </summary>
        [Map("3")]
        Submitting,
        /// <summary>
        /// Orders successfully submited
        /// </summary>
        [Map("4")]
        SubmitSuccess,
        /// <summary>
        /// Orders failed to submit
        /// </summary>
        [Map("5")]
        SubmitFailed,
        /// <summary>
        /// Orders canceled
        /// </summary>
        [Map("6")]
        Canceled,
        /// <summary>
        /// Canceled orders not found
        /// </summary>
        [Map("8")]
        CanceledOrderNotFound,
        /// <summary>
        /// Orders canceling
        /// </summary>
        [Map("9")]
        Canceling,
        /// <summary>
        /// Failed
        /// </summary>
        [Map("10")]
        Failed,
        /// <summary>
        /// Expired
        /// </summary>
        [Map("11")]
        Expired,
        /// <summary>
        /// Not activated - Expired
        /// </summary>
        [Map("12")]
        NotActivatedExpired
    }
}
