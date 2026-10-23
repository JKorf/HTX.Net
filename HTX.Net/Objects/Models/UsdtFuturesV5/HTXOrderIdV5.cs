namespace HTX.Net.Objects.Models.UsdtFuturesV5
{
    /// <summary>
    /// Order id
    /// </summary>
    [SerializationModel]
    public record HTXOrderIdV5
    {
        /// <summary>
        /// ["<c>order_id</c>"] Order id
        /// </summary>
        [JsonPropertyName("order_id")]
        public string OrderId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>client_order_id</c>"] Client order id
        /// </summary>
        [JsonPropertyName("client_order_id")]
        public string? ClientOrderId { get; set; }
    }

    /// <summary>
    /// Order result
    /// </summary>
    public record HTXOrderResultV5 : HTXOrderIdV5
    {
        /// <summary>
        /// ["<c>message</c>"] Message
        /// </summary>
        [JsonPropertyName("message")]
        public string Message { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>code</c>"] Code
        /// </summary>
        [JsonPropertyName("code")]
        public int Code { get; set; }
    }
}
