using CryptoExchange.Net.Attributes;

namespace HTX.Net.Enums
{
    /// <summary>
    /// Trigger order event
    /// </summary>
    [JsonConverter(typeof(EnumConverter<EventOrderTrigger>))]
    public enum EventOrderTrigger
    {
        /// <summary>
        /// ["<c>order</c>"] Trigger order placed
        /// </summary>
        [Map("order")]
        TriggerPlaced,
        /// <summary>
        /// ["<c>cancel</c>"] Trigger order canceled
        /// </summary>
        [Map("cancel")]
        TriggerCanceled,
        /// <summary>
        /// ["<c>trigger_success</c>"] Successfully triggered
        /// </summary>
        [Map("trigger_success")]
        TriggerSuccess,
        /// <summary>
        /// ["<c>trigger_fail</c>"] Failed to trigger
        /// </summary>
        [Map("trigger_fail")]
        TriggerFail
    }
}
