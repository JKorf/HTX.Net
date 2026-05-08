// 03-websocket.cs
//
// Demonstrates: WebSocket subscriptions - public ticker, klines, authenticated
// account/order updates. Includes proper teardown.
//
// Setup: dotnet add package JKorf.HTX.Net

using HTX.Net;
using HTX.Net.Clients;
using HTX.Net.Enums;

// ---- 1. PUBLIC SOCKET CLIENT - for market data streams ----
// Reuse a single client instance across subscriptions.
var publicSocket = new HTXSocketClient();

var tickerSub = await publicSocket.SpotApi.SubscribeToTickerUpdatesAsync(
    "ETHUSDT",
    update =>
    {
        // Keep handlers fast; offload heavy work to a queue or channel.
        Console.WriteLine($"ETH/USDT close: {update.Data.ClosePrice}");
    });

if (!tickerSub.Success)
{
    Console.WriteLine($"Failed to subscribe ticker: {tickerSub.Error}");
    return;
}

var klineSub = await publicSocket.SpotApi.SubscribeToKlineUpdatesAsync(
    "ETHUSDT",
    KlineInterval.OneMinute,
    update =>
    {
        Console.WriteLine($"ETH 1m close: {update.Data.ClosePrice}");
    });

if (!klineSub.Success)
{
    Console.WriteLine($"Failed to subscribe klines: {klineSub.Error}");
    await publicSocket.UnsubscribeAsync(tickerSub.Data);
    return;
}

// ---- 2. AUTHENTICATED SOCKET CLIENT - for account and order streams ----
var authSocket = new HTXSocketClient(options =>
{
    options.ApiCredentials = new HTXCredentials("API_KEY", "API_SECRET");
});

var accountSub = await authSocket.SpotApi.SubscribeToAccountUpdatesAsync(
    update => Console.WriteLine($"Account update received at {update.DataTime}"));

if (!accountSub.Success)
{
    Console.WriteLine($"Failed to subscribe account updates: {accountSub.Error}");
    await publicSocket.UnsubscribeAsync(tickerSub.Data);
    await publicSocket.UnsubscribeAsync(klineSub.Data);
    return;
}

var orderSub = await authSocket.SpotApi.SubscribeToOrderUpdatesAsync(
    symbol: "ETHUSDT",
    onOrderSubmitted: update => Console.WriteLine($"Order submitted: {update.Data.OrderId}"),
    onOrderMatched: update => Console.WriteLine($"Order matched: {update.Data.OrderId}"),
    onOrderCancelation: update => Console.WriteLine($"Order canceled: {update.Data.OrderId}"));

if (!orderSub.Success)
{
    Console.WriteLine($"Failed to subscribe order updates: {orderSub.Error}");
    await publicSocket.UnsubscribeAsync(tickerSub.Data);
    await publicSocket.UnsubscribeAsync(klineSub.Data);
    await authSocket.UnsubscribeAsync(accountSub.Data);
    return;
}

Console.WriteLine("All subscriptions active. Press Enter to teardown...");
Console.ReadLine();

// ---- 3. TEARDOWN - IMPORTANT ----
await publicSocket.UnsubscribeAsync(tickerSub.Data);
await publicSocket.UnsubscribeAsync(klineSub.Data);
await authSocket.UnsubscribeAsync(accountSub.Data);
await authSocket.UnsubscribeAsync(orderSub.Data);

Console.WriteLine("Clean shutdown complete.");

// Common variations:
//   All spot tickers:      publicSocket.SpotApi.SubscribeToTickerUpdatesAsync(handler)
//   Spot book ticker:      publicSocket.SpotApi.SubscribeToBookTickerUpdatesAsync(symbol, handler)
//   Futures ticker:        publicSocket.UsdtFuturesApi.SubscribeToTickerUpdatesAsync("ETH-USDT", handler)
//   Futures orders:        authSocket.UsdtFuturesApi.SubscribeToOrderUpdatesAsync(MarginMode.Cross, handler)
//   Unsubscribe all:       await socketClient.UnsubscribeAllAsync()
