---
title: IHuobiClientSpotApiTrading
has_children: false
parent: IHuobiClientSpotApi
grand_parent: Rest API documentation
---
*[generated documentation]*  
`HuobiClient > SpotApi > Trading`  
*Huobi trading endpoints, placing and mananging orders.*
  

***

## CancelOrderAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#submit-cancel-for-an-order](https://huobiapi.github.io/docs/spot/v1/en/#submit-cancel-for-an-order)  
<p>

*Cancels an open order*  

```csharp  
var client = new HuobiClient();  
var result = await client.SpotApi.Trading.CancelOrderAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<long>> CancelOrderAsync(long orderId, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|orderId|The id of the order to cancel|
|_[Optional]_ ct|Cancellation token|

</p>

***

## CancelOrderByClientOrderIdAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#submit-cancel-for-an-order-based-on-client-order-id](https://huobiapi.github.io/docs/spot/v1/en/#submit-cancel-for-an-order-based-on-client-order-id)  
<p>

*Cancels an open order*  

```csharp  
var client = new HuobiClient();  
var result = await client.SpotApi.Trading.CancelOrderByClientOrderIdAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<long>> CancelOrderByClientOrderIdAsync(string clientOrderId, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|clientOrderId|The client id of the order to cancel|
|_[Optional]_ ct|Cancellation token|

</p>

***

## CancelOrdersAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#submit-cancel-for-multiple-orders-by-ids](https://huobiapi.github.io/docs/spot/v1/en/#submit-cancel-for-multiple-orders-by-ids)  
<p>

*Cancel multiple open orders*  

```csharp  
var client = new HuobiClient();  
var result = await client.SpotApi.Trading.CancelOrdersAsync();  
```  

```csharp  
Task<WebCallResult<HuobiBatchCancelResult>> CancelOrdersAsync(IEnumerable<long>? orderIds = default, IEnumerable<string>? clientOrderIds = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ orderIds|The ids of the orders to cancel|
|_[Optional]_ clientOrderIds|The client ids of the orders to cancel|
|_[Optional]_ ct|Cancellation token|

</p>

***

## CancelOrdersByCriteriaAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#submit-cancel-for-multiple-orders-by-criteria](https://huobiapi.github.io/docs/spot/v1/en/#submit-cancel-for-multiple-orders-by-criteria)  
<p>

*Cancel multiple open orders*  

```csharp  
var client = new HuobiClient();  
var result = await client.SpotApi.Trading.CancelOrdersByCriteriaAsync();  
```  

```csharp  
Task<WebCallResult<HuobiByCriteriaCancelResult>> CancelOrdersByCriteriaAsync(long? accountId = default, IEnumerable<string>? symbols = default, OrderSide? side = default, int? limit = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ accountId|The account id used for this cancel|
|_[Optional]_ symbols|The trading symbol list (maximum 10 symbols, default value all symbols)|
|_[Optional]_ side|Filter on the direction of the trade|
|_[Optional]_ limit|The number of orders to cancel [1, 100]|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetClosedOrdersAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#search-past-orders](https://huobiapi.github.io/docs/spot/v1/en/#search-past-orders)  
<p>

*Gets a list of orders*  

```csharp  
var client = new HuobiClient();  
var result = await client.SpotApi.Trading.GetClosedOrdersAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<HuobiOrder>>> GetClosedOrdersAsync(string symbol, IEnumerable<OrderState>? states = default, IEnumerable<OrderType>? types = default, DateTime? startTime = default, DateTime? endTime = default, long? fromId = default, FilterDirection? direction = default, int? limit = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|symbol|The symbol to get orders for|
|_[Optional]_ states|The states of orders to return|
|_[Optional]_ types|The types of orders to return|
|_[Optional]_ startTime|Only get orders after this date|
|_[Optional]_ endTime|Only get orders before this date|
|_[Optional]_ fromId|Only get orders with ID before or after this. Used together with the direction parameter|
|_[Optional]_ direction|Direction of the results to return when using the fromId parameter|
|_[Optional]_ limit|The max number of results|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetHistoricalOrdersAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#search-historical-orders-within-48-hours](https://huobiapi.github.io/docs/spot/v1/en/#search-historical-orders-within-48-hours)  
<p>

*Gets a list of history orders*  

```csharp  
var client = new HuobiClient();  
var result = await client.SpotApi.Trading.GetHistoricalOrdersAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<HuobiOrder>>> GetHistoricalOrdersAsync(string? symbol = default, DateTime? startTime = default, DateTime? endTime = default, FilterDirection? direction = default, int? limit = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ symbol|The symbol to get orders for|
|_[Optional]_ startTime|Only get orders after this date|
|_[Optional]_ endTime|Only get orders before this date|
|_[Optional]_ direction|Direction of the results to return|
|_[Optional]_ limit|The max number of results|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetOpenOrdersAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#get-all-open-orders](https://huobiapi.github.io/docs/spot/v1/en/#get-all-open-orders)  
<p>

*Gets a list of open orders*  

```csharp  
var client = new HuobiClient();  
var result = await client.SpotApi.Trading.GetOpenOrdersAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<HuobiOpenOrder>>> GetOpenOrdersAsync(long? accountId = default, string? symbol = default, OrderSide? side = default, int? limit = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ accountId|The account id for which to get the orders for|
|_[Optional]_ symbol|The symbol for which to get the orders for|
|_[Optional]_ side|Only get buy or sell orders|
|_[Optional]_ limit|The max number of results|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetOrderAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#get-the-order-detail-of-an-order](https://huobiapi.github.io/docs/spot/v1/en/#get-the-order-detail-of-an-order)  
<p>

*Get details of an order*  

```csharp  
var client = new HuobiClient();  
var result = await client.SpotApi.Trading.GetOrderAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<HuobiOrder>> GetOrderAsync(long orderId, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|orderId|The id of the order to retrieve|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetOrderByClientOrderIdAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#get-the-order-detail-of-an-order-based-on-client-order-id](https://huobiapi.github.io/docs/spot/v1/en/#get-the-order-detail-of-an-order-based-on-client-order-id)  
<p>

*Get details of an order by client order id*  

```csharp  
var client = new HuobiClient();  
var result = await client.SpotApi.Trading.GetOrderByClientOrderIdAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<HuobiOrder>> GetOrderByClientOrderIdAsync(string clientOrderId, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|clientOrderId|The client id of the order to retrieve|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetOrderTradesAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#get-the-match-result-of-an-order](https://huobiapi.github.io/docs/spot/v1/en/#get-the-match-result-of-an-order)  
<p>

*Gets a list of trades made for a specific order*  

```csharp  
var client = new HuobiClient();  
var result = await client.SpotApi.Trading.GetOrderTradesAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<IEnumerable<HuobiOrderTrade>>> GetOrderTradesAsync(long orderId, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|orderId|The id of the order to get trades for|
|_[Optional]_ ct|Cancellation token|

</p>

***

## GetUserTradesAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#search-match-results](https://huobiapi.github.io/docs/spot/v1/en/#search-match-results)  
<p>

*Gets a list of trades for a specific symbol*  

```csharp  
var client = new HuobiClient();  
var result = await client.SpotApi.Trading.GetUserTradesAsync();  
```  

```csharp  
Task<WebCallResult<IEnumerable<HuobiOrderTrade>>> GetUserTradesAsync(IEnumerable<OrderState>? states = default, string? symbol = default, IEnumerable<OrderType>? types = default, DateTime? startTime = default, DateTime? endTime = default, long? fromId = default, FilterDirection? direction = default, int? limit = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|_[Optional]_ states|Only return trades with specific states|
|_[Optional]_ symbol|The symbol to retrieve trades for|
|_[Optional]_ types|The type of orders to return|
|_[Optional]_ startTime|Only get orders after this date|
|_[Optional]_ endTime|Only get orders before this date|
|_[Optional]_ fromId|Only get orders with ID before or after this. Used together with the direction parameter|
|_[Optional]_ direction|Direction of the results to return when using the fromId parameter|
|_[Optional]_ limit|The max number of results|
|_[Optional]_ ct|Cancellation token|

</p>

***

## PlaceOrderAsync  

[https://huobiapi.github.io/docs/spot/v1/en/#place-a-new-order](https://huobiapi.github.io/docs/spot/v1/en/#place-a-new-order)  
<p>

*Places an order*  

```csharp  
var client = new HuobiClient();  
var result = await client.SpotApi.Trading.PlaceOrderAsync(/* parameters */);  
```  

```csharp  
Task<WebCallResult<long>> PlaceOrderAsync(long accountId, string symbol, OrderSide side, OrderType type, decimal quantity, decimal? price = default, string? clientOrderId = default, SourceType? source = default, decimal? stopPrice = default, Operator? stopOperator = default, CancellationToken ct = default);  
```  

|Parameter|Description|
|---|---|
|accountId|The account to place the order for|
|symbol|The symbol to place the order for|
|side|The side of the order|
|type|The type of the order|
|quantity|The quantity of the order|
|_[Optional]_ price|The price of the order. Should be omitted for market orders|
|_[Optional]_ clientOrderId|The clientOrderId the order should get|
|_[Optional]_ source|Source. defaults to SpotAPI|
|_[Optional]_ stopPrice|Stop price|
|_[Optional]_ stopOperator|Operator of the stop price|
|_[Optional]_ ct|Cancellation token|

</p>
