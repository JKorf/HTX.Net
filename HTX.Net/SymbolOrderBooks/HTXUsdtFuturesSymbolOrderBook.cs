using CryptoExchange.Net.OrderBook;
using HTX.Net.Interfaces.Clients;
using HTX.Net.Clients;
using HTX.Net.Objects.Options;
using CryptoExchange.Net.Objects.Sockets;
using HTX.Net.Objects.Models.Socket;

namespace HTX.Net.SymbolOrderBooks
{
    /// <summary>
    /// HTX order book implementation
    /// </summary>
    public class HTXUsdtFuturesSymbolOrderBook : SymbolOrderBook
    {
        private readonly IHTXSocketClient _socketClient;
        private readonly int? _mergeStep;
        private readonly int? _levels;
        private readonly bool _socketOwner;
        private readonly TimeSpan _initialDataTimeout;

        /// <summary>
        /// Create a new order book instance
        /// </summary>
        /// <param name="symbol">The symbol the order book is for</param>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        public HTXUsdtFuturesSymbolOrderBook(string symbol, Action<HTXOrderBookOptions>? optionsDelegate = null)
            : this(symbol, optionsDelegate, null, null)
        {
        }

        /// <summary>
        /// Create a new order book instance
        /// </summary>
        /// <param name="symbol">The symbol the order book is for</param>
        /// <param name="optionsDelegate">Option configuration delegate</param>
        /// <param name="logger">Logger</param>
        /// <param name="socketClient">Socket client instance</param>
        public HTXUsdtFuturesSymbolOrderBook(string symbol,
            Action<HTXOrderBookOptions>? optionsDelegate,
            ILoggerFactory? logger,
            IHTXSocketClient? socketClient) : base(logger, "HTX", "Usdt Futures", symbol)
        {
            var options = HTXOrderBookOptions.Default.Copy();
            if (optionsDelegate != null)
                optionsDelegate(options);
            Initialize(options);

            _mergeStep = options?.MergeStep;
            _levels = options?.Levels;
            _strictLevels = false;
            _sequencesAreConsecutive = _levels != null;
            _initialDataTimeout = options?.InitialDataTimeout ?? TimeSpan.FromSeconds(30);

            if (_levels == null && _mergeStep == null)
            {
                _levels = 20;
            }
            
            _socketClient = socketClient ?? new HTXSocketClient();
            _socketOwner = socketClient == null;
        }

        /// <inheritdoc />
        protected override async Task<CallResult<UpdateSubscription>> DoStartAsync(CancellationToken ct)
        {
            if (_mergeStep != null)
            {
                var subResult = await _socketClient.UsdtFuturesApi.SubscribeToOrderBookUpdatesAsync(Symbol, _mergeStep.Value, HandleUpdate).ConfigureAwait(false);
                if (!subResult)
                    return subResult;

                var waitResult = await WaitForSetOrderBookAsync(_initialDataTimeout, ct).ConfigureAwait(false);
                if (!waitResult)
                    return waitResult.As<UpdateSubscription>(default);

                return subResult;
            }
            else
            {
                var subResult = await _socketClient.UsdtFuturesApi.SubscribeToIncrementalOrderBookUpdatesAsync(Symbol, false, _levels!.Value, HandleIncremental).ConfigureAwait(false);
                if (!subResult)
                    return subResult;

                var waitResult = await WaitForSetOrderBookAsync(_initialDataTimeout, ct).ConfigureAwait(false);
                if (!waitResult)
                    return waitResult.As<UpdateSubscription>(default);

                return subResult;
            }
        }

        private void HandleIncremental(DataEvent<HTXIncrementalOrderBookUpdate> book)
        {
            if (book.UpdateType == SocketUpdateType.Snapshot)
                SetInitialOrderBook(book.Data.Version!.Value, book.Data.Bids, book.Data.Asks);
            else
                UpdateOrderBook(book.Data.Version!.Value, book.Data.Bids, book.Data.Asks);
        }

        private void HandleUpdate(DataEvent<HTXOrderBookUpdate> data)
        {
            SetInitialOrderBook(data.Data.Version!.Value, data.Data.Bids, data.Data.Asks);
        }

        /// <inheritdoc />
        protected override async Task<CallResult<bool>> DoResyncAsync(CancellationToken ct)
        {
            if (_mergeStep != null)
            {
                return await WaitForSetOrderBookAsync(_initialDataTimeout, ct).ConfigureAwait(false);
            }
            else
            {
                // Wait a little so that the sequence number of the order book snapshot is higher than the first socket update sequence number
                await Task.Delay(5000).ConfigureAwait(false);
                var book = await _socketClient.SpotApi.GetOrderBookAsync(Symbol, _levels!.Value).ConfigureAwait(false);
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
