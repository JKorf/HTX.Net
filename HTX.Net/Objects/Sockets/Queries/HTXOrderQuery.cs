using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using HTX.Net.Objects.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace HTX.Net.Objects.Sockets.Queries
{
    internal class HTXOrderQuery<TRequest, T> : Query<HTXSocketOrderResponse<T>>
    {
        private readonly SocketApiClient _client;

        public HTXOrderQuery(SocketApiClient client, HTXSocketOrderRequest<TRequest> request) : base(request, true, 1)
        {
            _client = client;
            MessageMatcher = MessageMatcher.Create<HTXSocketOrderResponse<T>>(request.RequestId, HandleMessage);
            MessageRouter = MessageRouter.CreateWithoutTopicFilter<HTXSocketOrderResponse<T>>(request.RequestId, HandleMessage);
        }

        public CallResult<HTXSocketOrderResponse<T>> HandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, HTXSocketOrderResponse<T> message)
        {
            if (!message.Success)
                return new CallResult<HTXSocketOrderResponse<T>>(new ServerError(message.ErrorCode!, _client.GetErrorInfo(message.ErrorCode!, message.ErrorMessage)));

            return new CallResult<HTXSocketOrderResponse<T>>(message, originalData, null);
        }
    }
}
