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
        /// Trigger order placed
        /// </summary>
        [Map("order")]
        TriggerPlaced,
        /// <summary>
        /// Trigger order canceled
        /// </summary>
        [Map("cancel")]
        TriggerCanceled,
        /// <summary>
        /// Successfully triggered
        /// </summary>
        [Map("trigger_success")]
        TriggerSuccess,
        /// <summary>
        /// Failed to trigger
        /// </summary>
        [Map("trigger_fail")]
        TriggerFail
    }
}
