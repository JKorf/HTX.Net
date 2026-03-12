using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Order status
    /// </summary>
    [JsonConverter(typeof(EnumConverter<SwapMarginOrderStatus>))]
    public enum SwapMarginOrderStatus
    {
        /// <summary>
        /// ["<c>1</c>"] Ready to submit
        /// </summary>
        [Map("1")]
        ReadyToSubmit,
        /// <summary>
        /// ["<c>2</c>"] Submitting
        /// </summary>
        [Map("2")]
        Submitting,
        /// <summary>
        /// ["<c>3</c>"] Submitted / active
        /// </summary>
        [Map("3")]
        Submitted,
        /// <summary>
        /// ["<c>4</c>"] Partially filled
        /// </summary>
        [Map("4")]
        PartiallyFilled,
        /// <summary>
        /// ["<c>5</c>"] Partially filled, cancelled
        /// </summary>
        [Map("5")]
        PartiallyCancelled,
        /// <summary>
        /// ["<c>6</c>"] Filled
        /// </summary>
        [Map("6")]
        Filled,
        /// <summary>
        /// ["<c>7</c>"] Cancelled
        /// </summary>
        [Map("7")]
        Cancelled,
        /// <summary>
        /// ["<c>11</c>"] Cancelling
        /// </summary>
        [Map("11")]
        Cancelling
    }
}
