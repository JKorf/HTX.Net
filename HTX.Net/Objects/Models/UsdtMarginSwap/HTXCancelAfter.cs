namespace HTX.Net.Objects.Models.UsdtMarginSwap
{
    /// <summary>
    /// Cancel after status
    /// </summary>
    [SerializationModel]
    public record HTXCancelAfter
    {
        /// <summary>
        /// ["<c>current_time</c>"] Current time
        /// </summary>
        [JsonPropertyName("current_time")]
        public DateTime CurrentTime { get; set; }
        /// <summary>
        /// ["<c>trigger_time</c>"] Trigger time
        /// </summary>
        [JsonPropertyName("trigger_time")]
        public DateTime? TriggerTime { get; set; }
    }
}
