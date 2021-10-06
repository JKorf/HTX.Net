# Huobi.Net
![Build status](https://travis-ci.com/JKorf/Huobi.Net.svg?branch=master) ![Nuget version](https://img.shields.io/nuget/v/Huobi.net.svg)  ![Nuget downloads](https://img.shields.io/nuget/dt/Huobi.Net.svg)

Huobi.Net is a wrapper around the Huobi API as described on [Huobi](https://github.com/huobiapi), including all features the API provides using clear and readable objects, both for the REST  as the websocket API's.

**If you think something is broken, something is missing or have any questions, please open an [Issue](https://github.com/JKorf/Huobi .Net/issues)**

## CryptoExchange.Net
This library is build upon the CryptoExchange.Net library, make sure to check out the documentation on that for basic usage: [docs](https://github.com/JKorf/CryptoExchange.Net)

## Donations
I develop and maintain this package on my own for free in my spare time. Donations are greatly appreciated. If you prefer to donate any other currency please contact me.

**Btc**:  12KwZk3r2Y3JZ2uMULcjqqBvXmpDwjhhQS  
**Eth**:  0x069176ca1a4b1d6e0b7901a6bc0dbf3bb0bf5cc2  
**Nano**: xrb_1ocs3hbp561ef76eoctjwg85w5ugr8wgimkj8mfhoyqbx4s1pbc74zggw7gs  

## Discord
A Discord server is available [here](https://discord.gg/MSpeEtSY8t). Feel free to join for discussion and/or questions around the CryptoExchange.Net and implementation libraries.

## Getting started
Make sure you have installed the Huobi .Net [Nuget](https://www.nuget.org/packages/Huobi.Net/) package and add `using Huobi.Net` to your usings.  You now have access to 2 clients:  
**HuobiClient**  
The client to interact with the Huobi REST API. Getting prices:
````C#
var client = new HuobiClient(new HuobiClientOptions(){
 // Specify options for the client
});
var callResult = await client.GetTickersAsync();
// Make sure to check if the call was successful
if(!callResult.Success)
{
  // Call failed, check callResult.Error for more info
}
else
{
  // Call succeeded, callResult.Data will have the resulting data
}
````

Placing an order:
````C#
var client = new HuobiClient(new HuobiClientOptions(){
 // Specify options for the client
 ApiCredentials = new ApiCredentials("Key", "Secret")
});
var accountResult = await huobiClient.GetAccountsAsync();
if(!accountResult .Success)
{
  // Call failed, check accountResult .Error for more info
}
var callResult = await client.PlaceOrderAsync(accountResult.Data.First().Id, "BTCUSDT", OrderType.Limit, 10, 50);
// Make sure to check if the call was successful
if(!callResult.Success)
{
  // Call failed, check callResult.Error for more info
}
else
{
  // Call succeeded, callResult.Data will have the resulting data
}
````

**HuobiSocketClient**  
The client to interact with the Huobi websocket API. Basic usage:
````C#
var client = new HuobiSocketClient(new HuobiSocketClientOptions()
{
  // Specify options for the client
});
var subscribeResult = client.SubscribeToSymbolTickerUpdatesAsync("ETHBTC", data => {
  // Handle data when it is received
});
// Make sure to check if the subscritpion was successful
if(!subscribeResult.Success)
{
  // Subscription failed, check callResult.Error for more info
}
else
{
  // Subscription succeeded, the handler will start receiving data when it is available
}
````

## Client options
For the basic client options see also the CryptoExchange.Net [docs](https://github.com/JKorf/CryptoExchange.Net#client-options). The here listed options are the options specific for Huobi.Net.  
**HuobiClientOptions**  
| Property | Description | Default |
| ----------- | ----------- | ---------|
|`SignPublicRequests`|Whether or not public requests should be signed with the API credentials if provided. Needed for accurate rate limiting behavior|`false`

**HuobiSocketClientOptions**  
| Property | Description | Default |
| ----------- | ----------- | ---------|
|`BaseAddressAuthenticated`|The base address for authenticated subscriptions|`wss://api.huobi.pro/ws/v2`

## Release notes
* Version 3.2.2 - 06 Oct 2021
    * Updated CryptoExchange.Net, fixing socket issue when calling from .Net Framework
    * Fixed issue in HuobiSymbolOrderBook syncing

* Version 3.2.1 - 05 Oct 2021
    * Fixed incorrect sanity check in SubscribeToOrderUpdatesAsync

* Version 3.2.0 - 29 Sep 2021
    * Added missing AccountTypes
    * Renamed Amount to Quantity in parameters and properties
    * Implemented Market-By-Price streams
    * Updated CryptoExchange.Net

* Version 3.1.0 - 20 Sep 2021
    * Added missing SetApiCredentials method
    * Updated CryptoExchange.Net

* Version 3.0.5 - 15 Sep 2021
    * Updated CryptoExchange.Net

* Version 3.0.4 - 02 Sep 2021
    * Fix for disposing order book closing socket even if there are other connections

* Version 3.0.3 - 26 Aug 2021
    * Updated CryptoExchange.Net

* Version 3.0.2 - 24 Aug 2021
    * Updated CryptoExchange.Net, improving websocket and SymbolOrderBook performance
    * Fix for 15minute klines via IExchangeClient

* Version 3.0.1 - 13 Aug 2021
    * Fix for OperationCancelledException being thrown when closing a socket from a .net framework project

* Version 3.0.0 - 12 Aug 2021
	* Release version with new CryptoExchange.Net version 4.0.0
		* Multiple changes regarding logging and socket connection, see [CryptoExchange.Net release notes](https://github.com/JKorf/CryptoExchange.Net#release-notes)
		
* Version 3.0.0-beta3 - 09 Aug 2021
    * Renamed GetOrderInfoAsync to GetOrderAsync
    * Renamed GetOrderInfoByClientOrderIdAsync to GetOrderByClientOrderIdAsync
    * Renamed GetSymbolTradesAsync to GetUserTradeHistoryAsync
    * Renamed PlaceWithdrawAsync to WithdrawAsync
    * Renamed GetTradesAsync to GetTradeHistoryAsync

* Version 3.0.0-beta2 - 26 Jul 2021
    * Updated CryptoExchange.Net

* Version 3.0.0-beta1 - 09 Jul 2021
    * Added Async postfix for async methods
    * Updated CryptoExchange.Net

* Version 2.5.5 - 28 apr 2021
    * Updated CryptoExchange.Net

* Version 2.5.4 - 19 apr 2021
    * Updated CryptoExchange.Net

* Version 2.5.3 - 08 apr 2021
    * Added missing withdraw methods to client interface

* Version 2.5.2 - 30 mrt 2021
    * Updated CryptoExchange.Net
    * Added missing methods in client interface

* Version 2.5.1 - 01 mrt 2021
    * Added Nuget SymbolPackage

* Version 2.5.0 - 01 mrt 2021
    * Added config for deterministic build
    * Updated CryptoExchange.Net

* Version 2.4.0 - 22 jan 2021
    * Added GetWithdrawDeposit endpoint
    * Fixed ClientOrderId parsing
    * Updated for ICommonKline

* Version 2.3.3 - 15 jan 2021
    * Updated PlaceOrder paramters

* Version 2.3.2 - 14 jan 2021
    * Updated CryptoExchange.Net

* Version 2.3.1 - 05 jan 2021
    * Fixed missing symbol property on socket ticker

* Version 2.3.0 - 05 jan 2021
    * Added GetDepositAddress endpoint
    * Added Withdraw endpoint
    * Fix ClientOrderId deserialization on order
    * Added NextTime property for pagination on GetHistoryOrders
    * Updated HuobiOrderTrade model
    * Fixed ticker models

* Version 2.2.0 - 21 dec 2020
    * Update CryptoExchange.Net
    * Updated to latest IExchangeClient

* Version 2.1.1 - 11 dec 2020
    * Updated CryptoExchange.Net
    * Implemented IExchangeClient

* Version 2.1.0 - 25 nov 2020
    * Updated account socket subscriptions to V2 API

* Version 2.0.15 - 19 nov 2020
    * Added some V2 asset endpoints
    * Updated CryptoExchange.Net

* Version 2.0.14 - 08 Oct 2020
    * Added symbol properties
    * Updated CryptoExchange.Net

* Version 2.0.13 - 28 Aug 2020
    * Updated CryptoExchange.Net

* Version 2.0.12 - 12 Aug 2020
    * Updated CryptoExchange.Net

* Version 2.0.11 - 03 Aug 2020
    * Added best offer stream

* Version 2.0.10 - 20 Jul 2020
    * Fixed transactionType mapping

* Version 2.0.10 - 20 Jul 2020
    * Fixed TransactionType mapping

* Version 2.0.9 - 07 Jul 2020
    * Fixed error parsing
    * Updated ticker model

* Version 2.0.8 - 21 Jun 2020
    * Updated CryptoExchange

* Version 2.0.7 - 16 Jun 2020
    * Updated CryptoExchange.Net

* Version 2.0.6 - 07 Jun 2020
	* Updated CryptoExchange.Net to fix order book desync

* Version 2.0.5 - 03 Mar 2020
    * Added clientOrderId to orders, added clientOrderId endpoints

* Version 2.0.4 - 27 Jan 2020
    * Updated CryptoExchange.Net

* Version 2.0.3 - 01 Nov 2019
    * Fixed websocket client authentication

* Version 2.0.1 - 23 Oct 2019
	* Fixed validation length symbols again
	
* Version 2.0.1 - 23 Oct 2019
	* Fixed validation length symbols

* Version 2.0.0 - 23 Oct 2019
	* See CryptoExchange.Net 3.0 release notes
	* Added input validation
	* Added CancellationToken support to all requests
	* Now using IEnumerable<> for collections
	* Renamed Market -> Symbol
	* Renamed MarketDepth -> OrderBook
	* Renamed QueryXXX -> GetXXX

* Version 1.1.9 - 11 Sep 2019
    * Updated CryptoExchange.Net

* Version 1.1.8 - 07 Aug 2019
    * Updated CryptoExchange.Net

* Version 1.1.7 - 05 Aug 2019
    * added code docs xml

* Version 1.1.6 - 01 Aug 2019
    * Added HistoryOrders endpoint, made symbol parameter optional for order retrieving methods

* Version 1.1.5 - 09 jul 2019
	* Updated HuobiSymbolOrderBook

* Version 1.1.4 - 27 jun 2019
	* Added Loan and Interest to HuobiBalanceTypes enum, fixing deserialization issue

* Version 1.1.3 - 24 jun 2019
	* Extended HuobiSymbol object

* Version 1.1.2 - 17 may 2019
	* Fix for deserializing stop-orders created on the website

* Version 1.1.1 - 17 may 2019
	* Added filter direction parameter to order/trade get methods
	* Added limit parameter to book depth
	* Added merge step parameter to order book implementation

* Version 1.1.0 - 14 may 2019
	* Added 4 hour kline to enum
	* Added option to sign public requests to fix rate limiting issues
	* Added an order book implementation for easily keeping an updated order book
	* Added additional constructor to ApiCredentials to be able to read from file

* Version 1.0.4 - 01 may 2019
	* Updated to latest CryptoExchange.Net
		* Adds response header to REST call result
		* Added rate limiter per API key
		* Unified socket client workings
	* Fixed authentication for russian API

* Version 1.0.3 - 25 mar 2019
	* Added missing ConfigureAwait(false)
	* Made decimals nullable for new markets in market data

* Version 1.0.2 - 18 mar 2019
	* Fixed amount parameter in PlaceOrder when precision = 0

* Version 1.0.1 - 07 mar 2019
	* Updated CryptoExchange.Net

* Version 1.0.0 - 27 feb 2019
	* Sub account support added
	* Fixed account type json mapping
	* Update CryptoExchange.Net

* Version 0.0.5 - 15 feb 2019
	* Fixed sync issue while placing order concurrently

* Version 0.0.4 - 12 feb 2019
	* Fixed order json mapping
	
* Version 0.0.3 - 01 feb 2019
	* Updated CryptoExchange.Net

* Version 0.0.2 - 17 dec 2018
	* Fix for reconnecting sometimes throwing an error
	* Fix for order update fields

* Version 0.0.1 - 07 dec 2018
	* Initial release
