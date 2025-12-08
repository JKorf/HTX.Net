using HTX.Net.Enums;

namespace HTX.Net.Objects.Internal
{
    internal class HTXSocketPlaceOrderRequest
    {
        [JsonPropertyName("account-id")]
        public long AccountId { get; set; }
        [JsonPropertyName("price"), JsonConverter(typeof(DecimalStringWriterConverter)), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public decimal? Price { get; set; }
        [JsonPropertyName("amount"), JsonConverter(typeof(DecimalStringWriterConverter)), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public decimal? Quantity { get; set; }
        [JsonPropertyName("symbol")]
        public string Symbol { get; set; } = string.Empty;
        [JsonPropertyName("type")]
        public string Type { get; set; } = string.Empty;
        [JsonPropertyName("client-order-id"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string? ClientOrderId { get; set; }
        [JsonPropertyName("source"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public SourceType? SourceType { get; set; }
        [JsonPropertyName("stopPrice"), JsonConverter(typeof(DecimalStringWriterConverter)), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public decimal? StopPrice { get; set; }
        [JsonPropertyName("stopOperator"), JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public Operator? StopOperator { get; set; }
    }
}
