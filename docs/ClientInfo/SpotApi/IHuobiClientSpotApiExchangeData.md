---
title: IHuobiClientSpotApiExchangeData
has_children: false
parent: IHuobiRestClientSpotApi
grand_parent: Rest API documentation
---
*[generated documentation]*  
`HuobiRestClient > SpotApi > ExchangeData`  
*Huobi exchange data endpoints. Exchange data includes market data (tickers, order books, etc) and system status.*
  

***

## GetAssetDetailsAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#apiv2-currency-amp-chains](https://huobiapi.github.io/docs/spot/v1/en/#apiv2-currency-amp-chains)  
<p>

*Gets a list of supported currencies and chains*  

```csharp  
var client = new HuobiRestClient();  
var result = await client.SpotApi.ExchangeData.GetAssetDetailsAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<HuobiAssetInfo>>> GetAssetDetailsAsync(string? asset = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ asset|Filter by asset|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetAssetsAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#get-all-supported-currencies](https://huobiapi.github.io/docs/spot/v1/en/#get-all-supported-currencies)  
<p>

*Gets a list of supported currencies*  

```csharp  
var client = new HuobiRestClient();  
var result = await client.SpotApi.ExchangeData.GetAssetsAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<string>>> GetAssetsAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetKlinesAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#get-klines-candles](https://huobiapi.github.io/docs/spot/v1/en/#get-klines-candles)  
<p>

*Get candlestick data for a symbol*  

```csharp  
var client = new HuobiRestClient();  
var result = await client.SpotApi.ExchangeData.GetKlinesAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<HuobiKline>>> GetKlinesAsync(string symbol, KlineInterval period, int? limit = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to get the data for|
|period|The period of a single candlestick|
|_[Optional]_ limit|The amount of candlesticks|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetLastTradeAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#get-the-last-trade](https://huobiapi.github.io/docs/spot/v1/en/#get-the-last-trade)  
<p>

*Gets the last trade for a symbol*  

```csharp  
var client = new HuobiRestClient();  
var result = await client.SpotApi.ExchangeData.GetLastTradeAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<HuobiSymbolTrade>> GetLastTradeAsync(string symbol, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to request for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetNavAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#get-real-time-nav](https://huobiapi.github.io/docs/spot/v1/en/#get-real-time-nav)  
<p>

*Gets real time NAV for ETP*  

```csharp  
var client = new HuobiRestClient();  
var result = await client.SpotApi.ExchangeData.GetNavAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<HuobiNav>> GetNavAsync(string symbol, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to get the data for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetOrderBookAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#get-market-depth](https://huobiapi.github.io/docs/spot/v1/en/#get-market-depth)  
<p>

*Gets the order book for a symbol*  

```csharp  
var client = new HuobiRestClient();  
var result = await client.SpotApi.ExchangeData.GetOrderBookAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<HuobiOrderBook>> GetOrderBookAsync(string symbol, int mergeStep, int? limit = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to request for|
|mergeStep|The way the results will be merged together|
|_[Optional]_ limit|The depth of the book|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetServerTimeAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#get-current-timestamp](https://huobiapi.github.io/docs/spot/v1/en/#get-current-timestamp)  
<p>

*Gets the server time*  

```csharp  
var client = new HuobiRestClient();  
var result = await client.SpotApi.ExchangeData.GetServerTimeAsync();  
```  

```csharp  
Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetSymbolDetails24HAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#get-the-last-24h-market-summary](https://huobiapi.github.io/docs/spot/v1/en/#get-the-last-24h-market-summary)  
<p>

*Gets 24h stats for a symbol*  

```csharp  
var client = new HuobiRestClient();  
var result = await client.SpotApi.ExchangeData.GetSymbolDetails24HAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<HuobiSymbolDetails>> GetSymbolDetails24HAsync(string symbol, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to get the data for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetSymbolsAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#get-all-supported-trading-symbol](https://huobiapi.github.io/docs/spot/v1/en/#get-all-supported-trading-symbol)  
<p>

*Gets a list of supported symbols*  

```csharp  
var client = new HuobiRestClient();  
var result = await client.SpotApi.ExchangeData.GetSymbolsAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<HuobiSymbol>>> GetSymbolsAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetSymbolStatusAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#get-market-status](https://huobiapi.github.io/docs/spot/v1/en/#get-market-status)  
<p>

*Gets the current market status*  

```csharp  
var client = new HuobiRestClient();  
var result = await client.SpotApi.ExchangeData.GetSymbolStatusAsync();  
```  

```csharp  
Task<WebCallResult<HuobiSymbolStatus>> GetSymbolStatusAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetTickerAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#get-latest-aggregated-ticker](https://huobiapi.github.io/docs/spot/v1/en/#get-latest-aggregated-ticker)  
<p>

*Gets the ticker, including the best bid / best ask for a symbol*  

```csharp  
var client = new HuobiRestClient();  
var result = await client.SpotApi.ExchangeData.GetTickerAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<HuobiSymbolTickMerged>> GetTickerAsync(string symbol, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to get the ticker for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetTickersAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#get-latest-tickers-for-all-pairs](https://huobiapi.github.io/docs/spot/v1/en/#get-latest-tickers-for-all-pairs)  
<p>

*Gets the latest ticker for all symbols*  

```csharp  
var client = new HuobiRestClient();  
var result = await client.SpotApi.ExchangeData.GetTickersAsync();  
```  

```csharp  
Task<WebCallResult<HuobiSymbolTicks>> GetTickersAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetTradeHistoryAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#get-the-most-recent-trades](https://huobiapi.github.io/docs/spot/v1/en/#get-the-most-recent-trades)  
<p>

*Get the last x trades for a symbol*  

```csharp  
var client = new HuobiRestClient();  
var result = await client.SpotApi.ExchangeData.GetTradeHistoryAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<HuobiSymbolTrade>>> GetTradeHistoryAsync(string symbol, int? limit = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to get trades for|
|_[Optional]_ limit|The max number of results|
|_[Optional]_ ct|Cancellation token|

</p>
