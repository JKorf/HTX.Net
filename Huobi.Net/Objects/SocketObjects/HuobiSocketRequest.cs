using Newtonsoft.Json;

namespace Huobi.Net.Objects.SocketObjects
{
    internal class HuobiRequest
    {
        [JsonIgnore]
        public bool Signed { get; set; }

        [JsonIgnore]
        public string Id { get; set; }
    }

    internal class HuobiSocketRequest: HuobiRequest
    {
        [JsonProperty("req")]
        public string Request { get; set; }

        [JsonProperty("id")]
        public new string Id { get; set; }


        public HuobiSocketRequest(string topic)
        {
            Request = topic;
            Signed = false;
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

        public HuobiAuthenticatedRequest(string operation, string topic)
        {
            Operation = operation;
            Topic = topic;
            Signed = true;
        }
    }

    internal class HuobiSubscribeRequest: HuobiRequest
    {
        [JsonProperty("sub")]
        public string Topic { get; set; }
        [JsonProperty("id")]
        public new string Id { get; set; }

        public HuobiSubscribeRequest(string topic, bool signed = false)
        {
            Topic = topic;
            Signed = signed;
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
        public string Types { get; set; }
        [JsonProperty("start-date")]
        public string StartTime { get; set; }
        [JsonProperty("end-date")]
        public string EndTime { get; set; }
        [JsonProperty("from")]
        public string FromId { get; set; }
        [JsonProperty("size")]
        public string Limit { get; set; }

        public HuobiOrderListRequest(long accountId, string symbol, string states): base("req", "orders.list")
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

        public HuobiOrderDetailsRequest(string orderId): base("req", "orders.detail")
        {
            OrderId = orderId;
        }
    }
}
