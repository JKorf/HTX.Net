# Usage examples

Snippets are independent. Those using `client` assume a reused `HTXRestClient`, configured with credentials for private calls. Trading and leverage writes change live state when executed; see [operational considerations](safety.md).

## Imports

```csharp
using System;
using System.Linq;
using System.Threading.Tasks;
using HTX.Net;
using HTX.Net.Clients;
using HTX.Net.Enums;
using HTX.Net.Interfaces.Clients;
using CryptoExchange.Net.SharedApis;
using Microsoft.Extensions.DependencyInjection;
```

## Public spot ticker and results

```csharp
using var client = new HTXRestClient();
var result = await client.SpotApi.ExchangeData.GetTickerAsync("ETHUSDT");
if (!result.Success)
{
    Console.WriteLine(result.Error);
    return;
}
Console.WriteLine(result.Data.ClosePrice);
```

## Credentials, account selection, and balances

```csharp
using var client = new HTXRestClient(options =>
{
    options.ApiCredentials = new HTXCredentials("API_KEY", "API_SECRET");
});
var accounts = await client.SpotApi.Account.GetAccountsAsync();
if (!accounts.Success)
{
    Console.WriteLine(accounts.Error);
    return;
}
var account = accounts.Data.FirstOrDefault(x => x.Type == AccountType.Spot && x.Status == AccountStatus.Working);
if (account is null)
{
    Console.WriteLine("No spot account returned.");
    return;
}
var balances = await client.SpotApi.Account.GetBalancesAsync(account.Id);
if (!balances.Success)
{
    Console.WriteLine(balances.Error);
    return;
}
foreach (var balance in balances.Data)
    Console.WriteLine($"{balance.Asset} ({balance.Type}): {balance.Balance}");
```

## Spot limit order and cancellation

```csharp
var accounts = await client.SpotApi.Account.GetAccountsAsync();
if (!accounts.Success)
{
    Console.WriteLine(accounts.Error);
    return;
}
var account = accounts.Data.FirstOrDefault(x => x.Type == AccountType.Spot && x.Status == AccountStatus.Working);
if (account is null)
    return;
var order = await client.SpotApi.Trading.PlaceOrderAsync(
    account.Id, "ETHUSDT", OrderSide.Buy, OrderType.Limit, quantity: 0.01m, price: 1000m);
if (!order.Success)
{
    Console.WriteLine(order.Error);
    return;
}
var cancel = await client.SpotApi.Trading.CancelOrderAsync(order.Data);
if (!cancel.Success)
    Console.WriteLine(cancel.Error);
```

## USDT futures cross-margin order

Select account mode, contract quantity, leverage, and opening/closing intent before executing this example. Isolated margin uses separate methods.

```csharp
var leverage = await client.UsdtFuturesApi.Trading.SetCrossMarginLeverageAsync(
    leverageRate: 5, contractCode: "ETH-USDT");
if (!leverage.Success)
{
    Console.WriteLine(leverage.Error);
    return;
}
var order = await client.UsdtFuturesApi.Trading.PlaceCrossMarginOrderAsync(
    quantity: 1, side: OrderSide.Buy, leverageRate: 5,
    orderPriceType: OrderPriceType.Limit, contractCode: "ETH-USDT",
    offset: Offset.Open, price: 1000m);
if (!order.Success)
{
    Console.WriteLine(order.Error);
    return;
}
Console.WriteLine(order.Data.OrderId);
```

## V5 futures account read

```csharp
var result = await client.UsdtFuturesV5Api.Account.GetAccountBalanceAsync();
if (!result.Success)
{
    Console.WriteLine(result.Error);
    return;
}
Console.WriteLine(result.Data.Equity);
```

## Subscription lifecycle

```csharp
using var socketClient = new HTXSocketClient();
var subscription = await socketClient.SpotApi.SubscribeToTickerUpdatesAsync(
    "ETHUSDT", update => Console.WriteLine(update.Data.ClosePrice));
if (!subscription.Success)
{
    Console.WriteLine(subscription.Error);
    return;
}
try
{
    await Task.Delay(TimeSpan.FromSeconds(30));
}
finally
{
    await socketClient.UnsubscribeAsync(subscription.Data);
}
```

Private streams require credentials on the socket client. Spot order requests return query results; V5 futures also has order queries. Do not pass query result data to `UnsubscribeAsync`.

## Shared API V2

```csharp
IGetTickerRest ticker = client.SpotApi.SharedApi;
var symbol = new SharedSymbol(TradingMode.Spot, "ETH", "USDT");
var result = await ticker.GetTickerAsync(new GetTickerRequest(symbol));
if (!result.Success)
{
    Console.WriteLine(result.Error);
    return;
}
Console.WriteLine(result.Data.LastPrice);
```

Read [API surfaces](api-surfaces.md#shared-api-v2) for catalogs, account parameters, and differences between futures versions.

## Dependency injection

```csharp
var services = new ServiceCollection();
services.AddHTX(options =>
{
    options.Rest.ApiCredentials = new HTXCredentials("API_KEY", "API_SECRET");
    options.Socket.ApiCredentials = new HTXCredentials("API_KEY", "API_SECRET");
});
using var provider = services.BuildServiceProvider();
var client = provider.GetRequiredService<IHTXRestClient>();
var shared = provider.GetRequiredService<IHTXSharedApiClient>();
```

## Complete examples (checkout-only)

The documentation test compiles these programs without executing exchange calls. Running them can contact HTX and execute illustrated orders, position changes, and leverage settings.

- [Spot quickstart](../../../../Examples/ai-friendly/01-spot-quickstart.cs)
- [Futures](../../../../Examples/ai-friendly/02-futures.cs)
- [WebSockets](../../../../Examples/ai-friendly/03-websocket.cs)
- [Shared API](../../../../Examples/ai-friendly/04-multi-exchange.cs)
- [Error handling](../../../../Examples/ai-friendly/05-error-handling.cs)
