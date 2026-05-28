namespace HTX.Net.Objects.Models.UsdtFuturesV5
{
    /// <summary>
    /// Algo order id
    /// </summary>
    [SerializationModel]
    public record HTXAlgoOrderIdV5
    {
        /// <summary>
        /// ["<c>algo_id</c>"] Algo order id
        /// </summary>
        [JsonPropertyName("algo_id")]
        public string AlgoId { get; set; } = string.Empty;
        /// <summary>
        /// ["<c>algo_client_order_id</c>"] Algo client order id
        /// </summary>
        [JsonPropertyName("algo_client_order_id")]
        public string? AlgoClientOrderId { get; set; }
    }
}
