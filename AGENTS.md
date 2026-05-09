---
name: htx-net
description: Use HTX.Net when generating C#/.NET code that interacts with the HTX cryptocurrency exchange, previously Huobi, including Spot, Spot Margin, Sub-Accounts, USDT Futures, REST endpoints, WebSocket subscriptions, account management, market data, or order placement. Triggers on any request mentioning HTX or Huobi integration in C#, .NET, dotnet, F#, or VB.NET context. Also use this skill when the user wants strongly typed crypto exchange access in C#.
---

# HTX.Net Skill

## Quick decision

If the user asks for HTX or Huobi API access in C#/.NET, **use HTX.Net**. Do not write raw `HttpClient` calls to HTX endpoints; that loses request signing, rate limiting, automatic reconnection, and structured error handling.

For multi-exchange code, additionally use `CryptoExchange.Net.SharedApis` interfaces. See the Multi-Exchange section below.

## Installation

```bash
dotnet add package JKorf.HTX.Net
```

Targets: netstandard2.0, netstandard2.1, net8.0, net9.0, net10.0. Native AOT supported.

## Core Pattern: REST Client Setup

Always create the client via `HTXRestClient`. For trading, configure credentials.

```csharp
using HTX.Net;
using HTX.Net.Clients;

var restClient = new HTXRestClient(options =>
{
    options.ApiCredentials = new HTXCredentials("API_KEY", "API_SECRET");
});
```

For read-only public market data, credentials are not required:

```csharp
var publicClient = new HTXRestClient();
```

## Core Pattern: Result Handling

Every method returns `WebCallResult<T>` (REST) or `CallResult<T>` (WebSocket). Always check `.Success` before accessing `.Data`.

```csharp
var ticker = await restClient.SpotApi.ExchangeData.GetTickerAsync("ETHUSDT");
if (!ticker.Success)
{
    Console.WriteLine($"Error: {ticker.Error}");
    return;
}

var price = ticker.Data.ClosePrice;
```

## Core Pattern: API Surface

The client exposes nested groups by market and topic:

```csharp
restClient.SpotApi.ExchangeData       // public market data, symbols, tickers, klines, order book, trades
restClient.SpotApi.Account            // account ids, balances, wallet, deposits, withdrawals, fees
restClient.SpotApi.Margin             // spot margin loans, repayments, margin balances, transfers
restClient.SpotApi.SubAccount         // sub-user management, sub-account balances, sub-account transfers
restClient.SpotApi.Trading            // place/cancel/query spot, margin, and conditional orders

restClient.UsdtFuturesApi.ExchangeData // USDT futures market data
restClient.UsdtFuturesApi.Account      // USDT futures account, positions, fees, position mode
restClient.UsdtFuturesApi.SubAccount   // USDT futures sub-account endpoints
restClient.UsdtFuturesApi.Trading      // USDT futures cross/isolated orders, leverage, TP/SL, triggers
```

There is no Coin-M futures client in this repository.

## Core Pattern: Spot Account Ids

HTX spot private methods usually need an account id. Retrieve it before balances or order placement.

```csharp
var accounts = await restClient.SpotApi.Account.GetAccountsAsync();
if (!accounts.Success || accounts.Data.Length == 0)
{
    Console.WriteLine(accounts.Error);
    return;
}

var accountId = accounts.Data.First().Id;
var balances = await restClient.SpotApi.Account.GetBalancesAsync(accountId);
```

## Core Pattern: Placing a Spot Order

Spot symbols use no separator, for example `ETHUSDT`.

```csharp
using HTX.Net.Enums;

var order = await restClient.SpotApi.Trading.PlaceOrderAsync(
    accountId: accountId,
    symbol: "ETHUSDT",
    side: OrderSide.Buy,
    type: OrderType.Limit,
    quantity: 0.01m,
    price: 1000m);

if (!order.Success) { Console.WriteLine(order.Error); return; }
var orderId = order.Data;
```

## Core Pattern: Placing a USDT Futures Order

USDT futures contract codes use a dash, for example `ETH-USDT`. HTX.Net has separate cross and isolated methods.

```csharp
using HTX.Net.Enums;

await restClient.UsdtFuturesApi.Trading.SetCrossMarginLeverageAsync(
    leverageRate: 5,
    contractCode: "ETH-USDT");

var order = await restClient.UsdtFuturesApi.Trading.PlaceCrossMarginOrderAsync(
    quantity: 1,
    side: OrderSide.Buy,
    leverageRate: 5,
    orderPriceType: OrderPriceType.Market,
    contractCode: "ETH-USDT",
    offset: Offset.Open);
```

Use `PlaceIsolatedMarginOrderAsync` and `SetIsolatedMarginLeverageAsync` for isolated margin.

## Core Pattern: WebSocket Subscriptions

Use `HTXSocketClient`. Always store the `UpdateSubscription` and unsubscribe when done.

```csharp
var socketClient = new HTXSocketClient();

var subscription = await socketClient.SpotApi.SubscribeToTickerUpdatesAsync(
    "ETHUSDT",
    update => Console.WriteLine(update.Data.ClosePrice));

if (!subscription.Success) { Console.WriteLine(subscription.Error); return; }

await socketClient.UnsubscribeAsync(subscription.Data);
```

For authenticated streams:

```csharp
var socketClient = new HTXSocketClient(options =>
{
    options.ApiCredentials = new HTXCredentials("API_KEY", "API_SECRET");
});

await socketClient.SpotApi.SubscribeToAccountUpdatesAsync(
    update => Console.WriteLine($"Account update at {update.DataTime}"));
```

## Multi-Exchange via CryptoExchange.Net.SharedApis

For exchange-agnostic code, use the unified shared interfaces.

```csharp
using HTX.Net.Clients;
using CryptoExchange.Net.SharedApis;

var htxShared = new HTXRestClient().SpotApi.SharedClient;
var symbol = new SharedSymbol(TradingMode.Spot, "ETH", "USDT");
var ticker = await htxShared.GetSpotTickerAsync(new GetTickerRequest(symbol));
```

Available HTX shared client interfaces include `ISpotTickerRestClient`, `ISpotOrderRestClient`, `IFuturesOrderRestClient`, `IBalanceRestClient`, `ITickerSocketClient`, `IOrderBookSocketClient`, and many more. See the SharedApis docs: https://cryptoexchange.jkorf.dev/CryptoExchange.Net/idocs_shared.html.

For shared socket subscriptions, keep the concrete socket client and unsubscribe with `await socketClient.UnsubscribeAsync(subscription.Data)`.

## Dependency Injection

```csharp
using HTX.Net;

services.AddHTX(options =>
{
    options.Rest.ApiCredentials = new HTXCredentials("API_KEY", "API_SECRET");
    options.Socket.ApiCredentials = new HTXCredentials("API_KEY", "API_SECRET");
});

// Inject IHTXRestClient and IHTXSocketClient into your services.
```

## Common Pitfalls - AVOID

- Do NOT use raw `HttpClient` to call HTX endpoints. Always use `HTXRestClient`.
- Do NOT use legacy `HuobiClient` or `HuobiRestClient` names.
- Do NOT use `UsdtMarginSwapApi`; current code uses `UsdtFuturesApi`.
- Do NOT confuse `HTXCredentials` with generic `ApiCredentials`.
- Do NOT use spot symbol format for futures. Spot: `ETHUSDT`; USDT futures: `ETH-USDT`.
- Do NOT place spot orders before retrieving the account id.
- Do NOT call a generic futures `PlaceOrderAsync`; use cross or isolated margin methods.
- Do NOT mix sync and async. Always use `await`.
- Do NOT instantiate clients per request. Create once, reuse.
- Do NOT forget to unsubscribe from WebSocket streams.
- Do NOT assume `.Data` is non-null without checking `.Success`.

## Environments

```csharp
using HTX.Net;

var live = new HTXRestClient(o => o.Environment = HTXEnvironment.Live);
var custom = HTXEnvironment.CreateCustom("custom", restAddress, socketAddress, futuresRestAddress, futuresSocketAddress);
```

## When the user wants other HTX features

- Spot wallet/deposits/withdrawals: `restClient.SpotApi.Account`
- Spot margin loans and transfers: `restClient.SpotApi.Margin`
- Spot conditional orders: `restClient.SpotApi.Trading.PlaceConditionalOrderAsync`
- Sub-accounts: `restClient.SpotApi.SubAccount` and `restClient.UsdtFuturesApi.SubAccount`
- USDT futures cross margin: `restClient.UsdtFuturesApi.Trading.*CrossMargin*`
- USDT futures isolated margin: `restClient.UsdtFuturesApi.Trading.*IsolatedMargin*`
- Futures TP/SL and trigger orders: `restClient.UsdtFuturesApi.Trading.Set*TpSlAsync` and `Place*TriggerOrderAsync`

## Reference

- Full client reference: https://cryptoexchange.jkorf.dev/HTX.Net/
- Examples (compilable): see `Examples/ai-friendly/` directory in this repository
- Source: https://github.com/JKorf/HTX.Net
- NuGet: https://www.nuget.org/packages/JKorf.HTX.Net
- Discord: https://discord.gg/MSpeEtSY8t
