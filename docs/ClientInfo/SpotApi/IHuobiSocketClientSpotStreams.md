---
title: IHuobiSocketClientSpotStreams
has_children: true
parent: Socket API documentation
---
*[generated documentation]*  
`HuobiSocketClient > SpotStreams`  
*Spot streams*
  

***

## GetKlinesAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#get-latest-tickers-for-all-pairs](https://huobiapi.github.io/docs/spot/v1/en/#get-latest-tickers-for-all-pairs)  
<p>

*Gets candlestick data for a symbol*  

```csharp  
var client = new HuobiClient();  
var result = await client.SpotStreams.GetKlinesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<IEnumerable<HuobiKline>>> GetKlinesAsync(string symbol, KlineInterval period);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to get the data for|
|period|The period of a single candlestick|

</p>

***

## GetOrderBookAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#market-by-price-incremental-update](https://huobiapi.github.io/docs/spot/v1/en/#market-by-price-incremental-update)  
<p>

*Gets the current order book for a symbol*  

```csharp  
var client = new HuobiClient();  
var result = await client.SpotStreams.GetOrderBookAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<HuobiIncementalOrderBook>> GetOrderBookAsync(string symbol, int levels);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to get the data for|
|levels|The amount of rows. 5, 20, 150 or 400|

</p>

***

## GetOrderBookWithMergeStepAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#market-depth](https://huobiapi.github.io/docs/spot/v1/en/#market-depth)  
<p>

*Gets the current order book for a symbol*  

```csharp  
var client = new HuobiClient();  
var result = await client.SpotStreams.GetOrderBookWithMergeStepAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<HuobiOrderBook>> GetOrderBookWithMergeStepAsync(string symbol, int mergeStep);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to get the data for|
|mergeStep|The way the results will be merged together|

</p>

***

## GetSymbolDetailsAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#market-details](https://huobiapi.github.io/docs/spot/v1/en/#market-details)  
<p>

*Gets details for a symbol*  

```csharp  
var client = new HuobiClient();  
var result = await client.SpotStreams.GetSymbolDetailsAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<HuobiSymbolDetails>> GetSymbolDetailsAsync(string symbol);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to get data for|

</p>

***

## GetTradeHistoryAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#trade-detail](https://huobiapi.github.io/docs/spot/v1/en/#trade-detail)  
<p>

*Gets a list of trades for a symbol*  

```csharp  
var client = new HuobiClient();  
var result = await client.SpotStreams.GetTradeHistoryAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<IEnumerable<HuobiSymbolTradeDetails>>> GetTradeHistoryAsync(string symbol);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to get trades for|

</p>

***

## SubscribeToAccountUpdatesAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#subscribe-account-change](https://huobiapi.github.io/docs/spot/v1/en/#subscribe-account-change)  
<p>

*Subscribe to updates of account balances*  

```csharp  
var client = new HuobiClient();  
var result = await client.SpotStreams.SubscribeToAccountUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToAccountUpdatesAsync(Action<DataEvent<HuobiAccountUpdate>> onAccountUpdate, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|onAccountUpdate|Event handler|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToBestOfferUpdatesAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#best-bid-offer](https://huobiapi.github.io/docs/spot/v1/en/#best-bid-offer)  
<p>

*Subscribe to changes of a symbol's best ask/bid*  

```csharp  
var client = new HuobiClient();  
var result = await client.SpotStreams.SubscribeToBestOfferUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToBestOfferUpdatesAsync(string symbol, Action<DataEvent<HuobiBestOffer>> onData, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|Symbol to subscribe to|
|onData|Data handler|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToKlineUpdatesAsync  

<p>

*Subscribes to candlestick updates for a symbol*  

```csharp  
var client = new HuobiClient();  
var result = await client.SpotStreams.SubscribeToKlineUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string symbol, KlineInterval period, Action<DataEvent<HuobiKline>> onData, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to subscribe to|
|period|The period of a single candlestick|
|onData|The handler for updates|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToOrderBookChangeUpdatesAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#market-by-price-incremental-update](https://huobiapi.github.io/docs/spot/v1/en/#market-by-price-incremental-update)  
<p>

*Subscribes to order book updates for a symbol,*  

```csharp  
var client = new HuobiClient();  
var result = await client.SpotStreams.SubscribeToOrderBookChangeUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToOrderBookChangeUpdatesAsync(string symbol, int levels, Action<DataEvent<HuobiIncementalOrderBook>> onData, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to subscribe to|
|levels|The number of price levels. 5, 20, 150 or 400|
|onData|The handler for updates|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToOrderDetailsUpdatesAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#subscribe-trade-details-amp-order-cancellation-post-clearing](https://huobiapi.github.io/docs/spot/v1/en/#subscribe-trade-details-amp-order-cancellation-post-clearing)  
<p>

*Subscribe to detailed order matched/canceled updates*  

```csharp  
var client = new HuobiClient();  
var result = await client.SpotStreams.SubscribeToOrderDetailsUpdatesAsync();  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToOrderDetailsUpdatesAsync(string? symbol = default, Action<DataEvent<HuobiTradeUpdate>>? onOrderMatch = default, Action<DataEvent<HuobiOrderCancelationUpdate>>? onOrderCancel = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ symbol|Subscribe to a specific symbol|
|_[Optional]_ onOrderMatch|Event handler for the order matched event|
|_[Optional]_ onOrderCancel|Event handler for the order canceled event|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToOrderUpdatesAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#subscribe-order-updates](https://huobiapi.github.io/docs/spot/v1/en/#subscribe-order-updates)  
<p>

*Subscribe to updates of orders*  

```csharp  
var client = new HuobiClient();  
var result = await client.SpotStreams.SubscribeToOrderUpdatesAsync();  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToOrderUpdatesAsync(string? symbol = default, Action<DataEvent<HuobiSubmittedOrderUpdate>>? onOrderSubmitted = default, Action<DataEvent<HuobiMatchedOrderUpdate>>? onOrderMatched = default, Action<DataEvent<HuobiCanceledOrderUpdate>>? onOrderCancelation = default, Action<DataEvent<HuobiTriggerFailureOrderUpdate>>? onConditionalOrderTriggerFailure = default, Action<DataEvent<HuobiOrderUpdate>>? onConditionalOrderCanceled = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ symbol|Subscribe on a specific symbol|
|_[Optional]_ onOrderSubmitted|Event handler for the order submitted event|
|_[Optional]_ onOrderMatched|Event handler for the order matched event|
|_[Optional]_ onOrderCancelation|Event handler for the order cancelled event|
|_[Optional]_ onConditionalOrderTriggerFailure|Event handler for the conditional order trigger failed event|
|_[Optional]_ onConditionalOrderCanceled|Event handler for the condition order canceled event|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToPartialOrderBookUpdates100MilisecondAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#market-by-price-incremental-update](https://huobiapi.github.io/docs/spot/v1/en/#market-by-price-incremental-update)  
<p>

*Subscribes to order book updates for a symbol*  

```csharp  
var client = new HuobiClient();  
var result = await client.SpotStreams.SubscribeToPartialOrderBookUpdates100MilisecondAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdates100MilisecondAsync(string symbol, int levels, Action<DataEvent<HuobiOrderBook>> onData, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to subscribe to|
|levels|The number of price levels. 5, 10 or 20|
|onData|The handler for updates|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToPartialOrderBookUpdates1SecondAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#market-depth](https://huobiapi.github.io/docs/spot/v1/en/#market-depth)  
<p>

*Subscribes to order book updates for a symbol*  

```csharp  
var client = new HuobiClient();  
var result = await client.SpotStreams.SubscribeToPartialOrderBookUpdates1SecondAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToPartialOrderBookUpdates1SecondAsync(string symbol, int mergeStep, Action<DataEvent<HuobiOrderBook>> onData, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to subscribe to|
|mergeStep|The way the results will be merged together|
|onData|The handler for updates|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToSymbolDetailUpdatesAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#market-details](https://huobiapi.github.io/docs/spot/v1/en/#market-details)  
<p>

*Subscribes to symbol detail updates for a symbol*  

```csharp  
var client = new HuobiClient();  
var result = await client.SpotStreams.SubscribeToSymbolDetailUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToSymbolDetailUpdatesAsync(string symbol, Action<DataEvent<HuobiSymbolDetails>> onData, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to subscribe to|
|onData|The handler for updates|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToTickerUpdatesAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#market-ticker](https://huobiapi.github.io/docs/spot/v1/en/#market-ticker)  
<p>

*Subscribes to updates for a symbol*  

```csharp  
var client = new HuobiClient();  
var result = await client.SpotStreams.SubscribeToTickerUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(string symbol, Action<DataEvent<HuobiSymbolTick>> onData, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to subscribe|
|onData|The handler for updates|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToTickerUpdatesAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#market-ticker](https://huobiapi.github.io/docs/spot/v1/en/#market-ticker)  
<p>

*Subscribes to updates for all tickers*  

```csharp  
var client = new HuobiClient();  
var result = await client.SpotStreams.SubscribeToTickerUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToTickerUpdatesAsync(Action<DataEvent<HuobiSymbolDatas>> onData, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|onData|The handler for updates|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>

***

## SubscribeToTradeUpdatesAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#trade-detail](https://huobiapi.github.io/docs/spot/v1/en/#trade-detail)  
<p>

*Subscribes to trade updates for a symbol*  

```csharp  
var client = new HuobiClient();  
var result = await client.SpotStreams.SubscribeToTradeUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string symbol, Action<DataEvent<HuobiSymbolTrade>> onData, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to subscribe to|
|onData|The handler for updates|
|_[Optional]_ ct|Cancellation token for closing this subscription|

</p>
