using System.Threading.Tasks;
using Huobi.Net.Objects;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.OrderBook;
using CryptoExchange.Net.Sockets;
using Huobi.Net.Interfaces;

namespace Huobi.Net
{
    /// <summary>
    /// Huobi order book implementation
    /// </summary>
    public class HuobiSymbolOrderBook : SymbolOrderBook
    {
        private readonly IHuobiSocketClient socketClient;
        private readonly int mergeStep;

        /// <summary>
        /// Create a new order book instance
        /// </summary>
        /// <param name="symbol">The symbol the order book is for</param>
        /// <param name="options">The options for the order book</param>
        public HuobiSymbolOrderBook(string symbol, HuobiOrderBookOptions? options = null) : base(symbol, options ?? new HuobiOrderBookOptions())
        {
            mergeStep = options?.MergeStep ?? 0;
            socketClient = options?.SocketClient ?? new HuobiSocketClient();
        }

        /// <inheritdoc />
        protected override async Task<CallResult<UpdateSubscription>> DoStart()
        {
            var subResult = await socketClient.SubscribeToOrderBookUpdatesAsync(Symbol, mergeStep, HandleUpdate).ConfigureAwait(false);
            if (!subResult)
                return subResult;

            var setResult = await WaitForSetOrderBook(10000).ConfigureAwait(false);
            return setResult ? subResult : new CallResult<UpdateSubscription>(null, setResult.Error);
        }

        private void HandleUpdate(HuobiOrderBook data)
        {
            SetInitialOrderBook(data.Timestamp.Ticks, data.Bids, data.Asks);
        }

        /// <inheritdoc />
        protected override async Task<CallResult<bool>> DoResync()
        {
            return await WaitForSetOrderBook(10000).ConfigureAwait(false);
        }

        /// <inheritdoc />
        public override void Dispose()
        {
            processBuffer.Clear();
            asks.Clear();
            bids.Clear();

            socketClient?.Dispose();
        }
    }
}
