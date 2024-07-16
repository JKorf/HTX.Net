# ![.Huobi.Net](https://github.com/JKorf/Huobi.Net/blob/master/Huobi.Net/Icon/icon.png?raw=true) Huobi.Net

[![.NET](https://img.shields.io/github/actions/workflow/status/JKorf/Huobi.Net/dotnet.yml?style=for-the-badge)](https://github.com/JKorf/Huobi.Net/actions/workflows/dotnet.yml) ![License](https://img.shields.io/github/license/JKorf/Huobi.Net?style=for-the-badge)

Huobi.Net is a strongly typed client library for accessing the [Huobi REST and Websocket API](https://github.com/huobiapi). All data is mapped to readable models and enum values. Additional features include an implementation for maintaining a client side order book, easy integration with other exchange client libraries and more.

## Supported Frameworks
The library is targeting both `.NET Standard 2.0` and `.NET Standard 2.1` for optimal compatibility

|.NET implementation|Version Support|
|--|--|
|.NET Core|`2.0` and higher|
|.NET Framework|`4.6.1` and higher|
|Mono|`5.4` and higher|
|Xamarin.iOS|`10.14` and higher|
|Xamarin.Android|`8.0` and higher|
|UWP|`10.0.16299` and higher|
|Unity|`2018.1` and higher|

## Get the library
[![Nuget version](https://img.shields.io/nuget/v/Huobi.net.svg?style=for-the-badge)](https://www.nuget.org/packages/Huobi.Net)  [![Nuget downloads](https://img.shields.io/nuget/dt/Huobi.Net.svg?style=for-the-badge)](https://www.nuget.org/packages/Huobi.Net)  

	dotnet add package Huobi.Net

## How to use
*REST Endpoints* 
 
```csharp
// Get the ETH/USDT ticker via rest request
var restClient = new HuobiRestClient();
var tickerResult = await restClient.SpotApi.ExchangeData.GetTickerAsync("ETHUSDT");
var lastPrice = tickerResult.Data.ClosePrice;
```

*Websocket streams*  

```csharp
// Subscribe to ETH/USDT ticker updates via the websocket API
var socketClient = new HuobiSocketClient();
var tickerSubscriptionResult = socketClient.SpotApi.SubscribeToTickerUpdatesAsync("ethusdt", (update) =>
{
	var lastPrice = update.Data.ClosePrice;
});
```

For information on the clients, dependency injection, response processing and more see the [Huobi.Net documentation](https://jkorf.github.io/Huobi.Net), [CryptoExchange.Net documentation](https://jkorf.github.io/CryptoExchange.Net), or have a look at the examples [here](https://github.com/JKorf/Huobi.Net/tree/master/Examples) or [here](https://github.com/JKorf/CryptoExchange.Net/tree/master/Examples).

## CryptoExchange.Net
Huobi.Net is based on the [CryptoExchange.Net](https://github.com/JKorf/CryptoExchange.Net) base library. Other exchange API implementations based on the CryptoExchange.Net base library are available and follow the same logic.

CryptoExchange.Net also allows for [easy access to different exchange API's](https://jkorf.github.io/CryptoExchange.Net#idocs_common).

|Exchange|Repository|Nuget|
|--|--|--|
|Binance|[JKorf/Binance.Net](https://github.com/JKorf/Binance.Net)|[![Nuget version](https://img.shields.io/nuget/v/Binance.net.svg?style=flat-square)](https://www.nuget.org/packages/Binance.Net)|
|BingX|[JKorf/BingX.Net](https://github.com/JKorf/BingX.Net)|[![Nuget version](https://img.shields.io/nuget/v/JK.BingX.net.svg?style=flat-square)](https://www.nuget.org/packages/JK.BingX.Net)|
|Bitfinex|[JKorf/Bitfinex.Net](https://github.com/JKorf/Bitfinex.Net)|[![Nuget version](https://img.shields.io/nuget/v/Bitfinex.net.svg?style=flat-square)](https://www.nuget.org/packages/Bitfinex.Net)|
|Bitget|[JKorf/Bitget.Net](https://github.com/JKorf/Bitget.Net)|[![Nuget version](https://img.shields.io/nuget/v/JK.Bitget.net.svg?style=flat-square)](https://www.nuget.org/packages/JK.Bitget.Net)|
|Bybit|[JKorf/Bybit.Net](https://github.com/JKorf/Bybit.Net)|[![Nuget version](https://img.shields.io/nuget/v/Bybit.net.svg?style=flat-square)](https://www.nuget.org/packages/Bybit.Net)|
|CoinEx|[JKorf/CoinEx.Net](https://github.com/JKorf/CoinEx.Net)|[![Nuget version](https://img.shields.io/nuget/v/CoinEx.net.svg?style=flat-square)](https://www.nuget.org/packages/CoinEx.Net)|
|CoinGecko|[JKorf/CoinGecko.Net](https://github.com/JKorf/CoinGecko.Net)|[![Nuget version](https://img.shields.io/nuget/v/CoinGecko.net.svg?style=flat-square)](https://www.nuget.org/packages/CoinGecko.Net)|
|Gate.io|[JKorf/GateIo.Net](https://github.com/JKorf/GateIo.Net)|[![Nuget version](https://img.shields.io/nuget/v/GateIo.net.svg?style=flat-square)](https://www.nuget.org/packages/GateIo.Net)|
|Kraken|[JKorf/Kraken.Net](https://github.com/JKorf/Kraken.Net)|[![Nuget version](https://img.shields.io/nuget/v/KrakenExchange.net.svg?style=flat-square)](https://www.nuget.org/packages/KrakenExchange.Net)|
|Kucoin|[JKorf/Kucoin.Net](https://github.com/JKorf/Kucoin.Net)|[![Nuget version](https://img.shields.io/nuget/v/Kucoin.net.svg?style=flat-square)](https://www.nuget.org/packages/Kucoin.Net)|
|Mexc|[JKorf/Mexc.Net](https://github.com/JKorf/Mexc.Net)|[![Nuget version](https://img.shields.io/nuget/v/JK.Mexc.net.svg?style=flat-square)](https://www.nuget.org/packages/JK.Mexc.Net)|
|OKX|[JKorf/OKX.Net](https://github.com/JKorf/OKX.Net)|[![Nuget version](https://img.shields.io/nuget/v/JK.OKX.net.svg?style=flat-square)](https://www.nuget.org/packages/JK.OKX.Net)|

## Discord
[![Nuget version](https://img.shields.io/discord/847020490588422145?style=for-the-badge)](https://discord.gg/MSpeEtSY8t)  
A Discord server is available [here](https://discord.gg/MSpeEtSY8t). Feel free to join for discussion and/or questions around the CryptoExchange.Net and implementation libraries.

## Supported functionality

### Spot Api
|API|Supported|Location|
|--|--:|--|
|Reference Data|✓|`restClient.SpotApi.ExchangeData`|
|Market Data|✓|`restClient.SpotApi.ExchangeData`|
|Account|✓|`restClient.SpotApi.Account`|
|Wallet|✓|`restClient.SpotApi.Account`|
|Sub user management|Partial|`restClient.SpotApi.Account`|
|Trading|✓|`restClient.SpotApi.Trading`|
|Conditional Order|✓|`restClient.SpotApi.Trading`|
|Margin Loan|✓|`restClient.SpotApi.Account`|
|Margin Loan|✓|`restClient.SpotApi.Account`|
|Websocket Market Data|✓|`socketClient.SpotApi`|
|Websocket Account and Order|✓|`socketClient.SpotApi`|

### Coin-M Futures Api
|API|Supported|Location|
|--|--:|--|
|*|X||

### Coin-M Swap Api
|API|Supported|Location|
|--|--:|--|
|*|X||

### USDT-M Api
|API|Supported|Location|
|--|--:|--|
|Reference Data|✓|`restClient.UsdtMarginSwapApi.ExchangeData`|
|Swap Market Data Interface|✓|`restClient.UsdtMarginSwapApi.ExchangeData`|
|Swap Account Interface|✓|`restClient.UsdtMarginSwapApi.Account`|
|Swap Trade Interface|✓|`restClient.UsdtMarginSwapApi.Trading`|
|Swap Strategy Order Interface|X||
|Swap Transferring Interface|X|`restClient.SpotApi.Account`|
|Websocket Market Interface|✓|`socketClient.UsdtMarginSwapApi`|
|Websocket Index and Basis Interface|✓|`socketClient.UsdtMarginSwapApi`|
|Orders And Account WebSocket|X||
|WebSocket System updates|X||

## Support the project
I develop and maintain this package on my own for free in my spare time, any support is greatly appreciated.

### Donate
Make a one time donation in a crypto currency of your choice. If you prefer to donate a currency not listed here please contact me.

**Btc**:  bc1q277a5n54s2l2mzlu778ef7lpkwhjhyvghuv8qf  
**Eth**:  0xcb1b63aCF9fef2755eBf4a0506250074496Ad5b7   
**USDT (TRX)**  TKigKeJPXZYyMVDgMyXxMf17MWYia92Rjd

### Sponsor
Alternatively, sponsor me on Github using [Github Sponsors](https://github.com/sponsors/JKorf). 

## Release notes
* Version 5.5.0 - 16 Jul 2024
    * Updated CryptoExchange.Net to version 7.9.0, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/7.9.0
    * Updated internal classes to internal access modifier

* Version 5.4.1 - 02 Jul 2024
    * Updated CryptoExchange.Net to V7.8.0, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/7.8.0

* Version 5.4.0 - 23 Jun 2024
    * Updated CryptoExchange.Net to version 7.7.0, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/7.7.0
    * Updated response models from classes to records

* Version 5.3.0 - 11 Jun 2024
    * Updated CryptoExchange.Net to v7.6.0, see https://github.com/JKorf/CryptoExchange.Net?tab=readme-ov-file#release-notes for release notes

* Version 5.2.8 - 07 May 2024
    * Updated CryptoExchange.Net to v7.5.2, see https://github.com/JKorf/CryptoExchange.Net?tab=readme-ov-file#release-notes for release notes

* Version 5.2.7 - 01 May 2024
    * Updated CryptoExchange.Net to v7.5.0, see https://github.com/JKorf/CryptoExchange.Net?tab=readme-ov-file#release-notes for release notes

* Version 5.2.6 - 28 Apr 2024
    * Added HuobiExchange static info class
    * Added HuobiOrderBookFactory book creation method
    * Fixed HuobiOrderBookFactory injection issue
    * Fixed result checking on common spot GetTickerAsync endpoint
    * Updated CryptoExchange.Net to v7.4.0, see https://github.com/JKorf/CryptoExchange.Net?tab=readme-ov-file#release-notes for release notes

* Version 5.2.5 - 23 Apr 2024
    * Updated OrderSide and OrderType enum values and converters
    * Updated CryptoExchange.Net to 7.3.3, see https://github.com/JKorf/CryptoExchange.Net?tab=readme-ov-file#release-notes for release notes

* Version 5.2.4 - 18 Apr 2024
    * Updated CryptoExchange.Net to 7.3.1, see https://github.com/JKorf/CryptoExchange.Net?tab=readme-ov-file#release-notes for release notes
    * Until new rate limit configuration and handling has been implemented client side rate limiting has been disabled

* Version 5.2.3 - 12 Apr 2024
    * Fix for futures broker id reference

* Version 5.2.2 - 03 Apr 2024
    * Updated string comparision for improved performance
    * Fix for error parsing rest client
    * Removed pre-send symbol validation

* Version 5.2.1 - 24 Mar 2024
	* Updated CryptoExchange.Net to 7.2.0, see https://github.com/JKorf/CryptoExchange.Net?tab=readme-ov-file#release-notes for release notes
	* Added Market to OrderPriceType enum

* Version 5.2.0 - 16 Mar 2024
    * Updated CryptoExchange.Net to 7.1.0, see https://github.com/JKorf/CryptoExchange.Net?tab=readme-ov-file#release-notes for release notes
	
* Version 5.1.0 - 25 Feb 2024
    * Updated CryptoExchange.Net and implemented reworked websocket message handling. For release notes for the CryptoExchange.Net base library see: https://github.com/JKorf/CryptoExchange.Net?tab=readme-ov-file#release-notes
    * Fixed issue in DI registration causing http client to not be correctly injected
    * Removed redundant HuobiRestClient constructor overload
    * Updated some namespaces

* Version 5.0.5 - 03 Dec 2023
    * Updated CryptoExchange.Net

* Version 5.0.4 - 19 Nov 2023
    * Added support for sending broker id

* Version 5.0.3 - 24 Oct 2023
    * Updated CryptoExchange.Net

* Version 5.0.2 - 09 Oct 2023
    * Updated CryptoExchange.Net version
    * Added ISpotClient to DI injection

* Version 5.0.1 - 25 Aug 2023
    * Updated CryptoExchange.Net

* Version 5.0.0 - 25 Jun 2023
    * Updated CryptoExchange.Net to version 6.0.0
    * Renamed HuobiClient to HuobiRestClient
    * Renamed **Streams to **Api on the HuobiSocketClient
    * Updated endpoints to consistently use a base url without any path as basis to make switching environments/base urls clearer
    * Added IHuobiOrderBookFactory and implementation for creating order books
    * Updated dependency injection register method (AddHuobi)

* Version 4.2.4 - 18 Mar 2023
    * Updated CryptoExchange.Net

* Version 4.2.3 - 14 Feb 2023
    * Updated CryptoExchange.Net

* Version 4.2.2 - 05 Feb 2023
    * Fixed leverageRate parameter for margin swap orders

* Version 4.2.1 - 29 Dec 2022
    * Fixed GetCurrentFeeRatesAsync deserialization

* Version 4.2.0 - 17 Nov 2022
    * Added Usdt Margin Swap API
    * Updated CryptoExchange.Net
    * Added user fee rate endpoint

* Version 4.1.8 - 31 Jul 2022
    * Added missing account types
    * Fixed culture issue authenticating requests

* Version 4.1.7 - 18 Jul 2022
    * Updated CryptoExchange.Net

* Version 4.1.6 - 16 Jul 2022
    * Updated CryptoExchange.Net

* Version 4.1.5 - 10 Jul 2022
    * Updated CryptoExchange.Net

* Version 4.1.4 - 12 Jun 2022
    * Updated CryptoExchange.Net

* Version 4.1.3 - 24 May 2022
    * Updated CryptoExchange.Net

* Version 4.1.2 - 22 May 2022
    * Updated CryptoExchange.Net

* Version 4.1.1 - 12 May 2022
    * Fixed CryptoExchange.Net reference

* Version 4.1.0 - 12 May 2022
    * Added algo order endpoints
    * Added margin endpoints
    * Updated Url for partial order book stream
    * Removed symbol restriction on GetOpenOrdersAsync

* Version 4.0.10 - 08 May 2022
    * Updated CryptoExchange.Net

* Version 4.0.9 - 01 May 2022
    * Updated CryptoExchange.Net which fixed an timing related issue in the websocket reconnection logic
    * Added seconds representation to KlineInterval enum

* Version 4.0.8 - 14 Apr 2022
    * Updated CryptoExchange.Net

* Version 4.0.7 - 14 Mar 2022
    * Added GetUserIdAsync endpoint
    * Added GetSubAccountUsersAsync endpoint
    * Added GetSubUserAccountsAsync endpoint

* Version 4.0.6 - 10 Mar 2022
    * Updated CryptoExchange.Net
    * Fixed CancellationToken not being passed to Common GetRecentTradesAsync

* Version 4.0.5 - 08 Mar 2022
    * Updated CryptoExchange.Net

* Version 4.0.4 - 01 Mar 2022
    * Updated CryptoExchange.Net improving the websocket reconnection robustness

* Version 4.0.3 - 27 Feb 2022
    * Updated CryptoExchange.Net to fix timestamping issue when request is ratelimiter

* Version 4.0.2 - 25 Feb 2022
    * Fixed missing Side property on GetUserTradeAsync response model

* Version 4.0.1 - 24 Feb 2022
    * Updated CryptoExchange.Net

* Version 4.0.0 - 18 Feb 2022
	* Added Github.io page for documentation: https://jkorf.github.io/Huobi.Net/
	* Added unit tests for parsing the returned JSON for each endpoint and subscription
	* Added AddHuobi extension method on IServiceCollection for easy dependency injection
	* Added URL reference to API endpoint documentation for each endpoint
	* Added default rate limiter

	* Refactored OrderType to be split in Type and Side to be consistent across exchange implementations
	* Refactored client structure to be consistent across exchange implementations
	* Renamed various properties to be consistent across exchange implementations

	* Cleaned up project structure
	* Fixed various models

	* Updated CryptoExchange.Net, see https://github.com/JKorf/CryptoExchange.Net#release-notes
	* See https://jkorf.github.io/Huobi.Net/MigrationGuide.html for additional notes for updating from V3 to V4

* Version 3.2.4 - 03 Nov 2021
    * Fix for invalid check in SubscribeToOrderDetailsUpdatesAsync

* Version 3.2.3 - 08 Oct 2021
    * Updated CryptoExchange.Net to fix some socket issues

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
