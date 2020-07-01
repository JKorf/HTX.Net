using Newtonsoft.Json;

namespace Huobi.Net.Objects.SocketObjects
{
    internal class HuobiRequest
    {
        [JsonIgnore]
        public string? Id { get; set; }
    }

    internal class HuobiSocketRequest: HuobiRequest
    {
        [JsonProperty("req")]
        public string Request { get; set; }

        [JsonProperty("id")]
        public new string Id { get; set; }


        public HuobiSocketRequest(string id, string topic)
        {
            Id = id;
            Request = topic;
        }
    }

    internal class HuobiAuthenticatedRequest: HuobiRequest
    {
        [JsonProperty("op")]
        public string Operation { get; set; }
        [JsonProperty("topic")]
        public string Topic { get; set; }
        [JsonProperty("cid")]
        public new string Id { get; set; }

        public HuobiAuthenticatedRequest(string id, string operation, string topic)
        {
            Id = id;
            Operation = operation;
            Topic = topic;
        }
    }

    internal class HuobiSubscribeRequest: HuobiRequest
    {
        [JsonProperty("sub")]
        public string Topic { get; set; }
        [JsonProperty("id")]
        public new string Id { get; set; }

        public HuobiSubscribeRequest(string id, string topic)
        {
            Id = id;
            Topic = topic;
        }
    }

    internal class HuobiOrderListRequest: HuobiAuthenticatedRequest
    {
        [JsonProperty("symbol")]
        public string Symbol { get; set; }
        [JsonProperty("account-id")]
        public long AccountId { get; set; }
        [JsonProperty("states")]
        public string States { get; set; }
        [JsonProperty("types")]
        public string? Types { get; set; }
        [JsonProperty("start-date")]
        public string? StartTime { get; set; }
        [JsonProperty("end-date")]
        public string? EndTime { get; set; }
        [JsonProperty("from")]
        public string? FromId { get; set; }
        [JsonProperty("size")]
        public string? Limit { get; set; }

        public HuobiOrderListRequest(string id, long accountId, string symbol, string states): base(id, "req", "orders.list")
        {
            AccountId = accountId;
            Symbol = symbol;
            States = states;
        }
    }

    internal class HuobiOrderDetailsRequest : HuobiAuthenticatedRequest
    {
        [JsonProperty("order-id")]
        public string OrderId { get; set; }

        public HuobiOrderDetailsRequest(string id, string orderId): base(id, "req", "orders.detail")
        {
            OrderId = orderId;
        }
    }
}
