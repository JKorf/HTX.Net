
namespace HTX.Net.Objects.Internal
{
    internal abstract class HTXResponse
    {
        internal abstract bool IsSuccessful { get; }
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
        public T Data { get; set; } = default!;
        [JsonPropertyName("tick")]
        private T Tick { set => Data = value; get => Data; }
    }

    internal class HTXSubscribeResponse: HTXResponse
    {
        internal override bool IsSuccessful => string.Equals(Status, "ok", StringComparison.Ordinal);
        public string Status { get; set; } = string.Empty;
        public string Subbed { get; set; } = string.Empty;
        [JsonConverter(typeof(DateTimeConverter)), JsonPropertyName("ts")]
        public DateTime Timestamp { get; set; }
    }

    internal class HTXAuthSubscribeResponse
    {
        internal bool IsSuccessful => Code == 200;
        public int Code { get; set; }
        public string Message { get; set; } = string.Empty;
        [JsonPropertyName("ch")]
        public string Channel { get; set; } = string.Empty;
        public string Action { get; set; } = string.Empty;
    }
    
}
