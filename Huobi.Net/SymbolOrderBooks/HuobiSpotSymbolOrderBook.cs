using System.Threading.Tasks;
using CryptoExchange.Net.Objects;
using CryptoExchange.Net.OrderBook;
using CryptoExchange.Net.Sockets;
using System;
using Huobi.Net.Objects;
using Huobi.Net.Objects.Models;
using Huobi.Net.Interfaces.Clients;
using Huobi.Net.Clients;
using System.Threading;

namespace Huobi.Net.SymbolOrderBooks
{
    /// <summary>
    /// Huobi order book implementation
    /// </summary>
    public class HuobiSpotSymbolOrderBook : SymbolOrderBook
    {
        private readonly IHuobiSocketClient _socketClient;
        private readonly int? _mergeStep;
        private readonly int? _levels;
        private readonly bool _socketOwner;

        /// <summary>
        /// Create a new order book instance
        /// </summary>
        /// <param name="symbol">The symbol the order book is for</param>
        /// <param name="options">The options for the order book</param>
        public HuobiSpotSymbolOrderBook(string symbol, HuobiOrderBookOptions? options = null) : base("Huobi", symbol, options ?? new HuobiOrderBookOptions())
        {
            _mergeStep = options?.MergeStep;
            _levels = options?.Levels;
            strictLevels = false;
            sequencesAreConsecutive = _levels != null;

            if (_levels != 150 && _mergeStep != null)
                throw new ArgumentException("Mergestep only supported with 150 levels");

            if (_levels == null && _mergeStep == null)
            {
                _mergeStep = 0;
                _levels = 150;
            }
            
            _socketClient = options?.SocketClient ?? new HuobiSocketClient();
            _socketOwner = options?.SocketClient == null;
        }

        /// <inheritdoc />
        protected override async Task<CallResult<UpdateSubscription>> DoStartAsync(CancellationToken ct)
        {
            if(_mergeStep != null)
            {
                var subResult = await _socketClient.SpotStreams.SubscribeToPartialOrderBookUpdates1SecondAsync(Symbol, _mergeStep.Value, HandleUpdate).ConfigureAwait(false);
                if (!subResult)
                    return subResult;

                if(ct.IsCancellationRequested)
                {
                    await subResult.Data.CloseAsync().ConfigureAwait(false);
                    return subResult.AsError<UpdateSubscription>(new CancellationRequestedError());
                }

                Status = OrderBookStatus.Syncing;
                var setResult = await WaitForSetOrderBookAsync(10000, ct).ConfigureAwait(false);
                if (!setResult)
                    await subResult.Data.CloseAsync().ConfigureAwait(false);

                return setResult ? subResult : new CallResult<UpdateSubscription>(setResult.Error!);
            }
            else
            {
                var subResult = await _socketClient.SpotStreams.SubscribeToOrderBookChangeUpdatesAsync(Symbol, _levels!.Value, HandleIncremental).ConfigureAwait(false);
                if (!subResult)
                    return subResult;

                if (ct.IsCancellationRequested)
                {
                    await subResult.Data.CloseAsync().ConfigureAwait(false);
                    return subResult.AsError<UpdateSubscription>(new CancellationRequestedError());
                }

                Status = OrderBookStatus.Syncing;
                // Wait a little so that the sequence number of the order book snapshot is higher than the first socket update sequence number
                await Task.Delay(500).ConfigureAwait(false);
                var book = await _socketClient.SpotStreams.GetOrderBookAsync(Symbol, _levels.Value).ConfigureAwait(false);
                if (!book) 
                {
                    log.Write(Microsoft.Extensions.Logging.LogLevel.Debug, $"{Id} order book {Symbol} failed to retrieve initial order book");
                    await _socketClient.UnsubscribeAsync(subResult.Data).ConfigureAwait(false);
                    return new CallResult<UpdateSubscription>(book.Error!);
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
        protected override async Task<CallResult<bool>> DoResyncAsync(CancellationToken ct)
        {
            if (_mergeStep != null)
            {
                return await WaitForSetOrderBookAsync(10000, ct).ConfigureAwait(false);
            }
            else
            {
                // Wait a little so that the sequence number of the order book snapshot is higher than the first socket update sequence number
                await Task.Delay(5000).ConfigureAwait(false);
                var book = await _socketClient.SpotStreams.GetOrderBookAsync(Symbol, _levels!.Value).ConfigureAwait(false);
                if (!book)
                    return new CallResult<bool>(book.Error!);                

                SetInitialOrderBook(book.Data.SequenceNumber, book.Data.Bids!, book.Data.Asks!);
                return new CallResult<bool>(true);
            }
        }

        /// <inheritdoc />
        protected override void Dispose(bool disposing)
        {
            if(_socketOwner)
                _socketClient?.Dispose();

            base.Dispose(disposing);
        }
    }
}
