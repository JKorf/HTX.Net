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
        }

        public CallResult<HTXSocketOrderResponse<T>> HandleMessage(SocketConnection connection, DataEvent<HTXSocketOrderResponse<T>> message)
        {
            if (!message.Data.Success)
                return new CallResult<HTXSocketOrderResponse<T>>(new ServerError(message.Data.ErrorCode!, _client.GetErrorInfo(message.Data.ErrorCode!, message.Data.ErrorMessage)));

            return message.ToCallResult();
        }
    }
}
