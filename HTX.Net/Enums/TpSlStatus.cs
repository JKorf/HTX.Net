using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Tp/Sl order status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<TpSlStatus>))]
    public enum TpSlStatus
    {
        /// <summary>
        /// ["<c>0</c>"] All (for filtering)
        /// </summary>
        [Map("0")]
        All,
        /// <summary>
        /// ["<c>1</c>"] Not activated
        /// </summary>
        [Map("1")]
        NotActivated,
        /// <summary>
        /// ["<c>2</c>"] Ready to submit
        /// </summary>
        [Map("2")]
        ReadyToSubmit,
        /// <summary>
        /// ["<c>3</c>"] Submitting orders
        /// </summary>
        [Map("3")]
        Submitting,
        /// <summary>
        /// ["<c>4</c>"] Orders successfully submited
        /// </summary>
        [Map("4")]
        SubmitSuccess,
        /// <summary>
        /// ["<c>5</c>"] Orders failed to submit
        /// </summary>
        [Map("5")]
        SubmitFailed,
        /// <summary>
        /// ["<c>6</c>"] Orders canceled
        /// </summary>
        [Map("6")]
        Canceled,
        /// <summary>
        /// ["<c>8</c>"] Canceled orders not found
        /// </summary>
        [Map("8")]
        CanceledOrderNotFound,
        /// <summary>
        /// ["<c>9</c>"] Orders canceling
        /// </summary>
        [Map("9")]
        Canceling,
        /// <summary>
        /// ["<c>10</c>"] Failed
        /// </summary>
        [Map("10")]
        Failed,
        /// <summary>
        /// ["<c>11</c>"] Expired
        /// </summary>
        [Map("11")]
        Expired,
        /// <summary>
        /// ["<c>12</c>"] Not activated - Expired
        /// </summary>
        [Map("12")]
        NotActivatedExpired
    }
}
