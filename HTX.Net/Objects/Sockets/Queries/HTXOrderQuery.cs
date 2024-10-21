using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using HTX.Net.Objects.Internal;
using System;
using System.Collections.Generic;
using System.Text;

namespace HTX.Net.Objects.Sockets.Queries
{
    internal class HTXOrderQuery<TRequest, TResponse> : Query<HTXSocketOrderResponse<TResponse>>
    {
        public override HashSet<string> ListenerIdentifiers { get; set; }

        public HTXOrderQuery(HTXSocketOrderRequest<TRequest> request) : base(request, true, 1)
        {
            ListenerIdentifiers = new HashSet<string> { request.RequestId };
        }

        public override CallResult<HTXSocketOrderResponse<TResponse>> HandleMessage(SocketConnection connection, DataEvent<HTXSocketOrderResponse<TResponse>> message)
        {
            if (!message.Data.Success)
                return new CallResult<HTXSocketOrderResponse<TResponse>>(new ServerError(message.Data.ErrorCode + ": " + message.Data.ErrorMessage));

            return base.HandleMessage(connection, message);
        }
    }
}
