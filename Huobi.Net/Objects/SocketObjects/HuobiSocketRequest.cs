using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Huobi.Net.Objects.SocketObjects
{
    public class HuobiRequest
    {
        [JsonIgnore]
        public bool Signed { get; set; }

        public string Id { get; set; }
    }

    public class HuobiSocketRequest: HuobiRequest
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

    public class HuobiAuthenticatedRequest: HuobiRequest
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

    public class HuobiSubscribeRequest: HuobiRequest
    {
        [JsonProperty("sub")]
        public string Topic { get; set; }
        [JsonProperty("id")]
        public string Id { get; set; }

        public HuobiSubscribeRequest(string topic, bool signed = false)
        {
            Topic = topic;
            Signed = signed;
        }
    }

    public class HuobiOrderListRequest: HuobiAuthenticatedRequest
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

    public class HuobiOrderDetailsRequest : HuobiAuthenticatedRequest
    {
        [JsonProperty("order-id")]
        public string OrderId { get; set; }

        public HuobiOrderDetailsRequest(string orderId): base("req", "orders.detail")
        {
            OrderId = orderId;
        }
    }
}
