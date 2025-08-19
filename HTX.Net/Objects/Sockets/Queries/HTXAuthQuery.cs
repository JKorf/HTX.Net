using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;

namespace HTX.Net.Objects.Sockets.Queries
{
    internal class HTXAuthQuery : Query<HTXSocketAuthResponse>
    {
        private readonly SocketApiClient _client;

        public HTXAuthQuery(SocketApiClient client, string action, string topic, bool authenticated, int weight = 1) : base(new HTXAuthRequest() { Action = action, Channel = topic }, authenticated, weight)
        {
            _client = client;
            MessageMatcher = MessageMatcher.Create<HTXSocketAuthResponse>(action + topic, HandleMessage);
        }

        public HTXAuthQuery(SocketApiClient client, HTXAuthRequest request) : base(request, true, 1)
        {
            _client = client;
            MessageMatcher = MessageMatcher.Create<HTXSocketAuthResponse>(request.Action + request.Channel, HandleMessage);
        }

        public CallResult<HTXSocketAuthResponse> HandleMessage(SocketConnection connection, DataEvent<HTXSocketAuthResponse> message)
        {
            if (message.Data.Code != 200)
                return new CallResult<HTXSocketAuthResponse>(new ServerError(message.Data.Code, _client.GetErrorInfo(message.Data.Code, message.Data.Message!)));

            return message.ToCallResult();
        }
    }
}
