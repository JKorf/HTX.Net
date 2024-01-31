using CryptoExchange.Net.Objects;
using CryptoExchange.Net.Objects.Sockets;
using CryptoExchange.Net.Sockets;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Huobi.Net.Objects.Sockets.Queries
{
    internal class HuobiAuthQuery : Query<HuobiSocketAuthResponse>
    {
        public override HashSet<string> ListenerIdentifiers { get; set; }
        public HuobiAuthQuery(string action, string topic, bool authenticated, int weight = 1) : base(new HuobiAuthRequest() { Action = action, Channel = topic }, authenticated, weight)
        {
            ListenerIdentifiers = new HashSet<string> { action + topic };
        }
        public HuobiAuthQuery(HuobiAuthRequest request) : base(request, true, 1)
        {
            ListenerIdentifiers = new HashSet<string> { request.Action + request.Channel };
        }

        public override Task<CallResult<HuobiSocketAuthResponse>> HandleMessageAsync(SocketConnection connection, DataEvent<HuobiSocketAuthResponse> message)
        {
            if (message.Data.Code != 200)
                return Task.FromResult(new CallResult<HuobiSocketAuthResponse>(new ServerError(message.Data.Code, message.Data.Message!)));

            return base.HandleMessageAsync(connection, message);
        }
    }
}
