---
title: IHuobiClientUsdtMarginSwapApiExchangeData
has_children: false
parent: IHuobiClientUsdtMarginSwapApi
grand_parent: Rest API documentation
---
*[generated documentation]*  
`HuobiClient > UsdtMarginSwapApi > ExchangeData`  
*Huobi usdt margin swap exchange data endpoints. Exchange data includes market data (tickers, order books, etc) and system status.*
  

***

## GetBasisDataAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-basis-data](https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-basis-data)  
<p>

*Get basis data*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.ExchangeData.GetBasisDataAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<HuobiBasisData>>> GetBasisDataAsync(string contractCode, KlineInterval interval, int limit, string? basisPriceType = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|contractCode|Contract code|
|interval|Kline interval|
|limit|Limit|
|_[Optional]_ basisPriceType|Price type (open, close, high, low, average)|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetBestOfferAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-get-market-bbo-data](https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-get-market-bbo-data)  
<p>

*Get the best current offer*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.ExchangeData.GetBestOfferAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<HuobiSwapBestOffer>>> GetBestOfferAsync(string? contractCode = default, BusinessType? type = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ contractCode|Contract code|
|_[Optional]_ type|Type|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetContractInfoAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-swap-info](https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-swap-info)  
<p>

*Get contract info*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.ExchangeData.GetContractInfoAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<HuobiContractInfo>>> GetContractInfoAsync(string? contractCode = default, MarginMode? supportMarginMode = default, string? symbol = default, ContractType? contractType = default, BusinessType? businessType = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ contractCode|Contract code|
|_[Optional]_ supportMarginMode|Support margin mode|
|_[Optional]_ symbol|Symbol|
|_[Optional]_ contractType|Contract type|
|_[Optional]_ businessType|Business type|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetCrossMarginAdjustFactorInfoAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-query-information-on-tiered-adjustment-factor](https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-query-information-on-tiered-adjustment-factor)  
<p>

*Get cross margin adjust factor info*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.ExchangeData.GetCrossMarginAdjustFactorInfoAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<HuobiCrossSwapAdjustFactorInfo>>> GetCrossMarginAdjustFactorInfoAsync(string? contractCode = default, string? asset = default, ContractType? contractType = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ contractCode|Contract code|
|_[Optional]_ asset|Asset|
|_[Optional]_ contractType|Type|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetCrossMarginTradeStatusAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-query-information-on-trade-state](https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-query-information-on-trade-state)  
<p>

*Get cross margin trade status*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.ExchangeData.GetCrossMarginTradeStatusAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<HuobiCrossMarginTradeStatus>>> GetCrossMarginTradeStatusAsync(string? contractCode = default, string? symbol = default, ContractType? contractType = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ contractCode|Contract code|
|_[Optional]_ symbol|Asset|
|_[Optional]_ contractType|Type|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetCrossMarginTransferStatusAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-query-information-on-transfer-state](https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-query-information-on-transfer-state)  
<p>

*Get cross margin transfer status*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.ExchangeData.GetCrossMarginTransferStatusAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<HuobiCrossMarginTransferStatus>>> GetCrossMarginTransferStatusAsync(string? marginAccount = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ marginAccount|Margin account|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetCrossTieredMarginInfoAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-query-information-on-tiered-margin](https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-query-information-on-tiered-margin)  
<p>

*Get cross tiered margin info*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.ExchangeData.GetCrossTieredMarginInfoAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<HuobiTieredCrossMarginInfo>>> GetCrossTieredMarginInfoAsync(string? contractCode = default, string? symbol = default, ContractType? contractType = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ contractCode|Contract code|
|_[Optional]_ symbol|Symbol|
|_[Optional]_ contractType|Contract type|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetEstimatedFundingRateKlinesAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-estimated-funding-rate-kline-data](https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-estimated-funding-rate-kline-data)  
<p>

*Get estimated funding rate kliens*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.ExchangeData.GetEstimatedFundingRateKlinesAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<HuobiKline>>> GetEstimatedFundingRateKlinesAsync(string contractCode, KlineInterval interval, int limit, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|contractCode|Contract code|
|interval|Kline interval|
|limit|Limit|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetEstimatedSettlementPriceAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-get-the-estimated-settlement-price](https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-get-the-estimated-settlement-price)  
<p>

*Get estimated settlement price*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.ExchangeData.GetEstimatedSettlementPriceAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<HuobiEstimatedSettlementPrice>>> GetEstimatedSettlementPriceAsync(string? contractCode = default, string? symbol = default, ContractType? contractType = default, BusinessType? businessType = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ contractCode|Contract code|
|_[Optional]_ symbol|Symbol|
|_[Optional]_ contractType|Contract type|
|_[Optional]_ businessType|Business type|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetFundingRateAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-funding-rate](https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-funding-rate)  
<p>

*Get funding rate*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.ExchangeData.GetFundingRateAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<HuobiFundingRate>> GetFundingRateAsync(string contractCode, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|contractCode|Contract code|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetFundingRatesAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-a-batch-of-funding-rate](https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-a-batch-of-funding-rate)  
<p>

*Get funding rates*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.ExchangeData.GetFundingRatesAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<HuobiFundingRate>>> GetFundingRatesAsync(string? contractCode = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ contractCode|Contract code|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetHistoricalFundingRatesAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-historical-funding-rate](https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-historical-funding-rate)  
<p>

*Get historical funding rates*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.ExchangeData.GetHistoricalFundingRatesAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<HuobiFundingRatePage>> GetHistoricalFundingRatesAsync(string contractCode, int? page = default, int? pageSize = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|contractCode|Contract code|
|_[Optional]_ page|Page|
|_[Optional]_ pageSize|Page size|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetHistoricalSettlementRecordsAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-historical-settlement-records-of-the-platform-interface](https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-historical-settlement-records-of-the-platform-interface)  
<p>

*Get historical settlement records*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.ExchangeData.GetHistoricalSettlementRecordsAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<HuobiSettlementPage>> GetHistoricalSettlementRecordsAsync(string contractCode, DateTime? startTime = default, DateTime? endTime = default, int? page = default, int? pageSize = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|contractCode|Contract code|
|_[Optional]_ startTime|Filter by start time|
|_[Optional]_ endTime|Filter by end time|
|_[Optional]_ page|Page|
|_[Optional]_ pageSize|Page size|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetInsuranceFundHistoryAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-history-records-of-insurance-fund-balance](https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-history-records-of-insurance-fund-balance)  
<p>

*Get insurance fund history*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.ExchangeData.GetInsuranceFundHistoryAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<HuobiInsuranceInfo>> GetInsuranceFundHistoryAsync(string contractCode, int? page = default, int? pageSize = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|contractCode|Contract code|
|_[Optional]_ page|Page|
|_[Optional]_ pageSize|Page size|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetIsolatedMarginAdjustFactorInfoAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-query-information-on-tiered-adjustment-factor](https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-query-information-on-tiered-adjustment-factor)  
<p>

*Get isolated margin adjust factor info*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.ExchangeData.GetIsolatedMarginAdjustFactorInfoAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<HuobiSwapAdjustFactorInfo>>> GetIsolatedMarginAdjustFactorInfoAsync(string? contractCode = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ contractCode|Contract code|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetIsolatedMarginTieredInfoAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-query-information-on-tiered-margin](https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-query-information-on-tiered-margin)  
<p>

*Get isolated margin tier info*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.ExchangeData.GetIsolatedMarginTieredInfoAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<HuobiTieredMarginInfo>>> GetIsolatedMarginTieredInfoAsync(string? contractCode = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ contractCode|Contract code|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetIsolatedStatusAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-query-information-on-system-status](https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-query-information-on-system-status)  
<p>

*Get isolated margin status*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.ExchangeData.GetIsolatedStatusAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<HuobiContractStatus>>> GetIsolatedStatusAsync(string? contractCode = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ contractCode|Contract code|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetKlinesAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-get-kline-data](https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-get-kline-data)  
<p>

*Get klines*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.ExchangeData.GetKlinesAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<HuobiKline>>> GetKlinesAsync(string contractCode, KlineInterval interval, int? limit = default, DateTime? from = default, DateTime? to = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|contractCode|Contract code|
|interval|Kline interval|
|_[Optional]_ limit|Limit|
|_[Optional]_ from|Filter by start time|
|_[Optional]_ to|Filter by end time|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetLastTradesAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-the-last-trade-of-a-contract](https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-the-last-trade-of-a-contract)  
<p>

*Get last trades*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.ExchangeData.GetLastTradesAsync();  
```  

```csharp  
Task<WebCallResult<HuobiLastTrade>> GetLastTradesAsync(string? contractCode = default, BusinessType? businessType = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ contractCode|Contract code|
|_[Optional]_ businessType|Business type|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetLiquidationOrdersAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-liquidation-orders](https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-liquidation-orders)  
<p>

*Get liquidation orders*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.ExchangeData.GetLiquidationOrdersAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<HuobiLiquidationOrderPage>> GetLiquidationOrdersAsync(int createDate, LiquidationTradeType tradeType, string? contractCode = default, string? symbol = default, int? page = default, int? pageSize = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|createDate|Create date|
|tradeType|Trade type|
|_[Optional]_ contractCode|Contract code|
|_[Optional]_ symbol|Symbol|
|_[Optional]_ page|Page|
|_[Optional]_ pageSize|Page size|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetMarketDataAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-get-market-data-overview](https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-get-market-data-overview)  
<p>

*Get market data*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.ExchangeData.GetMarketDataAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<HuobiMarketData>> GetMarketDataAsync(string contractCode, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|contractCode|Contract code|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetMarketDatasAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-get-a-batch-of-market-data-overview](https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-get-a-batch-of-market-data-overview)  
<p>

*Get market datas*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.ExchangeData.GetMarketDatasAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<HuobiMarketData>>> GetMarketDatasAsync(string? contractCode = default, BusinessType? businessType = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ contractCode|Contract code|
|_[Optional]_ businessType|Business type|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetOpenInterestAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-information-on-open-interest](https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-information-on-open-interest)  
<p>

*Get open interest*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.ExchangeData.GetOpenInterestAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<HuobiOpenInterestValue>> GetOpenInterestAsync(InterestPeriod period, Unit unit, string? contractCode = default, string? symbol = default, ContractType? type = default, int? limit = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|period|Period|
|unit|Unit|
|_[Optional]_ contractCode|Contract code|
|_[Optional]_ symbol|Symbol|
|_[Optional]_ type|Type|
|_[Optional]_ limit|Limit|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetOrderBookAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-get-market-depth](https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-get-market-depth)  
<p>

*Get order book*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.ExchangeData.GetOrderBookAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<HuobiOrderBook>> GetOrderBookAsync(string contractCode, string step, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|contractCode|Contract code|
|step|Merge step|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetPremiumIndexKlinesAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-premium-index-kline-data](https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-premium-index-kline-data)  
<p>

*Get premium index klines*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.ExchangeData.GetPremiumIndexKlinesAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<HuobiKline>>> GetPremiumIndexKlinesAsync(string contractCode, KlineInterval interval, int limit, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|contractCode|Contract code|
|interval|Interval|
|limit|Limit|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetRecentTradesAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-a-batch-of-trade-records-of-a-contract](https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-a-batch-of-trade-records-of-a-contract)  
<p>

*Get recent trades*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.ExchangeData.GetRecentTradesAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<HuobiTrade>>> GetRecentTradesAsync(string contractCode, int limit, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|contractCode|Contract code|
|limit|Limit|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetServerTimeAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#get-current-system-timestamp](https://huobiapi.github.io/docs/usdt_swap/v1/en/#get-current-system-timestamp)  
<p>

*Get server time*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.ExchangeData.GetServerTimeAsync();  
```  

```csharp  
Task<WebCallResult<DateTime>> GetServerTimeAsync(CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetSwapIndexPriceAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-swap-index-price-information](https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-swap-index-price-information)  
<p>

*Get swap index price*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.ExchangeData.GetSwapIndexPriceAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<HuobiSwapIndex>>> GetSwapIndexPriceAsync(string? contractCode = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ contractCode|Contract code|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetSwapOpenInterestAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-get-swap-open-interest-information](https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-get-swap-open-interest-information)  
<p>

*Get swap open interest*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.ExchangeData.GetSwapOpenInterestAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<HuobiOpenInterest>>> GetSwapOpenInterestAsync(string? contractCode = default, string? symbol = default, ContractType? contractType = default, BusinessType? businessType = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ contractCode|Contract code|
|_[Optional]_ symbol|Symbol|
|_[Optional]_ contractType|Contract type|
|_[Optional]_ businessType|Business tpye|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetSwapPriceLimitationAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-swap-price-limitation](https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-swap-price-limitation)  
<p>

*Get swap price limitation*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.ExchangeData.GetSwapPriceLimitationAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<HuobiPriceLimitation>>> GetSwapPriceLimitationAsync(string? contractCode = default, string? symbol = default, ContractType? contractType = default, BusinessType? businessType = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ contractCode|Contract code|
|_[Optional]_ symbol|Symbol|
|_[Optional]_ contractType|Contract tpye|
|_[Optional]_ businessType|Business type|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetSwapRiskInfoAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-information-on-contract-insurance-fund-balance-and-estimated-clawback-rate](https://huobiapi.github.io/docs/usdt_swap/v1/en/#general-query-information-on-contract-insurance-fund-balance-and-estimated-clawback-rate)  
<p>

*Get swap risk info*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.ExchangeData.GetSwapRiskInfoAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<HuobiSwapRiskInfo>>> GetSwapRiskInfoAsync(string? contractCode = default, BusinessType? businessType = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ contractCode|Contract code|
|_[Optional]_ businessType|Business type|
|_[Optional]_ ct|Cancellation token|

</p>
