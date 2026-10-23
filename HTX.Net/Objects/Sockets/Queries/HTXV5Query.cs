using CryptoExchange.Net.Clients;
using CryptoExchange.Net.Sockets;
using CryptoExchange.Net.Sockets.Default;
using CryptoExchange.Net.Sockets.Default.Routing;
using HTX.Net.Objects.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HTX.Net.Objects.Sockets.Queries
{
    internal class HTXV5Query<T> : Query<T>
    {
        private readonly SocketApiClient _client;

        public HTXV5Query(SocketApiClient client, HTXV5Request request, bool authenticated, int weight = 1) : base(request, authenticated, weight)
        {
            _client = client;

            MessageRouter = MessageRouter.CreateForQuery<HTXV5Response<T>, T>(request.RequestId, HandleMessage);
        }

        private CallResult<T>? HandleMessage(SocketConnection connection, DateTime receiveTime, string? originalData, HTXV5Response<T> message)
        {
            if (message.Code != 200)
                return CallResult.Fail<T>(new ServerError(message.Code, _client.GetErrorInfo(message.Code, message.Message)), originalData);

            return CallResult.Ok<T>(message.Data, originalData);
        }
    }
}
