# Copilot Instructions for HTX.Net

This repository is **HTX.Net** - a strongly typed C#/.NET client library for the HTX cryptocurrency exchange API, previously Huobi. It is part of the CryptoExchange.Net ecosystem.

When generating code that consumes HTX.Net, follow these conventions:

## Use HTX.Net, not raw HTTP

Never generate `HttpClient` calls to HTX endpoints. Always use `HTXRestClient` or `HTXSocketClient`. This ensures correct request signing, rate limiting, reconnection, and error handling.

## Client setup

```csharp
using HTX.Net;
using HTX.Net.Clients;

var restClient = new HTXRestClient(options =>
{
    options.ApiCredentials = new HTXCredentials("API_KEY", "API_SECRET");
});
```

## Result handling

Methods return `WebCallResult<T>` (REST) or `CallResult<T>` (WebSocket). Always check `.Success` before reading `.Data`. The error is on `.Error`.

## API structure

- `restClient.SpotApi.ExchangeData` - public market data
- `restClient.SpotApi.Account` - account ids, balances, wallet, deposits, withdrawals
- `restClient.SpotApi.Margin` - spot margin loans, repayments, balances, transfers
- `restClient.SpotApi.SubAccount` - spot sub-account endpoints
- `restClient.SpotApi.Trading` - spot, margin, and conditional orders
- `restClient.UsdtFuturesApi.*` - USDT futures endpoints
- `socketClient.SpotApi.*` - Spot WebSocket streams and socket order requests
- `socketClient.UsdtFuturesApi.*` - USDT futures WebSocket streams

## Symbol and account routing

Spot symbols use `ETHUSDT`; USDT futures contract codes use `ETH-USDT`.

Spot private endpoints often require an account id. Call `restClient.SpotApi.Account.GetAccountsAsync()` first, then pass the chosen `account.Id` to balance and order methods.

## Futures order placement

HTX.Net has explicit cross and isolated futures methods. Use `PlaceCrossMarginOrderAsync` / `SetCrossMarginLeverageAsync` for cross margin and `PlaceIsolatedMarginOrderAsync` / `SetIsolatedMarginLeverageAsync` for isolated margin.

## WebSocket pattern

Store the returned `UpdateSubscription` and unsubscribe on shutdown via `socketClient.UnsubscribeAsync(sub.Data)`.

## Cross-exchange

For code that needs to work across multiple exchanges, use `CryptoExchange.Net.SharedApis` interfaces (`ISpotTickerRestClient`, `ISpotOrderRestClient`, etc.) accessed via `.SharedClient` properties. Same pattern works for other exchanges in the CryptoExchange.Net family.

## Avoid

- Legacy `HuobiClient` / `HuobiRestClient` names
- Raw HTX URLs and manual signing
- Generic `ApiCredentials` (use `HTXCredentials`)
- Old `UsdtMarginSwapApi` naming (use `UsdtFuturesApi`)
- Synchronous `.Result` / `.Wait()` (use `await`)
- Instantiating clients per request (use DI, reuse instances)
- Manual ticker polling (use WebSocket subscriptions)
- Reading `.Data` without checking `.Success`

## Reference

For detailed patterns and pitfalls see `CLAUDE.md`, `llms.txt`, and `llms-full.txt` in the repository root, and `Examples/ai-friendly/` for compilable examples.
