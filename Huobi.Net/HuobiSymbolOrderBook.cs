using System.Threading.Tasks;
using Huobi.Net.Objects;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.OrderBook;
using CryptoExchange.Net.Sockets;
using Huobi.Net.Interfaces;
using System;

namespace Huobi.Net
{
    /// <summary>
    /// Huobi order book implementation
    /// </summary>
    public class HuobiSymbolOrderBook : SymbolOrderBook
    {
        private readonly IHuobiSocketClient socketClient;
        private readonly int? mergeStep;
        private int? _levels;
        private bool _socketOwner;

        /// <summary>
        /// Create a new order book instance
        /// </summary>
        /// <param name="symbol">The symbol the order book is for</param>
        /// <param name="options">The options for the order book</param>
        public HuobiSymbolOrderBook(string symbol, HuobiOrderBookOptions? options = null) : base(symbol, options ?? new HuobiOrderBookOptions())
        {
            mergeStep = options?.MergeStep;
            _levels = options?.Levels;

            if (_levels != 150 && mergeStep != null)
                throw new ArgumentException("Mergestep only supported with 150 levels");

            if (_levels == null && mergeStep == null)
                throw new ArgumentException("Levels need to be set when MergeStep is not set");
            
            socketClient = options?.SocketClient ?? new HuobiSocketClient();
            _socketOwner = options?.SocketClient == null;
        }

        /// <inheritdoc />
        protected override async Task<CallResult<UpdateSubscription>> DoStartAsync()
        {
            if(mergeStep != null)
            {
                var subResult = await socketClient.SubscribeToPartialOrderBookUpdates1SecondAsync(Symbol, mergeStep.Value, HandleUpdate).ConfigureAwait(false);
                if (!subResult)
                    return subResult;

                Status = OrderBookStatus.Syncing;
                var setResult = await WaitForSetOrderBookAsync(10000).ConfigureAwait(false);
                if (!setResult)
                    await subResult.Data.CloseAsync().ConfigureAwait(false);

                return setResult ? subResult : new CallResult<UpdateSubscription>(null, setResult.Error);
            }
            else
            {
                var subResult = await socketClient.SubscribeToOrderBookChangeUpdatesAsync(Symbol, _levels!.Value, HandleIncremental).ConfigureAwait(false);
                if (!subResult)
                    return subResult;

                // Wait a little so that the sequence number of the order book snapshot is higher than the first socket update sequence number
                await Task.Delay(500).ConfigureAwait(false);
                var book = await socketClient.GetOrderBookAsync(Symbol, _levels.Value).ConfigureAwait(false);
                if (!book) 
                {
                    log.Write(Microsoft.Extensions.Logging.LogLevel.Debug, $"{Id} order book {Symbol} failed to retrieve initial order book");
                    await socketClient.UnsubscribeAsync(subResult.Data).ConfigureAwait(false);
                    return new CallResult<UpdateSubscription>(null, book.Error);
                }

                SetInitialOrderBook(book.Data.SequenceNumber, book.Data.Bids, book.Data.Asks);
                return subResult;
            }            
        }

        private void HandleIncremental(DataEvent<HuobiIncementalOrderBook> book)
        {
            if(book.Data.PreviousSequenceNumber != null)
                UpdateOrderBook(book.Data.PreviousSequenceNumber.Value, book.Data.SequenceNumber, book.Data.Bids, book.Data.Asks);
            else
                UpdateOrderBook(book.Data.SequenceNumber, book.Data.Bids, book.Data.Asks);
        }

        private void HandleUpdate(DataEvent<HuobiOrderBook> data)
        {
            SetInitialOrderBook(data.Data.Timestamp.Ticks, data.Data.Bids, data.Data.Asks);
        }

        /// <inheritdoc />
        protected override async Task<CallResult<bool>> DoResyncAsync()
        {
            if (mergeStep != null)
            {
                return await WaitForSetOrderBookAsync(10000).ConfigureAwait(false);
            }
            else
            {
                // Wait a little so that the sequence number of the order book snapshot is higher than the first socket update sequence number
                await Task.Delay(500).ConfigureAwait(false);
                var book = await socketClient.GetOrderBookAsync(Symbol, _levels!.Value).ConfigureAwait(false);
                if (!book)
                    return new CallResult<bool>(false, book.Error);                

                SetInitialOrderBook(book.Data.SequenceNumber, book.Data.Bids!, book.Data.Asks!);
                return new CallResult<bool>(true, null);
            }
        }

        /// <inheritdoc />
        public override void Dispose()
        {
            processBuffer.Clear();
            asks.Clear();
            bids.Clear();

            if(_socketOwner)
                socketClient?.Dispose();
        }
    }
}
