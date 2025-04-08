using HTX.Net.Clients;
using CryptoExchange.Net.Testing;
using NUnit.Framework;
using System.Threading.Tasks;
using HTX.Net.Objects.Models;
using System.Collections.Generic;
using HTX.Net.Objects.Models.Socket;

namespace HTX.Net.UnitTests
{
    [TestFixture]
    public class SocketSubscriptionTests
    {
        [Test]
        public async Task ValidateSpotSubscriptions()
        {
            var client = new HTXSocketClient(opts =>
            {
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new SocketSubscriptionValidator<HTXSocketClient>(client, "Subscriptions/Spot", "wss://api.huobi.pro", "data");
            await tester.ValidateAsync<HTXKline>((client, handler) => client.SpotApi.SubscribeToKlineUpdatesAsync("ETHUSDT", Enums.KlineInterval.OneDay, handler), "Klines", nestedJsonProperty: "tick");
            await tester.ValidateAsync<HTXOrderBook>((client, handler) => client.SpotApi.SubscribeToPartialOrderBookUpdates1SecondAsync("ETHUSDT", 0, handler), "OrderBook", nestedJsonProperty: "tick");
            await tester.ValidateAsync<HTXOrderBook>((client, handler) => client.SpotApi.SubscribeToPartialOrderBookUpdates100MillisecondAsync("ETHUSDT", 20, handler), "OrderBookMbp", nestedJsonProperty: "tick");
            await tester.ValidateAsync<HTXIncementalOrderBook>((client, handler) => client.SpotApi.SubscribeToOrderBookChangeUpdatesAsync("ETHUSDT", 5, handler), "OrderBookChange", nestedJsonProperty: "tick");
            await tester.ValidateAsync<HTXSymbolTrade>((client, handler) => client.SpotApi.SubscribeToTradeUpdatesAsync("ETHUSDT", handler), "Trades", nestedJsonProperty: "tick");
            await tester.ValidateAsync<HTXSymbolDetails>((client, handler) => client.SpotApi.SubscribeToSymbolDetailUpdatesAsync("ETHUSDT", handler), "Symbols", nestedJsonProperty: "tick");
            await tester.ValidateAsync<HTXSymbolTicker[]>((client, handler) => client.SpotApi.SubscribeToTickerUpdatesAsync(handler), "Tickers", nestedJsonProperty: "data");
            await tester.ValidateAsync<HTXSymbolTick>((client, handler) => client.SpotApi.SubscribeToTickerUpdatesAsync("ETHUSDT", handler), "Ticker", nestedJsonProperty: "tick");
            await tester.ValidateAsync<HTXBestOffer>((client, handler) => client.SpotApi.SubscribeToBookTickerUpdatesAsync("ETHUSDT", handler), "BookTicker", nestedJsonProperty: "tick");
            await tester.ValidateAsync<HTXSubmittedOrderUpdate>((client, handler) => client.SpotApi.SubscribeToOrderUpdatesAsync(null, handler), "Order1", nestedJsonProperty: "data");
            await tester.ValidateAsync<HTXAccountUpdate>((client, handler) => client.SpotApi.SubscribeToAccountUpdatesAsync(handler, null), "Account", nestedJsonProperty: "data");
            await tester.ValidateAsync<HTXTradeUpdate>((client, handler) => client.SpotApi.SubscribeToOrderDetailsUpdatesAsync(null, handler), "OrderTrade", nestedJsonProperty: "data");
        }

        [Test]
        public async Task ValidateUsdtMarginSwapSubscriptions()
        {
            var client = new HTXSocketClient(opts =>
            {
                opts.ApiCredentials = new CryptoExchange.Net.Authentication.ApiCredentials("123", "456");
            });
            var tester = new SocketSubscriptionValidator<HTXSocketClient>(client, "Subscriptions/UsdtMarginSwap", "wss://api.huobi.pro");
            await tester.ValidateAsync<HTXSwapKline>((client, handler) => client.UsdtFuturesApi.SubscribeToKlineUpdatesAsync("ETH-USDT", Enums.KlineInterval.OneDay, handler), "Klines", nestedJsonProperty: "tick");
            await tester.ValidateAsync<HTXOrderBookUpdate>((client, handler) => client.UsdtFuturesApi.SubscribeToOrderBookUpdatesAsync("ETH-USDT", 0, handler), "OrderBook", nestedJsonProperty: "tick", ignoreProperties: new List<string> { "ch", "ts" });
            await tester.ValidateAsync<HTXIncrementalOrderBookUpdate>((client, handler) => client.UsdtFuturesApi.SubscribeToIncrementalOrderBookUpdatesAsync("ETH-USDT", true, 20, handler), "OrderBookIncr", nestedJsonProperty: "tick", ignoreProperties: new List<string> { "ch", "ts" });
            await tester.ValidateAsync<HTXSymbolTickUpdate>((client, handler) => client.UsdtFuturesApi.SubscribeToTickerUpdatesAsync("ETH-USDT", handler), "Ticker", nestedJsonProperty: "tick");
            await tester.ValidateAsync<HTXBestOfferUpdate>((client, handler) => client.UsdtFuturesApi.SubscribeToBookTickerUpdatesAsync("ETH-USDT", handler), "BookTicker", nestedJsonProperty: "tick", ignoreProperties: new List<string> { "ch" });
            await tester.ValidateAsync<HTXUsdtMarginSwapTradesUpdate>((client, handler) => client.UsdtFuturesApi.SubscribeToTradeUpdatesAsync("ETH-USDT", handler), "Trades", nestedJsonProperty: "tick", ignoreProperties: new List<string> { "ch" });
            await tester.ValidateAsync<HTXKline>((client, handler) => client.UsdtFuturesApi.SubscribeToIndexKlineUpdatesAsync("ETH-USDT", Enums.KlineInterval.OneDay, handler), "IndexKline", nestedJsonProperty: "tick", ignoreProperties: new List<string> { "ch" });
            await tester.ValidateAsync<HTXKline>((client, handler) => client.UsdtFuturesApi.SubscribeToPremiumIndexKlineUpdatesAsync("ETH-USDT", Enums.KlineInterval.OneDay, handler), "PremiumKline", nestedJsonProperty: "tick", ignoreProperties: new List<string> { "ch" });
            await tester.ValidateAsync<HTXKline>((client, handler) => client.UsdtFuturesApi.SubscribeToEstimatedFundingRateKlineUpdatesAsync("ETH-USDT", Enums.KlineInterval.OneDay, handler), "FundingKlines", nestedJsonProperty: "tick", ignoreProperties: new List<string> { "ch" });
            await tester.ValidateAsync<HTXUsdtMarginSwapBasisUpdate>((client, handler) => client.UsdtFuturesApi.SubscribeToBasisUpdatesAsync("ETH-USDT", Enums.KlineInterval.OneDay, "open", handler), "Basis", nestedJsonProperty: "tick", ignoreProperties: new List<string> { "ch" });
            await tester.ValidateAsync<HTXKline>((client, handler) => client.UsdtFuturesApi.SubscribeToMarkPriceKlineUpdatesAsync("ETH-USDT", Enums.KlineInterval.OneDay, handler), "MarkKline", nestedJsonProperty: "tick", ignoreProperties: new List<string> { "ch" });
            await tester.ValidateAsync<HTXUsdtMarginSwapLiquidationUpdate>((client, handler) => client.UsdtFuturesApi.SubscribeToLiquidationUpdatesAsync(handler), "Liquidation", nestedJsonProperty: "data");
            await tester.ValidateAsync<HTXUsdtMarginSwapFundingRateUpdate[]>((client, handler) => client.UsdtFuturesApi.SubscribeToFundingRateUpdatesAsync(handler), "Funding", nestedJsonProperty: "data");
            await tester.ValidateAsync<HTXUsdtMarginSwapContractUpdate[]>((client, handler) => client.UsdtFuturesApi.SubscribeToContractUpdatesAsync(handler), "Contract", nestedJsonProperty: "data");
            await tester.ValidateAsync<HTXUsdtMarginSwapContractElementsUpdate[]>((client, handler) => client.UsdtFuturesApi.SubscribeToContractElementsUpdatesAsync(handler), "ContractElements", nestedJsonProperty: "data");
            await tester.ValidateAsync<HTXStatusUpdate>((client, handler) => client.UsdtFuturesApi.SubscribeToSystemStatusUpdatesAsync(handler), "Status");
            await tester.ValidateAsync<HTXUsdtMarginSwapOrderUpdate>((client, handler) => client.UsdtFuturesApi.SubscribeToOrderUpdatesAsync(Enums.MarginMode.Isolated, handler), "Order");
            await tester.ValidateAsync<HTXUsdtMarginSwapIsolatedBalanceUpdate>((client, handler) => client.UsdtFuturesApi.SubscribeToIsolatedMarginBalanceUpdatesAsync(handler), "IsolatedBalance");
            await tester.ValidateAsync<HTXUsdtMarginSwapCrossBalanceUpdate>((client, handler) => client.UsdtFuturesApi.SubscribeToCrossMarginBalanceUpdatesAsync(handler), "CrossBalance");
            await tester.ValidateAsync<HTXUsdtMarginSwapIsolatedPositionUpdate>((client, handler) => client.UsdtFuturesApi.SubscribeToIsolatedMarginPositionUpdatesAsync(handler), "IsolatedPositions");
            await tester.ValidateAsync<HTXUsdtMarginSwapCrossPositionUpdate>((client, handler) => client.UsdtFuturesApi.SubscribeToCrossMarginPositionUpdatesAsync(handler), "CrossPositions");
            await tester.ValidateAsync<HTXUsdtMarginSwapIsolatedTradeUpdate>((client, handler) => client.UsdtFuturesApi.SubscribeToIsolatedMarginUserTradeUpdatesAsync(handler), "IsolatedTrades");
            await tester.ValidateAsync<HTXUsdtMarginSwapCrossTradeUpdate>((client, handler) => client.UsdtFuturesApi.SubscribeToCrossMarginUserTradeUpdatesAsync(handler), "CrossTrades");
            await tester.ValidateAsync<HTXUsdtMarginSwapIsolatedTriggerOrderUpdate>((client, handler) => client.UsdtFuturesApi.SubscribeToIsolatedMarginTriggerOrderUpdatesAsync(handler), "IsolatedTrigger");
            await tester.ValidateAsync<HTXUsdtMarginSwapCrossTriggerOrderUpdate>((client, handler) => client.UsdtFuturesApi.SubscribeToCrossMarginTriggerOrderUpdatesAsync(handler), "CrossTrigger");
        }
    }
}
