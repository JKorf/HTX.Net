using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using HTX.Net.Objects.Internal;

namespace HTX.Net.Objects.Sockets.Queries
{
    internal class HTXOpAuthQuery : Query<HTXOpResponse>
    {
        private readonly SocketApiClient _client;

        public HTXOpAuthQuery(SocketApiClient client, HTXAuthenticationRequest2 request) : base(request, false, 1)
        {
            _client = client;
            MessageMatcher = MessageMatcher.Create<HTXOpResponse>("auth", HandleMessage);
            MessageRouter = MessageRouter.Create<HTXOpResponse>("auth", HandleMessage);
        }

        public CallResult<HTXOpResponse> HandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, HTXOpResponse message)
        {
            if (message.ErrorCode != 0)
                return new CallResult<HTXOpResponse>(new ServerError(message.ErrorCode, _client.GetErrorInfo(message.ErrorCode, message.ErrorMessage!)), originalData);

            return new CallResult<HTXOpResponse>(message, originalData, null);
        }
    }
}
