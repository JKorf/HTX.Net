using System.Threading.Tasks;
using Huobi.Net.Objects;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.OrderBook;
using CryptoExchange.Net.Sockets;
using Huobi.Net.Interfaces;

namespace Huobi.Net
{
    public class HuobiSymbolOrderBook : SymbolOrderBook
    {
        private readonly IHuobiSocketClient socketClient;
        private readonly int mergeStep;

        /// <summary>
        /// Create a new order book instance
        /// </summary>
        /// <param name="symbol">The symbol the order book is for</param>
        /// <param name="options">The options for the order book</param>
        public HuobiSymbolOrderBook(string symbol, HuobiOrderBookOptions options = null) : base(symbol, options ?? new HuobiOrderBookOptions())
        {
            mergeStep = options?.MergeStep ?? 0;
            socketClient = options?.SocketClient ?? new HuobiSocketClient();
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
