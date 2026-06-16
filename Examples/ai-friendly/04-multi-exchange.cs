// 04-multi-exchange.cs
//
// Demonstrates: writing exchange-agnostic code using CryptoExchange.Net.SharedApis.
// Same code works against HTX, Binance, OKX, Bybit, Kraken, and other exchanges
// from the CryptoExchange.Net family.
//
// Setup:
//   dotnet add package JKorf.HTX.Net
//   dotnet add package Binance.Net   // optional, for another exchange
//   dotnet add package JK.OKX.Net    // optional, for another exchange

using CryptoExchange.Net.SharedApis;
using HTX.Net.Clients;

// ---- THE PATTERN ----
// Each exchange client exposes a `.SharedClient` property on supported API surfaces.
// SharedClient implements interfaces like ISpotTickerRestClient, ISpotOrderRestClient,
// IFuturesOrderRestClient, IBalanceRestClient, and socket equivalents.

var restClient = new HTXRestClient();
ISpotTickerRestClient htxShared = restClient.SpotApi.SharedClient;
var capabilities = restClient.SpotApi.SharedClient.Discover();
Console.WriteLine($"Shared spot REST features: {capabilities.Features.Count(x => x.Supported)}");

// To add Binance or OKX, install the package and use that exchange's shared client:
//   ISpotTickerRestClient binanceShared = new BinanceRestClient().SpotApi.SharedClient;
//   ISpotTickerRestClient okxShared     = new OKXRestClient().UnifiedApi.SharedClient;

var ethusdt = new SharedSymbol(TradingMode.Spot, "ETH", "USDT");

await PrintTicker(htxShared, ethusdt);

async Task PrintTicker(ISpotTickerRestClient client, SharedSymbol symbol)
{
    var result = await client.GetSpotTickerAsync(new GetTickerRequest(symbol));
    if (!result.Success)
    {
        Console.WriteLine($"[{client.Exchange}] Failed: {result.Error}");
        return;
    }

    Console.WriteLine($"[{client.Exchange}] {result.Data.Symbol}: {result.Data.LastPrice}");
}

// ---- AVAILABLE HTX SHARED INTERFACES ----
// Spot REST:
//   IAssetsRestClient, IBalanceRestClient, IDepositRestClient, IKlineRestClient
//   IOrderBookRestClient, IRecentTradeRestClient, ISpotOrderRestClient
//   ISpotSymbolRestClient, ISpotTickerRestClient, IWithdrawalRestClient
//   IWithdrawRestClient, IFeeRestClient, ISpotOrderClientIdRestClient
//   ISpotTriggerOrderRestClient, IBookTickerRestClient, ITransferRestClient
// USDT futures REST:
//   IBalanceRestClient, IFuturesTickerRestClient, IFuturesSymbolRestClient
//   IFuturesOrderRestClient, IKlineRestClient, IMarkPriceKlineRestClient
//   IIndexPriceKlineRestClient, IOrderBookRestClient, IRecentTradeRestClient
//   IFundingRateRestClient, IOpenInterestRestClient, IPositionModeRestClient
//   IFeeRestClient, IFuturesOrderClientIdRestClient, IFuturesTriggerOrderRestClient
//   IFuturesTpSlRestClient, IBookTickerRestClient

// ---- WEBSOCKET EXAMPLE - SHARED SUBSCRIPTION ----
var htxSocket = new HTXSocketClient();
ITickerSocketClient tickerSocket = htxSocket.SpotApi.SharedClient;

var sub = await tickerSocket.SubscribeToTickerUpdatesAsync(
    new SubscribeTickerRequest(ethusdt),
    update => Console.WriteLine($"[{tickerSocket.Exchange}] {update.Data.Symbol}: {update.Data.LastPrice}"));

if (!sub.Success)
{
    Console.WriteLine($"Subscribe failed: {sub.Error}");
    return;
}

Console.WriteLine("Press Enter to exit");
Console.ReadLine();

// Shared socket interfaces do not expose UnsubscribeAsync. Keep the concrete
// socket client and unsubscribe through it.
await htxSocket.UnsubscribeAsync(sub.Data);

// Common variations:
//   Futures ticker:        new HTXRestClient().UsdtFuturesApi.SharedClient
//   Cross-exchange scans:  loop over List<ISpotTickerRestClient>
//   Order book sockets:    IOrderBookSocketClient from each exchange
//   User data trackers:    use each exchange's tracker factory for long-running services
