# API surfaces

## Client roots, symbols, and account IDs

`client` below is an `HTXRestClient`; `socketClient` is an `HTXSocketClient`. Both expose `SpotApi`, `UsdtFuturesApi`, and `UsdtFuturesV5Api`. Spot REST groups are `ExchangeData`, `Account`, `Margin`, `SubAccount`, and `Trading`. USDT futures has market/account/subaccount/trading groups; V5 has market/account/trading groups.

Spot symbols are compact, for example `ETHUSDT`; the shared formatter produces lowercase `ethusdt`. USDT futures contract codes use dashes, for example `ETH-USDT`. Query metadata for the selected market; this library does not provide a separate Coin-M client.

Spot balances and placement need an account ID from `SpotApi.Account.GetAccountsAsync()`. Select the intended `AccountType` and verify the returned account state. Spot and margin accounts are different scopes; an arbitrary first account is not a reliable trading account.

Native `HTXSymbol.EnableRpi` exposes RPI availability, and `HTXSymbolTradeDetails.IsRpiTrade` identifies RPI trades. Use symbol constraints and contract metadata for order sizing.

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
| Get latest trade | `client.SpotApi.ExchangeData.GetLastTradeAsync("ETHUSDT")`|
| Get recent trades | `client.SpotApi.ExchangeData.GetTradeHistoryAsync("ETHUSDT")`|
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

## USDT Futures V5

V5 has `Account`, `ExchangeData`, and `Trading` groups with distinct models and order parameters.

| Intent | Method |
| --- | --- |
| Get Funding Rate | `client.UsdtFuturesV5Api.ExchangeData.GetFundingRateAsync(...)` |
| Get Funding Rate History | `client.UsdtFuturesV5Api.ExchangeData.GetFundingRateHistoryAsync(...)` |
| Get Open Interest | `client.UsdtFuturesV5Api.ExchangeData.GetOpenInterestAsync(...)` |
| Get Price Limits | `client.UsdtFuturesV5Api.ExchangeData.GetPriceLimitsAsync(...)` |
| Get Risk Limits | `client.UsdtFuturesV5Api.ExchangeData.GetRiskLimitsAsync(...)` |
| Get Asset Mode | `client.UsdtFuturesV5Api.Account.GetAssetModeAsync(...)` |
| Set Asset Mode | `client.UsdtFuturesV5Api.Account.SetAssetModeAsync(...)` |
| Get Account Balance | `client.UsdtFuturesV5Api.Account.GetAccountBalanceAsync(...)` |
| Get Bills | `client.UsdtFuturesV5Api.Account.GetBillsAsync(...)` |
| Get Position Mode | `client.UsdtFuturesV5Api.Account.GetPositionModeAsync(...)` |
| Set Position Mode | `client.UsdtFuturesV5Api.Account.SetPositionModeAsync(...)` |
| Get Leverage | `client.UsdtFuturesV5Api.Account.GetLeverageAsync(...)` |
| Set Leverage | `client.UsdtFuturesV5Api.Account.SetLeverageAsync(...)` |
| Open positions | `client.UsdtFuturesV5Api.Account.GetOpenPositionsAsync(...)` |
| Position risk limits | `client.UsdtFuturesV5Api.Account.GetRiskLimitsAsync(...)` |
| Get Risk Limit Tiers | `client.UsdtFuturesV5Api.Account.GetRiskLimitTiersAsync(...)` |
| Place Order | `client.UsdtFuturesV5Api.Trading.PlaceOrderAsync(...)` |
| Cancel Order | `client.UsdtFuturesV5Api.Trading.CancelOrderAsync(...)` |
| Cancel All Orders | `client.UsdtFuturesV5Api.Trading.CancelAllOrdersAsync(...)` |
| Get Order | `client.UsdtFuturesV5Api.Trading.GetOrderAsync(...)` |
| Get Open Orders | `client.UsdtFuturesV5Api.Trading.GetOpenOrdersAsync(...)` |

V5 sockets expose account, order, execution, and position updates, plus futures order queries. Use `socketClient.UsdtFuturesV5Api` and its V5-specific request/response models. Market subscriptions remain available on `UsdtFuturesApi`.

## Shared API V2

Shared surfaces exist on REST/socket `SpotApi`, `UsdtFuturesApi`, and `UsdtFuturesV5Api`. The DI aggregate `IHTXSharedApiClient` exposes `SpotRest`, `UsdtFuturesRest`, `UsdtFuturesV5Rest`, `SpotSocket`, `UsdtFuturesSocket`, and `UsdtFuturesV5Socket`.

| Surface | Representative capabilities |
| --- | --- |
| Spot REST | `IGetSpotSymbolsRest`, `IGetTickerRest`, `IGetAllTickersRest`, balances, assets, deposits/withdrawals, fees, transfers, `IPlaceSpotOrderRest`, order/client-ID lifecycle and trigger orders |
| USDT futures REST | `IGetFuturesSymbolsRest`, ticker/book/klines, mark/index klines, funding/open interest, `IPlaceFuturesOrderRest`, order/client-ID lifecycle, `IGetPositionsRest`, `ICloseFullPositionRest`, position mode, fees, trigger orders and TP/SL |
| USDT futures V5 REST | `IGetBalancesRest`, `IGetFundingInfoRest`, `IGetFundingRateHistoryRest`, `IPlaceFuturesOrderRest`, order/client-ID lifecycle, cancel-all, `IGetPositionsRest`, position mode, `IGetLeverageRest`, `ISetLeverageRest`, `IGetLeverageTiersRest` |
| Spot socket | Public ticker/all-ticker/trade/book-ticker/kline/book subscriptions, private balance/order/execution subscriptions, `IPlaceSpotOrderSocket`, `ICancelSpotOrderSocket` |
| USDT futures socket | Public ticker/trade/book-ticker/kline/book subscriptions, private balance/futures-order/execution/position subscriptions |
| USDT futures V5 socket | `ISubscribeBalancesSocket`, `ISubscribeFuturesOrdersSocket`, `ISubscribeUserTradesSocket`, `ISubscribePositionsSocket`, `IPlaceFuturesOrderSocket`, `ICancelFuturesOrderSocket`, client-ID and cancel-all socket capabilities |

V5 does not duplicate the older futures market capability inventory. Select a surface by actual supported operation rather than assuming every version implements the same capabilities.

For dynamic selection, call the aggregate's `GetCapability` with a descriptor such as `SharedCapabilities.Tickers.GetTicker.Rest` and the required trading mode. Check for a missing match before accessing `Capability` and `Options`. `GetCapabilities` enumerates implementations and `Discover()` describes operations. Multiple futures implementations can match a capability; select the intended API surface and its rules. With multiple exchanges registered, resolve `IEnumerable<TCapability>` and select by exchange and supported mode.

### Catalogs, quantities, and prerequisites

Load `IGetSpotSymbolsRest.GetSpotSymbolsAsync(new GetSymbolsRequest())` or `IGetFuturesSymbolsRest.GetFuturesSymbolsAsync(new GetSymbolsRequest())` successfully before reading `SpotSymbolCatalog` or `FuturesSymbolCatalog`. The futures catalog comes from the `UsdtFuturesApi` capability. Requests support asset-type/subtype filters; metadata includes `DisplayName`, constraints, and classifications (`Crypto`, `Fiat`, `TradFi`; applicable `StableCoin`, `Equity`, `Commodity` subtypes).

Shared orders use `SharedOrderQuantity`. Inspect spot capability quantity options for order type/side, especially market buys in quote currency. Futures orders use `QuantityInContracts`; futures books use `SharedQuantityType.Contracts`, while spot books use `SharedQuantityType.BaseAsset`.

Spot shared balances and placement require `AccountId` in `ExchangeParameters`, including socket placement. Older futures account/subscription capabilities can require the `MarginMode` exchange parameter; select cross or isolated deliberately. Older shared futures placement requires leverage, whereas V5 has separate leverage capabilities and different order configuration. Shared futures client-order IDs must be integer-compatible. Read each operation's options instead of applying one version's rules to another.

Shared REST uses `HttpResult<T>` / `HttpResult`, subscriptions use `WebSocketResult<UpdateSubscription>`, socket queries use `QueryResult<T>` / `QueryResult`, and non-I/O symbol/cache helpers can return `ExchangeCallResult<T>`.

## Environments and helpers

`HTXEnvironment.Live` is the built-in environment. `CreateCustom` requires spot REST/socket and futures REST/socket addresses; custom endpoints do not themselves supply a testnet.

Checkout-only: [public interfaces](../../../../HTX.Net/Interfaces/Clients/), [local order books](../../../../HTX.Net/SymbolOrderBooks/), and [tracker factory](../../../../HTX.Net/HTXTrackerFactory.cs). Use maintained books when synchronized snapshots/deltas are needed.
