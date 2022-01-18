There are a decent amount of breaking changes when moving from version 3.x.x to version 4.x.x. Although the interface has changed, the available endpoints/information have not, so there should be no need to completely rewrite your program.
Most endpoints are now available under a slightly different name or path, and most data models have remained the same, barring a few renames.
In this document most changes will be described. If you have any other questions or issues when updating, feel free to open an issue.

Changes related to `IExchangeClient`, options and client structure are also (partially) covered in the [CryptoExchange.Net Migration Guide](https://github.com/JKorf/CryptoExchange.Net/wiki/Migration-Guide)

### Namespaces
There are a few namespace changes:  
|Type|Old|New|
|----|---|---|
|Enums|`Huobi.Net.Objects`|`Huobi.Net.Enums`  |
|Clients|`Huobi.Net`|`Huobi.Net.Clients`  |
|Client interfaces|`Huobi.Net.Interfaces`|`Huobi.Net.Interfaces.Clients`  |
|Objects|`Huobi.Net.Objects`|`Huobi.Net.Objects.Models`  |
|SymbolOrderBook|`Huobi.Net`|`Huobi.Net.SymbolOrderBooks`|

### Client options
The `BaseAddress` and rate limiting options are now under the `SpotApiOptions`.  
*V3*
````C#
var huobiClient = new HuobiClient(new HuobiClientOptions
{
	ApiCredentials = new ApiCredentials("API-KEY", "API-SECRET"),
	BaseAddress = "ADDRESS",
	RateLimitingBehaviour = RateLimitingBehaviour.Fail
});
````

*V4*
````C#
var huobiClient = new HuobiClient(new HuobiClientOptions
{
	ApiCredentials = new ApiCredentials("API-KEY", "API-SECRET"),
	SpotApiOptions = new RestApiClientOptions
	{
		BaseAddress = "ADDRESS",
		RateLimitingBehaviour = RateLimitingBehaviour.Fail
	}
});
````

### Client structure
Version 4 adds the `SpotApi` Api client under the `HuobiClient`, and a topic underneath that. This is done to keep the same client structure as other exchange implementations, more info on this [here](https://github.com/Jkorf/CryptoExchange.Net/wiki/Clients).
In the HuobiSocketClient a `SpotStreams` Api client is added. This means all calls will have changed, though most will only need to add `SpotApi.[Topic].XXX`/`SpotStreams.XXX`:

*V3*
````C#
var balances = await huobiClient.GetBalancesAsync();
var withdrawals = await huobiClient.GetWithdrawDepositAsync();

var tickers = await huobiClient.GetTickersAsync();
var symbols = await huobiClient.GetSymbolsAsync();

var order = await huobiClient.PlaceOrderAsync();
var trades = await huobiClient.GetUserTradesAsync();

var sub = huobiSocketClient.SubscribeToTickerUpdatesAsync(DataHandler);
````

*V4*  
````C#
var balances = await huobiClient.SpotApi.Account.GetBalancesAsync();
var withdrawals = await huobiClient.SpotApi.Account.GetWithdrawDepositAsync();

var tickers = await huobiClient.SpotApi.ExchangeData.GetTickersAsync();
var symbols = await huobiClient.SpotApi.ExchangeData.GetSymbolsAsync();

var order = await huobiClient.SpotApi.Trading.PlaceOrderAsync();
var trades = await huobiClient.SpotApi.Trading.GetUserTradesAsync();

var sub = huobiSocketClient.SpotStreams.SubscribeToTickerUpdatesAsync(DataHandler);
````

### Definitions
Some names have been changed to a common definition. This includes where the name is part of a bigger name  
|Old|New||
|----|---|---|
|`Currency`|`Asset`|`GetCurrenciesAndChainsAsync()` -> `GetAssetDetailsAsync()`|
|`Size`|`Quantity`||
|`Id/Open/High/Low/Close`|`OpenTime/OpenPrice/HighPrice/LowPrice/ClosePrice`||
|`Ask`/`AskSize`/`Bid`/`BidSize`|`BestAskPrice`/`BestAskQuantity`/`BestBidPrice`/`BestBidQuantity`||
|`HuobiPeriod`|`KlineInterval`||
|`Chains`|`Networks`||

Some names have slightly changed to be consistent across different libraries   
`FilledQuantity`/`ExecutedQuantity` -> `QuantityFilled`  

### Enum names
`Huobi` prefixes have been removed  
*V3*  
`HuobiAccountState, HuobiOrderSide`, etc  

*V4*  
`AccountState, OrderSide`

### HuobiSymbolOrderBook
The `HuobiSymbolOrderBook` has been renamed to `HuobiSpotSymbolOrderBook`.

### Changed methods
The `PlaceOrderAsync` and order objects used to have a combined property for the type of the order (limit, market etc) and the side (buy, sell). This has been split up for consistency with the other `CryptoExchange.Net` implementations:  
*V3*
````C#
Task<WebCallResult<long>> PlaceOrderAsync(
	long accountId, 
	string symbol, 
	HuobiOrderType orderType, 
	decimal quantity, 
	decimal? price = null, 
	string? clientOrderId = null, 
	SourceType? source = null, 
	decimal? stopPrice = null, 
	Operator? stopOperator = null, 
	CancellationToken ct = default);
````
*V4*
````C#
Task<WebCallResult<long>> PlaceOrderAsync(
	long accountId, 
	string symbol, 
	OrderSide side, 
	OrderType type, 
	decimal quantity, 
	decimal? price = null, 
	string? clientOrderId = null, 
	SourceType? source = null, 
	decimal? stopPrice = null, 
	Operator? stopOperator = null, 
	CancellationToken ct = default);
````


