---
title: Examples
nav_order: 3
---

## Basic operations
Make sure to read the [CryptoExchange.Net documentation](https://jkorf.github.io/CryptoExchange.Net/Clients.html#processing-request-responses) on processing responses.

### Get market data
```csharp
// Getting info on all symbols
var symbolData = await huobiClient.SpotApi.ExchangeData.GetSymbolsAsync();

// Getting tickers for all symbols
var tickerData = await huobiClient.SpotApi.ExchangeData.GetTickersAsync();

// Getting the order book of a symbol
var orderBookData = await huobiClient.SpotApi.ExchangeData.GetOrderBookAsync("BTC-USDT", 0);

// Getting recent trades of a symbol
var tradeHistoryData = await huobiClient.SpotApi.ExchangeData.GetTradeHistoryAsync("BTC-USDT");
```

### Retrieving accounts
A Huobi account is internally split into different sub-accounts. For most actions you'll need to pass in an account id. Here's how to retrieve these accounts:
```csharp
var accounts = await huobiClient.SpotApi.Account.GetAccountsAsync();
```
The account id's are static, so you should retrieve these once and keep track of them somewhere so you can pass them into methods later without the need to re-request this info.

### Requesting balances
```csharp
var accounts = await huobiClient.SpotApi.Account.GetAccountsAsync();
var symbolData = await huobiClient.SpotApi.Account.GetBalancesAsync(accounts.Data.Single(d => d.Type == AccountType.Spot).Id);
```
### Placing order
```csharp
// Placing a buy limit order for 0.001 BTC at a price of 50000USDT each
var orderData = await huobiClient.SpotApi.Trading.PlaceOrderAsync(
                accountId,
                "BTC-USDT",
                OrderSide.Buy,
                OrderType.Limit,
                0.001m,
                50000);
        
// Placing a market buy order for 50USDT. Buy market orders specify the quantity in quote quantity
var orderData = await huobiClient.SpotApi.Trading.PlaceOrderAsync(
                accountId,
                "BTC-USDT",
                OrderSide.Buy,
                OrderType.Market,
                50);            
                
                                                    
// Place a stop loss order, place a limit order of 0.001 BTC at 39000USDT each when the last trade price drops below 40000USDT
var orderData = await huobiClient.SpotApi.Trading.PlaceOrderAsync(
                accountId,
                "BTC-USDT",
                OrderSide.Sell,
                OrderType.StopLimit,
                0.001m,
                39000,
                stopPrice: 40000,
                stopOperator: Operator.LesserThanOrEqual);
```

### Requesting a specific order
```csharp
// Request info on order with id `1234`
var orderData = await huobiClient.SpotApi.Trading.GetOrderAsync(1234);
```

### Requesting order history
```csharp
// Get all orders conform the parameters
 var ordersData = await huobiClient.SpotApi.Trading.GetClosedOrdersAsync("btcusdt");
```

### Cancel order
```csharp
// Cancel order with id `1234`
var orderData = await huobiClient.SpotApi.Trading.CancelOrderAsync(1234);
```

### Get user trades
```csharp
var userTradesResult = await huobiClient.SpotApi.Trading.GetUserTradesAsync();
```

### Subscribing to market data updates
```csharp
var subscribeResult = await huobiSocket.SpotApi.SubscribeToTickerUpdatesAsync(data =>
{
    // Handle ticker data
});
```

### Subscribing to order updates
```csharp
// Any of these handlers can passed as null if you're not interested in the update type
var subscribeResult = await huobiSocket.SpotApi.SubscribeToOrderUpdatesAsync(
    null,
    data =>
    {
        // Handle order submitted update
    },
    data =>
    {
        // Handle order matched update
    },
    data =>
    {
        // Handle order cancel update
    },
    data =>
    {
        // Handle conditional order trigger failure update
    }
    , data =>
    {
        // Handle conditional order canceled update
    });
```
