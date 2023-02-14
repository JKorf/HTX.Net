---
title: IHuobiClientUsdtMarginSwapApiTrading
has_children: false
parent: IHuobiClientUsdtMarginSwapApi
grand_parent: Rest API documentation
---
*[generated documentation]*  
`HuobiClient > UsdtMarginSwapApi > Trading`  
*Huobi usdt margin swap trading endpoints, placing and mananging orders.*
  

***

## CancelAllCrossMarginOrdersAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-cancel-all-orders](https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-cancel-all-orders)  
<p>

*Cancel all cross margin orders fitting the parameters*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.Trading.CancelAllCrossMarginOrdersAsync();  
```  

```csharp  
Task<WebCallResult<HuobiBatchResult>> CancelAllCrossMarginOrdersAsync(string? contractCode = default, string? symbol = default, ContractType? contractType = default, OrderSide? side = default, Offset? offset = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ contractCode|Contract code|
|_[Optional]_ symbol|Symbol|
|_[Optional]_ contractType|Contract type|
|_[Optional]_ side|Side|
|_[Optional]_ offset|Offset|
|_[Optional]_ ct|Cancellation token|

</p>

***

## CancelAllIsolatedMarginOrdersAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-cancel-all-orders](https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-cancel-all-orders)  
<p>

*Cancel all isolated margin order fitting the parameters*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.Trading.CancelAllIsolatedMarginOrdersAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<HuobiBatchResult>> CancelAllIsolatedMarginOrdersAsync(string contractCode, OrderSide? side = default, Offset? offset = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|contractCode|Contract code|
|_[Optional]_ side|Side|
|_[Optional]_ offset|Offset|
|_[Optional]_ ct|Cancellation token|

</p>

***

## CancelCrossMarginOrderAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-cancel-all-orders](https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-cancel-all-orders)  
<p>

*Cancel cross margin order*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.Trading.CancelCrossMarginOrderAsync();  
```  

```csharp  
Task<WebCallResult<HuobiBatchResult>> CancelCrossMarginOrderAsync(long? orderId = default, long? clientOrderId = default, string? contractCode = default, string? symbol = default, ContractType? contractType = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ orderId|The order id|
|_[Optional]_ clientOrderId|The client order id|
|_[Optional]_ contractCode|Contract code|
|_[Optional]_ symbol|Symbol|
|_[Optional]_ contractType|Contract type|
|_[Optional]_ ct|Cancellation token|

</p>

***

## CancelCrossMarginOrdersAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-cancel-all-orders](https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-cancel-all-orders)  
<p>

*Cancel cross margin orders*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.Trading.CancelCrossMarginOrdersAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<HuobiBatchResult>> CancelCrossMarginOrdersAsync(IEnumerable<long> orderIds, IEnumerable<long> clientOrderIds, string? contractCode = default, string? symbol = default, ContractType? contractType = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|orderIds|Order ids|
|clientOrderIds|Client order ids|
|_[Optional]_ contractCode|Contract code|
|_[Optional]_ symbol|Symbol|
|_[Optional]_ contractType|Contract type|
|_[Optional]_ ct|Cancellation token|

</p>

***

## CancelIsolatedMarginOrderAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-cancel-an-order](https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-cancel-an-order)  
<p>

*Cancel isolated margin order*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.Trading.CancelIsolatedMarginOrderAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<HuobiBatchResult>> CancelIsolatedMarginOrderAsync(string contractCode, long? orderId = default, long? clientOrderId = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|contractCode|Contract code|
|_[Optional]_ orderId|Order id|
|_[Optional]_ clientOrderId|Client order id|
|_[Optional]_ ct|Cancellation token|

</p>

***

## CancelIsolatedMarginOrdersAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-cancel-an-order](https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-cancel-an-order)  
<p>

*Cancel isolated margin orders*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.Trading.CancelIsolatedMarginOrdersAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<HuobiBatchResult>> CancelIsolatedMarginOrdersAsync(string contractCode, IEnumerable<long> orderId, IEnumerable<long> clientOrderId, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|contractCode|Contract  code|
|orderId|Order ids|
|clientOrderId|Client order ids|
|_[Optional]_ ct|Cancellation token|

</p>

***

## ChangeCrossMarginLeverageAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-switch-leverage](https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-switch-leverage)  
<p>

*Change cross margin leverage*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.Trading.ChangeCrossMarginLeverageAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<HuobiCrossMarginLeverageRate>> ChangeCrossMarginLeverageAsync(int leverageRate, string? contractCode = default, string? symbol = default, ContractType? contractType = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|leverageRate|Leverage rate|
|_[Optional]_ contractCode|Contract code|
|_[Optional]_ symbol|Symbol|
|_[Optional]_ contractType|Contract type|
|_[Optional]_ ct|Cancellation token|

</p>

***

## ChangeIsolatedMarginLeverageAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-switch-leverage](https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-switch-leverage)  
<p>

*Change isolated margin leverage*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.Trading.ChangeIsolatedMarginLeverageAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<HuobiIsolatedMarginLeverageRate>> ChangeIsolatedMarginLeverageAsync(string contractCode, int leverageRate, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|contractCode|Contract code|
|leverageRate|Leverage rate|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetCrossMarginClosedOrdersAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-get-history-orders-new](https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-get-history-orders-new)  
<p>

*Get cross margin closed orders*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.Trading.GetCrossMarginClosedOrdersAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<HuobiCrossMarginOrderPage>> GetCrossMarginClosedOrdersAsync(MarginTradeType tradeType, bool allOrders, int daysInHistory, string? contractCode = default, string? symbol = default, int? page = default, int? pageSize = default, string? sortBy = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|tradeType|Trade type|
|allOrders|All orders|
|daysInHistory|Days in history|
|_[Optional]_ contractCode|Contract code|
|_[Optional]_ symbol|Symbol|
|_[Optional]_ page|Page|
|_[Optional]_ pageSize|Page size|
|_[Optional]_ sortBy|Sort by|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetCrossMarginOpenOrdersAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-current-unfilled-order-acquisition](https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-current-unfilled-order-acquisition)  
<p>

*Get cross margin open orders*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.Trading.GetCrossMarginOpenOrdersAsync();  
```  

```csharp  
Task<WebCallResult<HuobiCrossMarginOrderPage>> GetCrossMarginOpenOrdersAsync(string? contractCode = default, string? symbol = default, int? page = default, int? pageSize = default, string? sortBy = default, MarginTradeType? tradeType = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ contractCode|Contract code|
|_[Optional]_ symbol|Symbol|
|_[Optional]_ page|Page|
|_[Optional]_ pageSize|Page size|
|_[Optional]_ sortBy|Sort by|
|_[Optional]_ tradeType|Trade type|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetCrossMarginOrderAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-get-information-of-order](https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-get-information-of-order)  
<p>

*Get cross margin order*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.Trading.GetCrossMarginOrderAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<HuobiCrossMarginOrder>>> GetCrossMarginOrderAsync(string? contractCode = default, string? symbol = default, long? orderId = default, long? clientOrderId = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ contractCode|Contract code|
|_[Optional]_ symbol|Symbol|
|_[Optional]_ orderId|Order id|
|_[Optional]_ clientOrderId|Client order id|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetCrossMarginOrderDetailsAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-get-detail-information-of-order](https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-get-detail-information-of-order)  
<p>

*Get cross margin order details*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.Trading.GetCrossMarginOrderDetailsAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<HuobiMarginOrderDetails>> GetCrossMarginOrderDetailsAsync(string contractCode, long orderId, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|contractCode|Contract code|
|orderId|Order id|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetCrossMarginOrdersAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-get-information-of-order](https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-get-information-of-order)  
<p>

*Get cross margin orders*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.Trading.GetCrossMarginOrdersAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<HuobiCrossMarginOrder>>> GetCrossMarginOrdersAsync(IEnumerable<long> orderIds, IEnumerable<long> clientOrderIds, string? contractCode = default, string? symbol = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|orderIds|Order ids|
|clientOrderIds|Client order ids|
|_[Optional]_ contractCode|Contract code|
|_[Optional]_ symbol|Symbol|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetCrossMarginUserTradesAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-get-history-match-results](https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-get-history-match-results)  
<p>

*Get cross margin user trades*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.Trading.GetCrossMarginUserTradesAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<HuobiCrossMarginUserTradePage>> GetCrossMarginUserTradesAsync(string contractCode, MarginTradeType tradeType, int daysInHistory, int? page = default, int? pageSize = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|contractCode|Contract code|
|tradeType|Trade type|
|daysInHistory|Days in history|
|_[Optional]_ page|Page|
|_[Optional]_ pageSize|Page size|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetIsolatedMarginClosedOrdersAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-get-history-orders](https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-get-history-orders)  
<p>

*Get isolated margin closed orders*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.Trading.GetIsolatedMarginClosedOrdersAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<HuobiIsolatedMarginOrderPage>> GetIsolatedMarginClosedOrdersAsync(string contractCode, MarginTradeType tradeType, bool allOrders, int daysInHistory, int? page = default, int? pageSize = default, string? sortBy = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|contractCode|Contract code|
|tradeType|Trade type|
|allOrders|All orders|
|daysInHistory|Days in history|
|_[Optional]_ page|Page|
|_[Optional]_ pageSize|Page size|
|_[Optional]_ sortBy|Sort by|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetIsolatedMarginOpenOrdersAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-current-unfilled-order-acquisition](https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-current-unfilled-order-acquisition)  
<p>

*Get isolated margin open orders*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.Trading.GetIsolatedMarginOpenOrdersAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<HuobiIsolatedMarginOrderPage>> GetIsolatedMarginOpenOrdersAsync(string contractCode, int? page = default, int? pageSize = default, string? sortBy = default, MarginTradeType? tradeType = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|contractCode|Contract code|
|_[Optional]_ page|Page|
|_[Optional]_ pageSize|Page size|
|_[Optional]_ sortBy|Sort by|
|_[Optional]_ tradeType|Trade type|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetIsolatedMarginOrderAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-get-information-of-an-order](https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-get-information-of-an-order)  
<p>

*Get isoalted margin order*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.Trading.GetIsolatedMarginOrderAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<HuobiIsolatedMarginOrder>>> GetIsolatedMarginOrderAsync(string contractCode, long? orderId = default, long? clientOrderId = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|contractCode|Contract code|
|_[Optional]_ orderId|Order id|
|_[Optional]_ clientOrderId|Client order id|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetIsolatedMarginOrderDetailsAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-order-details-acquisition](https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-order-details-acquisition)  
<p>

*Get isolated margin order details*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.Trading.GetIsolatedMarginOrderDetailsAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<HuobiMarginOrderDetails>> GetIsolatedMarginOrderDetailsAsync(string contractCode, long orderId, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|contractCode|Contract code|
|orderId|Order id|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetIsolatedMarginOrdersAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-get-information-of-an-order](https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-get-information-of-an-order)  
<p>

*Get isolated margin orders*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.Trading.GetIsolatedMarginOrdersAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<HuobiIsolatedMarginOrder>>> GetIsolatedMarginOrdersAsync(string contractCode, IEnumerable<long> orderIds, IEnumerable<long> clientOrderIds, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|contractCode|Contract code|
|orderIds|Order ids|
|clientOrderIds|Client order ids|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetIsolatedMarginUserTradesAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-acquire-history-match-results-new](https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-acquire-history-match-results-new)  
<p>

*Get isolated margin user trades*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.Trading.GetIsolatedMarginUserTradesAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<HuobiIsolatedMarginUserTradePage>> GetIsolatedMarginUserTradesAsync(string contractCode, MarginTradeType tradeType, int daysInHistory, int? page = default, int? pageSize = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|contractCode|Contract code|
|tradeType|Trade type|
|daysInHistory|Days in history|
|_[Optional]_ page|Page|
|_[Optional]_ pageSize|Page size|
|_[Optional]_ ct|Cancellation token|

</p>

***

## PlaceCrossMarginOrderAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-place-an-order](https://huobiapi.github.io/docs/usdt_swap/v1/en/#cross-place-an-order)  
<p>

*Place a new cross margin order*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.Trading.PlaceCrossMarginOrderAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<HuobiPlacedOrderId>> PlaceCrossMarginOrderAsync(decimal quantity, OrderSide side, int leverageRate, string? contractCode = default, string? symbol = default, ContractType? contractType = default, decimal? price = default, Offset? offset = default, OrderPriceType? orderPriceType = default, decimal? takeProfitTriggerPrice = default, decimal? takeProfitOrderPrice = default, OrderPriceType? takeProfitOrderPriceType = default, decimal? stopLossTriggerPrice = default, decimal? stopLossOrderPrice = default, OrderPriceType? stopLossOrderPriceType = default, bool? reduceOnly = default, long? clientOrderId = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|quantity|Order quantity|
|side|Order side|
|leverageRate|Leverage rate|
|_[Optional]_ contractCode|Contract code|
|_[Optional]_ symbol|Symbol|
|_[Optional]_ contractType|Contract type|
|_[Optional]_ price|Price|
|_[Optional]_ offset|Offset|
|_[Optional]_ orderPriceType|Order price type|
|_[Optional]_ takeProfitTriggerPrice|Take profit trigger price|
|_[Optional]_ takeProfitOrderPrice|Take profit order price|
|_[Optional]_ takeProfitOrderPriceType|Take profit order price type|
|_[Optional]_ stopLossTriggerPrice|Stop loss trigger price|
|_[Optional]_ stopLossOrderPrice|Stop loss order price|
|_[Optional]_ stopLossOrderPriceType|Stop loss order price type|
|_[Optional]_ reduceOnly|Reduce only|
|_[Optional]_ clientOrderId|Client order id|
|_[Optional]_ ct|Cancellation token|

</p>

***

## PlaceIsolatedMarginOrderAsync  

[https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-place-an-order](https://huobiapi.github.io/docs/usdt_swap/v1/en/#isolated-place-an-order)  
<p>

*Place a new isolated margin order*  

```csharp  
var client = new HuobiClient();  
var result = await client.UsdtMarginSwapApi.Trading.PlaceIsolatedMarginOrderAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<HuobiPlacedOrderId>> PlaceIsolatedMarginOrderAsync(string contractCode, decimal quantity, OrderSide side, int leverageRate, decimal? price = default, Offset? offset = default, OrderPriceType? orderPriceType = default, decimal? takeProfitTriggerPrice = default, decimal? takeProfitOrderPrice = default, OrderPriceType? takeProfitOrderPriceType = default, decimal? stopLossTriggerPrice = default, decimal? stopLossOrderPrice = default, OrderPriceType? stopLossOrderPriceType = default, bool? reduceOnly = default, long? clientOrderId = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|contractCode|Contract code|
|quantity|Quantity|
|side|Order side|
|leverageRate|Leverage rate|
|_[Optional]_ price|Price|
|_[Optional]_ offset|Offset|
|_[Optional]_ orderPriceType|Order price type|
|_[Optional]_ takeProfitTriggerPrice|Take profit trigger price|
|_[Optional]_ takeProfitOrderPrice|Take profit order price|
|_[Optional]_ takeProfitOrderPriceType|Take profit order price type|
|_[Optional]_ stopLossTriggerPrice|Stop loss trigger price|
|_[Optional]_ stopLossOrderPrice|Stop loss order price|
|_[Optional]_ stopLossOrderPriceType|Stop loss order price type|
|_[Optional]_ reduceOnly|Reduce only|
|_[Optional]_ clientOrderId|Client order id|
|_[Optional]_ ct|Cancellation token|

</p>
