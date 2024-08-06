namespace HTX.Net.Objects.Internal
{
    internal abstract class HTXApiResponse
    {
        [JsonInclude, JsonPropertyName("status")]
        internal string? Status { get; set; }


        [JsonInclude, JsonPropertyName("err-msg")]
        internal string? ErrorMessage { get; set; }
        [JsonInclude, JsonPropertyName("err_msg")]
        private string? ErrorMessageInternal
        {
            get => ErrorMessage;
            set => ErrorMessage = value;
        }
        [JsonInclude, JsonPropertyName("err-code"), JsonConverter(typeof(NumberStringConverter))]
        internal string? ErrorCode { get; set; }
        [JsonInclude, JsonPropertyName("err_code"), JsonConverter(typeof(NumberStringConverter))]
        private string? ErrorCodeInternal
        {
            get => ErrorCode;
            set => ErrorCode = value;
        }
        [JsonInclude, JsonPropertyName("code"), JsonConverter(typeof(NumberStringConverter))]
        private string? ErrorCodeInternal2
        {
            get => ErrorCode;
            set => ErrorCode = value;
        }
    }

    internal class HTXBasicResponse : HTXApiResponse
    {
        [JsonPropertyName("ts"), JsonConverter(typeof(DateTimeConverter))]
        public DateTime Timestamp { get; set; }
        [JsonPropertyName("ch")]
        public string Channel { get; set; } = string.Empty;
        [JsonInclude, JsonPropertyName("next-time"), JsonConverter(typeof(DateTimeConverter))]
        private DateTime NextTime { get => Timestamp; set => Timestamp = value; }
    }

    internal class HTXBasicResponse<T> : HTXBasicResponse
    {
        [JsonPropertyName("data")]
        public T Data { get; set; } = default!;
        [JsonInclude, JsonPropertyName("tick")]
        private T Tick { set => Data = value; get => Data; }
        [JsonInclude, JsonPropertyName("ticks")]
        private T Ticks { set => Data = value; get => Data; }
    }
}
