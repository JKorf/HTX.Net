# HTX.Net AI API Quick Map

Use this file to route common user intents to the correct HTX.Net client member. If a method name or parameter is not listed here, inspect `HTX.Net/Interfaces/Clients/**` before generating code.

## Client Roots

| Intent | Use |
|---|---|
| REST calls | `new HTXRestClient()` |
| WebSocket streams and socket API requests | `new HTXSocketClient()` |
| API key authentication | `options.ApiCredentials = new HTXCredentials("key", "secret")` |
| Live environment | `HTXEnvironment.Live` |
| Custom environment | `HTXEnvironment.CreateCustom(...)` |
| Dependency injection | `services.AddHTX(options => { ... })` |

## Spot REST

| User intent | HTX.Net member |
|---|---|
| Get server time | `client.SpotApi.ExchangeData.GetServerTimeAsync()` |
| Get system status | `client.SpotApi.ExchangeData.GetSystemStatusAsync()` |
| Get spot symbols | `client.SpotApi.ExchangeData.GetSymbolsAsync()` |
| Get spot symbol config | `client.SpotApi.ExchangeData.GetSymbolConfigAsync(new[] { "ETHUSDT" })` |
| Get assets | `client.SpotApi.ExchangeData.GetAssetsAsync()` |
| Get networks | `client.SpotApi.ExchangeData.GetNetworksAsync(...)` |
| Get assets and networks | `client.SpotApi.ExchangeData.GetAssetsAndNetworksAsync(...)` |
| Get all spot tickers | `client.SpotApi.ExchangeData.GetTickersAsync()` |
| Get spot ticker | `client.SpotApi.ExchangeData.GetTickerAsync("ETHUSDT")` |
| Get spot klines | `client.SpotApi.ExchangeData.GetKlinesAsync("ETHUSDT", KlineInterval.OneMinute)` |
| Get spot order book | `client.SpotApi.ExchangeData.GetOrderBookAsync("ETHUSDT", mergeStep: 0)` |
| Get full spot order book | `client.SpotApi.ExchangeData.GetFullOrderBookAsync("ETHUSDT")` |
| Get latest trade | `client.SpotApi.ExchangeData.GetLastTradeAsync("ETHUSDT")` |
| Get recent trades | `client.SpotApi.ExchangeData.GetTradeHistoryAsync("ETHUSDT")` |
| Get 24h symbol details | `client.SpotApi.ExchangeData.GetSymbolDetails24HAsync("ETHUSDT")` |
| Get symbol status | `client.SpotApi.ExchangeData.GetSymbolStatusAsync()` |
| Get user id | `client.SpotApi.Account.GetUserIdAsync()` |
| Get accounts | `client.SpotApi.Account.GetAccountsAsync()` |
| Get balances | `client.SpotApi.Account.GetBalancesAsync(accountId)` |
| Get account history | `client.SpotApi.Account.GetAccountHistoryAsync(accountId, ...)` |
| Get account ledger | `client.SpotApi.Account.GetAccountLedgerAsync(accountId, ...)` |
| Transfer between account types | `client.SpotApi.Account.TransferAsync(...)` |
| Get deposit addresses | `client.SpotApi.Account.GetDepositAddressesAsync("USDT")` |
| Withdraw asset | `client.SpotApi.Account.WithdrawAsync(...)` |
| Get deposit/withdraw history | `client.SpotApi.Account.GetWithdrawDepositHistoryAsync(...)` |
| Get trading fees | `client.SpotApi.Account.GetTradingFeesAsync(new[] { "ETHUSDT" })` |
| Get withdrawal quotas | `client.SpotApi.Account.GetWithdrawalQuotasAsync(...)` |
| Get withdrawal addresses | `client.SpotApi.Account.GetWithdrawalAddressesAsync("USDT")` |
| Cancel withdrawal | `client.SpotApi.Account.CancelWithdrawalAsync(id)` |
| Place spot order | `client.SpotApi.Trading.PlaceOrderAsync(accountId, "ETHUSDT", OrderSide.Buy, OrderType.Limit, quantity, price)` |
| Place multiple spot orders | `client.SpotApi.Trading.PlaceMultipleOrderAsync(...)` |
| Get open spot orders | `client.SpotApi.Trading.GetOpenOrdersAsync(...)` |
| Get spot order | `client.SpotApi.Trading.GetOrderAsync(orderId)` |
| Get spot order by client order id | `client.SpotApi.Trading.GetOrderByClientOrderIdAsync(clientOrderId)` |
| Get order trades | `client.SpotApi.Trading.GetOrderTradesAsync(orderId)` |
| Get closed orders | `client.SpotApi.Trading.GetClosedOrdersAsync("ETHUSDT", ...)` |
| Get user trades | `client.SpotApi.Trading.GetUserTradesAsync(...)` |
| Cancel spot order | `client.SpotApi.Trading.CancelOrderAsync(orderId)` |
| Cancel by client order id | `client.SpotApi.Trading.CancelOrderByClientOrderIdAsync(clientOrderId)` |
| Cancel multiple orders | `client.SpotApi.Trading.CancelOrdersAsync(...)` |
| Cancel all spot orders | `client.SpotApi.Trading.CancelAllOrdersAsync(symbol)` |
| Cancel orders by criteria | `client.SpotApi.Trading.CancelOrdersByCriteriaAsync(...)` |
| Place conditional order | `client.SpotApi.Trading.PlaceConditionalOrderAsync(...)` |
| Get open conditional orders | `client.SpotApi.Trading.GetOpenConditionalOrdersAsync(...)` |
| Get closed conditional orders | `client.SpotApi.Trading.GetClosedConditionalOrdersAsync(...)` |
| Get conditional order | `client.SpotApi.Trading.GetConditionalOrderAsync(clientOrderId)` |
| Cancel conditional orders | `client.SpotApi.Trading.CancelConditionalOrdersAsync(clientOrderIds)` |

## Spot Margin REST

Margin has its own `SpotApi.Margin` topic. Margin order placement is under `SpotApi.Trading.PlaceMarginOrderAsync(...)`.

| User intent | HTX.Net member |
|---|---|
| Repay loan | `client.SpotApi.Margin.RepayLoanAsync(...)` |
| Transfer spot to isolated margin | `client.SpotApi.Margin.TransferSpotToIsolatedMarginAsync(...)` |
| Transfer isolated margin to spot | `client.SpotApi.Margin.TransferIsolatedMarginToSpotAsync(...)` |
| Request isolated margin loan | `client.SpotApi.Margin.RequestIsolatedMarginLoanAsync(...)` |
| Repay isolated margin loan | `client.SpotApi.Margin.RepayIsolatedMarginLoanAsync(...)` |
| Get isolated margin balances | `client.SpotApi.Margin.GetIsolatedMarginBalanceAsync(symbol)` |
| Get isolated margin closed orders | `client.SpotApi.Margin.GetIsolatedMarginClosedOrdersAsync(...)` |
| Transfer spot to cross margin | `client.SpotApi.Margin.TransferSpotToCrossMarginAsync(...)` |
| Transfer cross margin to spot | `client.SpotApi.Margin.TransferCrossMarginToSpotAsync(...)` |
| Request cross margin loan | `client.SpotApi.Margin.RequestCrossMarginLoanAsync(...)` |
| Repay cross margin loan | `client.SpotApi.Margin.RepayCrossMarginLoanAsync(...)` |
| Get cross margin balance | `client.SpotApi.Margin.GetCrossMarginBalanceAsync()` |
| Get cross margin closed orders | `client.SpotApi.Margin.GetCrossMarginClosedOrdersAsync(...)` |
| Get repayment history | `client.SpotApi.Margin.GetRepaymentHistoryAsync(...)` |
| Place margin order | `client.SpotApi.Trading.PlaceMarginOrderAsync(...)` |

## Spot Sub-Account REST

| User intent | HTX.Net member |
|---|---|
| Create sub accounts | `client.SpotApi.SubAccount.CreateSubAccountsAsync(...)` |
| Get sub users | `client.SpotApi.SubAccount.GetSubUserListAsync()` |
| Lock/unlock sub user | `client.SpotApi.SubAccount.SetLockAsync(...)` |
| Get sub user | `client.SpotApi.SubAccount.GetSubUserAsync(subUserId)` |
| Set tradable markets | `client.SpotApi.SubAccount.SetTradableMarketAsync(...)` |
| Set transfer permissions | `client.SpotApi.SubAccount.SetAssetTransferPermissionsAsync(...)` |
| Get sub user accounts | `client.SpotApi.SubAccount.GetSubUserAccountsAsync(subUserId)` |
| Create sub API key | `client.SpotApi.SubAccount.CreateApiKeyAsync(...)` |
| Edit sub API key | `client.SpotApi.SubAccount.EditApiKeyAsync(...)` |
| Delete sub API key | `client.SpotApi.SubAccount.DeleteApiKeyAsync(...)` |
| Get sub deposit address | `client.SpotApi.SubAccount.GetDepositAddressAsync(...)` |
| Get sub deposit history | `client.SpotApi.SubAccount.GetDepositHistoryAsync(...)` |
| Get aggregate balances | `client.SpotApi.SubAccount.GetAggregateBalancesAsync()` |
| Get sub balances | `client.SpotApi.SubAccount.GetBalancesAsync(subAccountId)` |
| Transfer with sub account | `client.SpotApi.SubAccount.TransferWithSubAccountAsync(...)` |

## USDT Futures REST

HTX.Net exposes USDT futures as `UsdtFuturesApi`. The library does not expose Coin-M futures or Coin-M swap clients.

| User intent | HTX.Net member |
|---|---|
| Get futures server time | `client.UsdtFuturesApi.ExchangeData.GetServerTimeAsync()` |
| Get contract info | `client.UsdtFuturesApi.ExchangeData.GetContractsAsync(...)` |
| Get contract elements | `client.UsdtFuturesApi.ExchangeData.GetContractElementsAsync("ETH-USDT")` |
| Get futures ticker | `client.UsdtFuturesApi.ExchangeData.GetTickerAsync("ETH-USDT")` |
| Get futures tickers | `client.UsdtFuturesApi.ExchangeData.GetTickersAsync(...)` |
| Get futures book ticker | `client.UsdtFuturesApi.ExchangeData.GetBookTickerAsync(...)` |
| Get futures order book | `client.UsdtFuturesApi.ExchangeData.GetOrderBookAsync("ETH-USDT")` |
| Get futures klines | `client.UsdtFuturesApi.ExchangeData.GetKlinesAsync("ETH-USDT", KlineInterval.OneMinute)` |
| Get recent futures trades | `client.UsdtFuturesApi.ExchangeData.GetRecentTradesAsync("ETH-USDT", 100)` |
| Get funding rate | `client.UsdtFuturesApi.ExchangeData.GetFundingRateAsync("ETH-USDT")` |
| Get all funding rates | `client.UsdtFuturesApi.ExchangeData.GetFundingRatesAsync(...)` |
| Get historical funding rates | `client.UsdtFuturesApi.ExchangeData.GetHistoricalFundingRatesAsync("ETH-USDT")` |
| Get swap index price | `client.UsdtFuturesApi.ExchangeData.GetSwapIndexPriceAsync(...)` |
| Get open interest | `client.UsdtFuturesApi.ExchangeData.GetSwapOpenInterestAsync(...)` |
| Get open interest history | `client.UsdtFuturesApi.ExchangeData.GetOpenInterestHistoryAsync(...)` |
| Get price limits | `client.UsdtFuturesApi.ExchangeData.GetSwapPriceLimitationAsync(...)` |
| Get risk info | `client.UsdtFuturesApi.ExchangeData.GetSwapRiskInfoAsync(...)` |
| Get liquidation orders | `client.UsdtFuturesApi.ExchangeData.GetLiquidationOrdersAsync(...)` |
| Get insurance fund info | `client.UsdtFuturesApi.ExchangeData.GetInsuranceFundInfoAsync()` |
| Get isolated account info | `client.UsdtFuturesApi.Account.GetIsolatedMarginAccountInfoAsync(...)` |
| Get cross account info | `client.UsdtFuturesApi.Account.GetCrossMarginAccountInfoAsync(...)` |
| Get cross assets and positions | `client.UsdtFuturesApi.Account.GetCrossMarginAssetsAndPositionsAsync(marginAccount)` |
| Get isolated positions | `client.UsdtFuturesApi.Account.GetIsolatedMarginPositionsAsync(...)` |
| Get cross positions | `client.UsdtFuturesApi.Account.GetCrossMarginPositionsAsync(...)` |
| Get trading fees | `client.UsdtFuturesApi.Account.GetTradingFeesAsync(...)` |
| Get financial records | `client.UsdtFuturesApi.Account.GetFinancialRecordsAsync(...)` |
| Set isolated position mode | `client.UsdtFuturesApi.Account.SetIsolatedMarginPositionModeAsync(...)` |
| Set cross position mode | `client.UsdtFuturesApi.Account.SetCrossMarginPositionModeAsync(...)` |
| Get isolated position mode | `client.UsdtFuturesApi.Account.GetIsolatedMarginPositionModeAsync(...)` |
| Get cross position mode | `client.UsdtFuturesApi.Account.GetCrossMarginPositionModeAsync(...)` |
| Set cross leverage | `client.UsdtFuturesApi.Trading.SetCrossMarginLeverageAsync(...)` |
| Set isolated leverage | `client.UsdtFuturesApi.Trading.SetIsolatedMarginLeverageAsync(...)` |
| Place cross margin order | `client.UsdtFuturesApi.Trading.PlaceCrossMarginOrderAsync(...)` |
| Place isolated margin order | `client.UsdtFuturesApi.Trading.PlaceIsolatedMarginOrderAsync(...)` |
| Get cross open orders | `client.UsdtFuturesApi.Trading.GetCrossMarginOpenOrdersAsync(...)` |
| Get isolated open orders | `client.UsdtFuturesApi.Trading.GetIsolatedMarginOpenOrdersAsync(...)` |
| Get cross order | `client.UsdtFuturesApi.Trading.GetCrossMarginOrderAsync(...)` |
| Get isolated order | `client.UsdtFuturesApi.Trading.GetIsolatedMarginOrderAsync(...)` |
| Cancel cross order | `client.UsdtFuturesApi.Trading.CancelCrossMarginOrderAsync(...)` |
| Cancel isolated order | `client.UsdtFuturesApi.Trading.CancelIsolatedMarginOrderAsync(...)` |
| Cancel all cross orders | `client.UsdtFuturesApi.Trading.CancelAllCrossMarginOrdersAsync(...)` |
| Cancel all isolated orders | `client.UsdtFuturesApi.Trading.CancelAllIsolatedMarginOrdersAsync(...)` |
| Close cross position | `client.UsdtFuturesApi.Trading.CloseCrossMarginPositionAsync(...)` |
| Close isolated position | `client.UsdtFuturesApi.Trading.CloseIsolatedMarginPositionAsync(...)` |
| Place cross trigger order | `client.UsdtFuturesApi.Trading.PlaceCrossMarginTriggerOrderAsync(...)` |
| Place isolated trigger order | `client.UsdtFuturesApi.Trading.PlaceIsolatedMarginTriggerOrderAsync(...)` |
| Set cross TP/SL | `client.UsdtFuturesApi.Trading.SetCrossMarginTpSlAsync(...)` |
| Set isolated TP/SL | `client.UsdtFuturesApi.Trading.SetIsolatedMarginTpSlAsync(...)` |
| Cancel orders after timeout | `client.UsdtFuturesApi.Trading.CancelOrdersAfterAsync(...)` |

## Spot WebSocket

| User intent | HTX.Net member |
|---|---|
| Subscribe spot ticker | `socketClient.SpotApi.SubscribeToTickerUpdatesAsync("ETHUSDT", handler)` |
| Subscribe all spot tickers | `socketClient.SpotApi.SubscribeToTickerUpdatesAsync(handler)` |
| Subscribe spot klines | `socketClient.SpotApi.SubscribeToKlineUpdatesAsync("ETHUSDT", KlineInterval.OneMinute, handler)` |
| Subscribe spot book ticker | `socketClient.SpotApi.SubscribeToBookTickerUpdatesAsync("ETHUSDT", handler)` |
| Subscribe spot order book snapshots | `socketClient.SpotApi.SubscribeToPartialOrderBookUpdates1SecondAsync(...)` |
| Subscribe spot high-frequency order book | `socketClient.SpotApi.SubscribeToPartialOrderBookUpdates100MillisecondAsync(...)` |
| Subscribe spot incremental order book | `socketClient.SpotApi.SubscribeToOrderBookChangeUpdatesAsync(...)` |
| Subscribe spot trades | `socketClient.SpotApi.SubscribeToTradeUpdatesAsync("ETHUSDT", handler)` |
| Subscribe spot symbol details | `socketClient.SpotApi.SubscribeToSymbolDetailUpdatesAsync("ETHUSDT", handler)` |
| Subscribe spot account updates | `socketClient.SpotApi.SubscribeToAccountUpdatesAsync(handler)` |
| Subscribe spot order updates | `socketClient.SpotApi.SubscribeToOrderUpdatesAsync(...)` |
| Subscribe spot order detail updates | `socketClient.SpotApi.SubscribeToOrderDetailsUpdatesAsync(...)` |
| Socket API place spot order | `socketClient.SpotApi.PlaceOrderAsync(...)` |
| Socket API cancel spot order | `socketClient.SpotApi.CancelOrderAsync(...)` |

## USDT Futures WebSocket

| User intent | HTX.Net member |
|---|---|
| Subscribe futures ticker | `socketClient.UsdtFuturesApi.SubscribeToTickerUpdatesAsync("ETH-USDT", handler)` |
| Subscribe futures klines | `socketClient.UsdtFuturesApi.SubscribeToKlineUpdatesAsync("ETH-USDT", KlineInterval.OneMinute, handler)` |
| Subscribe futures book ticker | `socketClient.UsdtFuturesApi.SubscribeToBookTickerUpdatesAsync("ETH-USDT", handler)` |
| Subscribe futures order book | `socketClient.UsdtFuturesApi.SubscribeToOrderBookUpdatesAsync("ETH-USDT", mergeStep, handler)` |
| Subscribe futures incremental order book | `socketClient.UsdtFuturesApi.SubscribeToIncrementalOrderBookUpdatesAsync(...)` |
| Subscribe futures trades | `socketClient.UsdtFuturesApi.SubscribeToTradeUpdatesAsync("ETH-USDT", handler)` |
| Subscribe funding rate | `socketClient.UsdtFuturesApi.SubscribeToFundingRateUpdatesAsync(handler)` |
| Subscribe contract updates | `socketClient.UsdtFuturesApi.SubscribeToContractUpdatesAsync(handler)` |
| Subscribe system status | `socketClient.UsdtFuturesApi.SubscribeToSystemStatusUpdatesAsync(handler)` |
| Subscribe futures order updates | `socketClient.UsdtFuturesApi.SubscribeToOrderUpdatesAsync(MarginMode.Cross, handler)` |
| Subscribe isolated balance updates | `socketClient.UsdtFuturesApi.SubscribeToIsolatedMarginBalanceUpdatesAsync(handler)` |
| Subscribe cross balance updates | `socketClient.UsdtFuturesApi.SubscribeToCrossMarginBalanceUpdatesAsync(handler)` |
| Subscribe isolated position updates | `socketClient.UsdtFuturesApi.SubscribeToIsolatedMarginPositionUpdatesAsync(handler)` |
| Subscribe cross position updates | `socketClient.UsdtFuturesApi.SubscribeToCrossMarginPositionUpdatesAsync(handler)` |
| Subscribe futures user trades | `socketClient.UsdtFuturesApi.SubscribeToCrossMarginUserTradeUpdatesAsync(handler)` |

## SharedApis

Use SharedApis for exchange-agnostic code across HTX, Binance, Bybit, OKX, Kraken, and other CryptoExchange.Net libraries.

| User intent | HTX.Net member or interface |
|---|---|
| Shared spot REST client | `new HTXRestClient().SpotApi.SharedClient` |
| Shared USDT futures REST client | `new HTXRestClient().UsdtFuturesApi.SharedClient` |
| Shared spot socket client | `new HTXSocketClient().SpotApi.SharedClient` |
| Shared USDT futures socket client | `new HTXSocketClient().UsdtFuturesApi.SharedClient` |
| Discover shared capabilities | `client.SpotApi.SharedClient.Discover()` |
| Shared spot ticker REST | `ISpotTickerRestClient.GetSpotTickerAsync(new GetTickerRequest(symbol))` |
| Shared spot order REST | `ISpotOrderRestClient.PlaceSpotOrderAsync(...)` |
| Shared futures order REST | `IFuturesOrderRestClient.PlaceFuturesOrderAsync(...)` |
| Shared ticker socket | `ITickerSocketClient.SubscribeToTickerUpdatesAsync(...)` |
| Shared order book socket | `IOrderBookSocketClient.SubscribeToOrderBookUpdatesAsync(...)` |

Shared REST methods return `HttpResult<T>` / `HttpResult`; shared socket subscriptions return `WebSocketResult<UpdateSubscription>`; shared symbol/cache helpers such as `SupportsSpotSymbolAsync` and `SupportsFuturesSymbolAsync` can return `ExchangeCallResult<T>`.

For shared socket subscriptions, keep the concrete socket client and unsubscribe with `await socketClient.UnsubscribeAsync(subscription.Data)`.

## Result Handling

| Situation | Pattern |
|---|---|
| REST success check | `if (!result.Success) { Console.WriteLine(result.Error); return; }` |
| Socket subscription success check | `if (!sub.Success) { Console.WriteLine(sub.Error); return; }` where `sub` is `WebSocketResult<UpdateSubscription>` |
| Spot socket request success check | `if (!query.Success) { Console.WriteLine(query.Error); return; }` where `query` is `QueryResult<T>` or `QueryResult` |
| Read REST data | Read `result.Data` only after `result.Success` |
| Read shared helper data | Read `ExchangeCallResult<T>.Data` only after `.Success` |
| Retry decision | Retry only when `result.Error?.IsTransient == true` |
| Cancellation | Pass `ct: cancellationToken` |

## Common Routing Pitfalls

| Do not use | Use instead |
|---|---|
| `HTXClient` or `HuobiClient` | `HTXRestClient` |
| `HuobiRestClient` | `HTXRestClient` |
| `ApiCredentials` | `HTXCredentials` |
| `UsdtMarginSwapApi` in current code | `UsdtFuturesApi` |
| `SpotApi.Account.GetAccountInfoAsync()` | `SpotApi.Account.GetAccountsAsync()` plus `GetBalancesAsync(accountId)` |
| Futures symbol `ETHUSDT` | Futures contract code `ETH-USDT` |
| Spot symbol `ETH-USDT` | Spot symbol `ETHUSDT` |
| A generic futures `PlaceOrderAsync` | `PlaceCrossMarginOrderAsync` or `PlaceIsolatedMarginOrderAsync` |
| Cross/isolated futures methods interchangeably | Match the user's margin mode |
| `.Data` without `.Success` check | Check `.Success` first |
| `ITickerSocketClient.UnsubscribeAsync(...)` | Keep the concrete socket client and call `socketClient.UnsubscribeAsync(subscription.Data)` |
