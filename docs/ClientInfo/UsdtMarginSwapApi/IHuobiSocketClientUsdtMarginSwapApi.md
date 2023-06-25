---
title: IHuobiSocketClientUsdtMarginSwapApi
has_children: true
parent: Rest API documentation
---
*[generated documentation]*  
`HuobiClient > UsdtMarginSwapApi`  
*Usdt margin swap streams*
  

***

## SubscribeToBasisUpdatesAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-subscribe-basis-data](https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-subscribe-basis-data)  
<p>

*Subscribe to basis updates*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.SubscribeToBasisUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToBasisUpdatesAsync(string contractCode, KlineInterval period, string priceType, Action<DataEvent<HuobiUsdtMarginSwapBasisUpdate>> onData, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|contractCode|Contract code|
|period|Period|
|priceType|Price type|
|onData|Data handler|
|_[Optional]_ ct|Cancellation token|

</p>

***

## SubscribeToBestOfferUpdatesAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-subscribe-market-bbo-data-push](https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-subscribe-market-bbo-data-push)  
<p>

*Subscribe to best offer updates*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.SubscribeToBestOfferUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToBestOfferUpdatesAsync(string contractCode, Action<DataEvent<HuobiBestOfferUpdate>> onData, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|contractCode|Contract code|
|onData|Data handler|
|_[Optional]_ ct|Cancellation token|

</p>

***

## SubscribeToEstimatedFundingRateKlineUpdatesAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-subscribe-estimated-funding-rate-kline-data](https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-subscribe-estimated-funding-rate-kline-data)  
<p>

*Subscribe to estimated funding rate kline updates*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.SubscribeToEstimatedFundingRateKlineUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToEstimatedFundingRateKlineUpdatesAsync(string contractCode, KlineInterval period, Action<DataEvent<HuobiKline>> onData, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|contractCode|Contract code|
|period|Period|
|onData|Data handler|
|_[Optional]_ ct|Cancellation token|

</p>

***

## SubscribeToIncrementalOrderBookUpdatesAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-subscribe-incremental-market-depth-data](https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-subscribe-incremental-market-depth-data)  
<p>

*Subscribe to incremental order book updates*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.SubscribeToIncrementalOrderBookUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToIncrementalOrderBookUpdatesAsync(string contractCode, bool snapshot, int limit, Action<DataEvent<HuobiUsdtMarginSwapIncementalOrderBook>> onData, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|contractCode|Contract code|
|snapshot|Snapshot or incremental|
|limit|Depth|
|onData|Data handler|
|_[Optional]_ ct|Cancellation token|

</p>

***

## SubscribeToIndexKlineUpdatesAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-subscribe-index-kline-data](https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-subscribe-index-kline-data)  
<p>

*Subscribe to index kline updates*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.SubscribeToIndexKlineUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToIndexKlineUpdatesAsync(string contractCode, KlineInterval period, Action<DataEvent<HuobiKline>> onData, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|contractCode|Contract code|
|period|Period|
|onData|Data handler|
|_[Optional]_ ct|Cancellation token|

</p>

***

## SubscribeToKlineUpdatesAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-subscribe-kline-data](https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-subscribe-kline-data)  
<p>

*Subscribe to kline updates*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.SubscribeToKlineUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToKlineUpdatesAsync(string contractCode, KlineInterval period, Action<DataEvent<HuobiKline>> onData, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|contractCode|Contract code|
|period|Period|
|onData|Data handler|
|_[Optional]_ ct|Cancellation token|

</p>

***

## SubscribeToMarkPriceKlineUpdatesAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-subscribe-kline-data-of-mark-price](https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-subscribe-kline-data-of-mark-price)  
<p>

*Subscribe to mark price updates*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.SubscribeToMarkPriceKlineUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToMarkPriceKlineUpdatesAsync(string contractCode, KlineInterval period, Action<DataEvent<HuobiKline>> onData, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|contractCode|Contract code|
|period|Period|
|onData|Data handler|
|_[Optional]_ ct|Cancellation token|

</p>

***

## SubscribeToOrderBookUpdatesAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-subscribe-market-depth-data](https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-subscribe-market-depth-data)  
<p>

*Subscribe to order book updates*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.SubscribeToOrderBookUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToOrderBookUpdatesAsync(string contractCode, int mergeStep, Action<DataEvent<HuobiUsdtMarginSwapIncementalOrderBook>> onData, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|contractCode|Contract code|
|mergeStep|Merge step|
|onData|Data handler|
|_[Optional]_ ct|Cancellation token|

</p>

***

## SubscribeToPremiumIndexKlineUpdatesAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-subscribe-premium-index-kline-data](https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-subscribe-premium-index-kline-data)  
<p>

*Subscribe to premium index kline updates*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.SubscribeToPremiumIndexKlineUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToPremiumIndexKlineUpdatesAsync(string contractCode, KlineInterval period, Action<DataEvent<HuobiKline>> onData, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|contractCode|Contract code|
|period|Period|
|onData|Data handler|
|_[Optional]_ ct|Cancellation token|

</p>

***

## SubscribeToSymbolTickerUpdatesAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-subscribe-market-detail-data](https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-subscribe-market-detail-data)  
<p>

*Subscribe to symbol ticker updates*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.SubscribeToSymbolTickerUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToSymbolTickerUpdatesAsync(string contractCode, Action<DataEvent<HuobiSymbolTickUpdate>> onData, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|contractCode|Contract code|
|onData|Data handler|
|_[Optional]_ ct|Cancellation token|

</p>

***

## SubscribeToTradeUpdatesAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-subscribe-trade-detail-data](https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-subscribe-trade-detail-data)  
<p>

*Subscribe to symbol trade updates*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.SubscribeToTradeUpdatesAsync(/* parameters */);  
```  

```csharp  
Task<CallResult<UpdateSubscription>> SubscribeToTradeUpdatesAsync(string contractCode, Action<DataEvent<HuobiUsdtMarginSwapTradesUpdate>> onData, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|contractCode|Contract code|
|onData|Data handler|
|_[Optional]_ ct|Cancellation token|

</p>
