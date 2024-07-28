namespace HTX.Net.Objects.Internal
{
    internal abstract class HTXApiResponse
    {
        [JsonPropertyName("status")]
        internal string? Status { get; set; }


        [JsonPropertyName("err-msg")]
        internal string? ErrorMessage { get; set; }
        [JsonPropertyName("err_msg")]
        private string? ErrorMessageInternal
        {
            get => ErrorMessage;
            set => ErrorMessage = value;
        }
        [JsonPropertyName("err-code")]
        internal string? ErrorCode { get; set; }
        [JsonPropertyName("err_code")]
        private string? ErrorCodeInternal
        {
            get => ErrorCode;
            set => ErrorCode = value;
        }
        [JsonPropertyName("code")]
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
        [JsonPropertyName("next-time"), JsonConverter(typeof(DateTimeConverter))]
        private DateTime NextTime { get => Timestamp; set => Timestamp = value; }
    }

    internal class HTXBasicResponse<T> : HTXBasicResponse
    {
        public T Data { get; set; } = default!;
        [JsonPropertyName("tick")]
        private T Tick { set => Data = value; get => Data; }
        [JsonPropertyName("ticks")]
        private T Ticks { set => Data = value; get => Data; }
    }
}
