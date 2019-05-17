using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Huobi.Net.Objects;
using CryptoExchange.Net.Logging;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.OrderBook;
using CryptoExchange.Net.Sockets;

namespace Huobi.Net
{
    public class HuobiSymbolOrderBook : SymbolOrderBook
    {
        private readonly HuobiSocketClient socketClient;
        private int mergeStep = 0;

        public HuobiSymbolOrderBook(string symbol, int mergeStep = 0, LogVerbosity logVerbosity = LogVerbosity.Info, IEnumerable<TextWriter> logWriters = null) : base("Huobi", symbol, false, logVerbosity, logWriters)
        {
            socketClient = new HuobiSocketClient();
        }

        protected override async Task<CallResult<UpdateSubscription>> DoStart()
        {
            return await socketClient.SubscribeToMarketDepthUpdatesAsync(Symbol, mergeStep, HandleUpdate).ConfigureAwait(false);
        }

        private void HandleUpdate(HuobiMarketDepth data)
        {
            SetInitialOrderBook(data.Timestamp.Ticks, data.Asks, data.Bids);
        }

        protected override Task<CallResult<bool>> DoResync()
        {
            return Task.FromResult(new CallResult<bool>(true, null));
        }

        public override void Dispose()
        {
            processBuffer.Clear();
            asks.Clear();
            bids.Clear();

            socketClient?.Dispose();
        }
    }
}
