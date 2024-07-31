
namespace HTX.Net.Objects.Internal
{
    internal abstract class HTXResponse
    {
        internal abstract bool IsSuccessful { get; }
        [JsonPropertyName("id")]
        public string? Id { get; set; }
        [JsonPropertyName("err-code")]
        public string? ErrorCode { get; set; }
        [JsonPropertyName("err-msg")]
        public string? ErrorMessage { get; set; }
    }

    internal class HTXSocketResponse<T>: HTXResponse
    {
        internal override bool IsSuccessful => string.Equals(Status, "ok", StringComparison.Ordinal);
        [JsonPropertyName("status")] internal string Status { get; set; } = string.Empty;
        
        /// <summary>
        /// The timestamp of the data
        /// </summary>
        [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }

        /// <summary>
        /// The data
        /// </summary>
        [JsonPropertyName("data")]
        public T Data { get; set; } = default!;
        [JsonPropertyName("tick")]
        private T Tick { set => Data = value; get => Data; }
    }    
}
