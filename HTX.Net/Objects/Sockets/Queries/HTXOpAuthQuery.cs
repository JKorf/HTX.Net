using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using CryptoExchange.Net.Sockets.Default.Routing;
using HTX.Net.Objects.Internal;

namespace HTX.Net.Objects.Sockets.Queries
{
    internal class HTXOpAuthQuery : Query<HTXOpResponse>
    {
        private readonly SocketApiClient _client;

        public HTXOpAuthQuery(SocketApiClient client, HTXAuthenticationRequest2 request) : base(request, false, 1)
        {
            _client = client;
            MessageRouter = MessageRouter.CreateForQuery<HTXOpResponse>("auth", HandleMessage);
        }

        public CallResult<HTXOpResponse> HandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, HTXOpResponse message)
        {
            if (message.ErrorCode != 0)
                return CallResult.Fail<HTXOpResponse>(new ServerError(message.ErrorCode, _client.GetErrorInfo(message.ErrorCode, message.ErrorMessage!)), originalData);

            return CallResult<HTXOpResponse>.Ok(message, originalData);
        }
    }
}
