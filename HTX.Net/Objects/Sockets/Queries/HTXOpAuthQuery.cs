using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using HTX.Net.Objects.Internal;

namespace HTX.Net.Objects.Sockets.Queries
{
    internal class HTXOpAuthQuery : Query<HTXOpResponse>
    {
        public HTXOpAuthQuery(HTXAuthenticationRequest2 request) : base(request, false, 1)
        {
            MessageMatcher = MessageMatcher.Create<HTXOpResponse>("auth", HandleMessage);
        }

        public CallResult<HTXOpResponse> HandleMessage(SocketConnection connection, DataEvent<HTXOpResponse> message)
        {
            if (message.Data.ErrorCode != 0)
                return new CallResult<HTXOpResponse>(new ServerError(message.Data.ErrorCode, message.Data.ErrorMessage!));

            return message.ToCallResult();
        }
    }
}
