using Newtonsoft.Json;

namespace Huobi.Net.Objects.Internal
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

    internal class HuobiAuthenticatedSubscribeRequest
    {
        [JsonProperty("action")]
        public string Action { get; set; }
        [JsonProperty("ch")]
        public string Channel { get; set; }

        public HuobiAuthenticatedSubscribeRequest(string channel, string action = "sub")
        {
            Action = action;
            Channel = channel;
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

    internal class HuobiIncrementalOrderBookSubscribeRequest : HuobiSubscribeRequest
    {
        [JsonProperty("data_type")]
        public string DataType { get; set; }
        public HuobiIncrementalOrderBookSubscribeRequest(string id, string topic, string dataType) : base(id, topic) 
        { 
            DataType = dataType;
        }
    }
}
