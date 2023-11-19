using CryptoExchange.Net;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Huobi.Net.Objects.Sockets.Queries
{
    internal class HuobiAuthQuery : Query<HuobiSocketAuthResponse>
    {
        public override List<string> Identifiers { get; }
        public HuobiAuthQuery(string action, string topic, bool authenticated, int weight = 1) : base(new HuobiAuthRequest() { Action = action, Channel = topic }, authenticated, weight)
        {
            Identifiers = new List<string> { action + topic };
        }
        public HuobiAuthQuery(HuobiAuthRequest request) : base(request, true, 1)
        {
            Identifiers = new List<string> { request.Action + request.Channel };
        }

        public override Task<CallResult<HuobiSocketAuthResponse>> HandleMessageAsync(SocketConnection connection, DataEvent<ParsedMessage<HuobiSocketAuthResponse>> message)
        {
            if (message.Data.TypedData.Code != 200)
                return Task.FromResult(new CallResult<HuobiSocketAuthResponse>(new ServerError(message.Data.TypedData.Code, message.Data.TypedData.Message)));

            return base.HandleMessageAsync(connection, message);
        }
    }
}
