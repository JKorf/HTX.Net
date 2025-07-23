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
        public HTXOrderQuery(HTXSocketOrderRequest<TRequest> request) : base(request, true, 1)
        {
            MessageMatcher = MessageMatcher.Create<HTXSocketOrderResponse<T>>(request.RequestId, HandleMessage);
        }

        public CallResult<HTXSocketOrderResponse<T>> HandleMessage(SocketConnection connection, DataEvent<HTXSocketOrderResponse<T>> message)
        {
            if (!message.Data.Success)
                return new CallResult<HTXSocketOrderResponse<T>>(new ServerError(message.Data.ErrorCode + ": " + message.Data.ErrorMessage));

            return message.ToCallResult();
        }
    }
}
