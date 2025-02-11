# ![.HTX.Net](https://github.com/JKorf/HTX.Net/blob/master/HTX.Net/Icon/icon.png?raw=true) HTX.Net

[![.NET](https://img.shields.io/github/actions/workflow/status/JKorf/HTX.Net/dotnet.yml?style=for-the-badge)](https://github.com/JKorf/HTX.Net/actions/workflows/dotnet.yml) ![License](https://img.shields.io/github/license/JKorf/HTX.Net?style=for-the-badge)

HTX.Net, previously known as Huobi.Net, is a strongly typed client library for accessing the [HTX REST and Websocket API](https://www.htx.com/en-us/opend/).
## Features
* Response data is mapped to descriptive models
* Input parameters and response values are mapped to discriptive enum values where possible
* Automatic websocket (re)connection management 
* Client side rate limiting 
* Client side order book implementation
* Extensive logging
* Support for different environments
* Easy integration with other exchange client based on the CryptoExchange.Net base library

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

## Install the library

### NuGet 
[![NuGet version](https://img.shields.io/nuget/v/JKorf.HTX.net.svg?style=for-the-badge)](https://www.nuget.org/packages/JKorf.HTX.Net)  [![Nuget downloads](https://img.shields.io/nuget/dt/JKorf.HTX.Net.svg?style=for-the-badge)](https://www.nuget.org/packages/JKorf.HTX.Net)

	dotnet add package JKorf.HTX.Net
	
### GitHub packages
HTX.Net is available on [GitHub packages](https://github.com/JKorf/HTX.Net/pkgs/nuget/JKorf.HTX.Net). You'll need to add `https://nuget.pkg.github.com/JKorf/index.json` as a NuGet package source.

### Download release
[![GitHub Release](https://img.shields.io/github/v/release/JKorf/HTX.Net?style=for-the-badge&label=GitHub)](https://github.com/JKorf/HTX.Net/releases)

The NuGet package files are added along side the source with the latest GitHub release which can found [here](https://github.com/JKorf/HTX.Net/releases).

## How to use
*REST Endpoints* 
 
```csharp
// Get the ETH/USDT ticker via rest request
var restClient = new HTXRestClient();
var tickerResult = await restClient.SpotApi.ExchangeData.GetTickerAsync("ETHUSDT");
var lastPrice = tickerResult.Data.ClosePrice;
```

*Websocket streams*  

```csharp
// Subscribe to ETH/USDT ticker updates via the websocket API
var socketClient = new HTXSocketClient();
var tickerSubscriptionResult = socketClient.SpotApi.SubscribeToTickerUpdatesAsync("ETHUSDT", (update) =>
{
	var lastPrice = update.Data.ClosePrice;
});
```

For information on the clients, dependency injection, response processing and more see the [HTX.Net documentation](https://jkorf.github.io/HTX.Net), [CryptoExchange.Net documentation](https://jkorf.github.io/CryptoExchange.Net), or have a look at the examples [here](https://github.com/JKorf/HTX.Net/tree/master/Examples) or [here](https://github.com/JKorf/CryptoExchange.Net/tree/master/Examples).

## CryptoExchange.Net
HTX.Net is based on the [CryptoExchange.Net](https://github.com/JKorf/CryptoExchange.Net) base library. Other exchange API implementations based on the CryptoExchange.Net base library are available and follow the same logic.

CryptoExchange.Net also allows for [easy access to different exchange API's](https://jkorf.github.io/CryptoExchange.Net#idocs_shared).

|Exchange|Repository|Nuget|
|--|--|--|
|Binance|[JKorf/Binance.Net](https://github.com/JKorf/Binance.Net)|[![Nuget version](https://img.shields.io/nuget/v/Binance.net.svg?style=flat-square)](https://www.nuget.org/packages/Binance.Net)|
|BingX|[JKorf/BingX.Net](https://github.com/JKorf/BingX.Net)|[![Nuget version](https://img.shields.io/nuget/v/JK.BingX.net.svg?style=flat-square)](https://www.nuget.org/packages/JK.BingX.Net)|
|Bitfinex|[JKorf/Bitfinex.Net](https://github.com/JKorf/Bitfinex.Net)|[![Nuget version](https://img.shields.io/nuget/v/Bitfinex.net.svg?style=flat-square)](https://www.nuget.org/packages/Bitfinex.Net)|
|Bitget|[JKorf/Bitget.Net](https://github.com/JKorf/Bitget.Net)|[![Nuget version](https://img.shields.io/nuget/v/JK.Bitget.net.svg?style=flat-square)](https://www.nuget.org/packages/JK.Bitget.Net)|
|BitMart|[JKorf/BitMart.Net](https://github.com/JKorf/BitMart.Net)|[![Nuget version](https://img.shields.io/nuget/v/BitMart.net.svg?style=flat-square)](https://www.nuget.org/packages/BitMart.Net)|
|BitMEX|[JKorf/BitMEX.Net](https://github.com/JKorf/BitMEX.Net)|[![Nuget version](https://img.shields.io/nuget/v/JKorf.BitMEX.net.svg?style=flat-square)](https://www.nuget.org/packages/JKorf.BitMEX.Net)|
|Bybit|[JKorf/Bybit.Net](https://github.com/JKorf/Bybit.Net)|[![Nuget version](https://img.shields.io/nuget/v/Bybit.net.svg?style=flat-square)](https://www.nuget.org/packages/Bybit.Net)|
|Coinbase|[JKorf/Coinbase.Net](https://github.com/JKorf/Coinbase.Net)|[![Nuget version](https://img.shields.io/nuget/v/JKorf.Coinbase.Net.svg?style=flat-square)](https://www.nuget.org/packages/JKorf.Coinbase.Net)|
|CoinEx|[JKorf/CoinEx.Net](https://github.com/JKorf/CoinEx.Net)|[![Nuget version](https://img.shields.io/nuget/v/CoinEx.net.svg?style=flat-square)](https://www.nuget.org/packages/CoinEx.Net)|
|CoinGecko|[JKorf/CoinGecko.Net](https://github.com/JKorf/CoinGecko.Net)|[![Nuget version](https://img.shields.io/nuget/v/CoinGecko.net.svg?style=flat-square)](https://www.nuget.org/packages/CoinGecko.Net)|
|Crypto.com|[JKorf/CryptoCom.Net](https://github.com/JKorf/CryptoCom.Net)|[![Nuget version](https://img.shields.io/nuget/v/CryptoCom.net.svg?style=flat-square)](https://www.nuget.org/packages/CryptoCom.Net)|
|Gate.io|[JKorf/GateIo.Net](https://github.com/JKorf/GateIo.Net)|[![Nuget version](https://img.shields.io/nuget/v/GateIo.net.svg?style=flat-square)](https://www.nuget.org/packages/GateIo.Net)|
|HyperLiquid|[JKorf/HyperLiquid.Net](https://github.com/JKorf/HyperLiquid.Net)|[![Nuget version](https://img.shields.io/nuget/v/HyperLiquid.Net.svg?style=flat-square)](https://www.nuget.org/packages/HyperLiquid.Net)|
|Kraken|[JKorf/Kraken.Net](https://github.com/JKorf/Kraken.Net)|[![Nuget version](https://img.shields.io/nuget/v/KrakenExchange.net.svg?style=flat-square)](https://www.nuget.org/packages/KrakenExchange.Net)|
|Kucoin|[JKorf/Kucoin.Net](https://github.com/JKorf/Kucoin.Net)|[![Nuget version](https://img.shields.io/nuget/v/Kucoin.net.svg?style=flat-square)](https://www.nuget.org/packages/Kucoin.Net)|
|Mexc|[JKorf/Mexc.Net](https://github.com/JKorf/Mexc.Net)|[![Nuget version](https://img.shields.io/nuget/v/JK.Mexc.net.svg?style=flat-square)](https://www.nuget.org/packages/JK.Mexc.Net)|
|OKX|[JKorf/OKX.Net](https://github.com/JKorf/OKX.Net)|[![Nuget version](https://img.shields.io/nuget/v/JK.OKX.net.svg?style=flat-square)](https://www.nuget.org/packages/JK.OKX.Net)|
|WhiteBit|[JKorf/WhiteBit.Net](https://github.com/JKorf/WhiteBit.Net)|[![Nuget version](https://img.shields.io/nuget/v/WhiteBit.net.svg?style=flat-square)](https://www.nuget.org/packages/WhiteBit.Net)|
|XT|[JKorf/XT.Net](https://github.com/JKorf/XT.Net)|[![Nuget version](https://img.shields.io/nuget/v/XT.net.svg?style=flat-square)](https://www.nuget.org/packages/XT.Net)|

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
|Sub user management|✓|`restClient.SpotApi.SubAccount`|
|Trading|✓|`restClient.SpotApi.Trading`|
|Conditional Order|✓|`restClient.SpotApi.Trading`|
|Margin Loan|✓|`restClient.SpotApi.Account`|
|Websocket Market Data|✓|`socketClient.SpotApi`|
|Websocket Account and Order|✓|`socketClient.SpotApi`|

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

### Coin-M Futures Api
|API|Supported|Location|
|--|--:|--|
|*|X||

### Coin-M Swap Api
|API|Supported|Location|
|--|--:|--|
|*|X||

## Support the project
Any support is greatly appreciated.

### Referal
If you do not yet have an account please consider using this referal link to sign up:  
[Link](https://www.htx.com/invite/en-us/1f?invite_code=fxp9)

### Donate
Make a one time donation in a crypto currency of your choice. If you prefer to donate a currency not listed here please contact me.

**Btc**:  bc1q277a5n54s2l2mzlu778ef7lpkwhjhyvghuv8qf  
**Eth**:  0xcb1b63aCF9fef2755eBf4a0506250074496Ad5b7   
**USDT (TRX)**  TKigKeJPXZYyMVDgMyXxMf17MWYia92Rjd

### Sponsor
Alternatively, sponsor me on Github using [Github Sponsors](https://github.com/sponsors/JKorf). 

## Release notes
* Version 6.9.0 - 11 Feb 2025
    * Updated CryptoExchange.Net to version 8.8.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added support for more SharedKlineInterval values
    * Added setting of DataTime value on websocket DataEvent updates
    * Added UpdateTime to HTXOrder response model
    * Added restClient.SpotApi.Trading.CancelAllOrdersAsync endpoint
    * Added missing parameters to restClient.SpotApi.Trading.GetOpenOrdersAsync endpoint
    * Added TotalTradeQuantity property to socketClient.SpotApi.SubscribeToOrderUpdatesAsync update model
    * Added restClient.SpotApi.ExchangeData.GetFullOrderBookAsync endpoint
    * Fix Mono runtime exception on rest client construction using DI

* Version 6.8.2 - 22 Jan 2025
    * Added restClient.SpotApi.ExchangeData.GetNetworksAsync

* Version 6.8.1 - 07 Jan 2025
    * Updated CryptoExchange.Net version
    * Added Type property to HTXExchange class

* Version 6.8.0 - 23 Dec 2024
    * Updated CryptoExchange.Net to version 8.5.0, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Added SetOptions methods on Rest and Socket clients
    * Added setting of DefaultProxyCredentials to CredentialCache.DefaultCredentials on the DI http client
    * Improved websocket disconnect detection

* Version 6.7.2 - 08 Dec 2024
    * Updated CryptoExchange.Net to version 8.4.4 to fix deserialization error in .net framework

* Version 6.7.1 - 03 Dec 2024
    * Updated CryptoExchange.Net to version 8.4.3, see https://github.com/JKorf/CryptoExchange.Net/releases/
    * Fix for AllowAppendingClientOrderId options setting

* Version 6.7.0 - 02 Dec 2024
    * Added AllowAppendingClientOrderId option
    * Updated client order id logic for client reference for the Spot API
    * Fix for orderbook creation via HTXOrderBookFactory

* Version 6.6.0 - 28 Nov 2024
    * Updated CryptoExchange.Net to version 8.4.0, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/8.4.0
    * Added GetFeesAsync Shared REST client implementations
    * Updated HTXOptions to LibraryOptions implementation
    * Updated test and analyzer package versions

* Version 6.5.0 - 19 Nov 2024
    * Updated CryptoExchange.Net to version 8.3.0
    * Added support for loading client settings from IConfiguration
    * Added DI registration method for configuring Rest and Socket options at the same time
    * Added DisplayName and ImageUrl properties to HTXExchange class
    * Updated client constructors to accept IOptions from DI
    * Removed redundant HTXSocketClient constructor

* Version 6.4.1 - 11 Nov 2024
    * Fixed deserialization issue in restClient.UsdtFuturesApi.ExchangeData.GetTickersAsync endpoint

* Version 6.4.0 - 06 Nov 2024
    * Updated CryptoExchange.Net to version 8.2.0, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/8.2.0

* Version 6.3.0 - 28 Oct 2024
    * Updated CryptoExchange.Net to version 8.1.0, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/8.1.0
    * Moved FormatSymbol to HTXExchange class
    * Added support Side setting on SharedTrade model
    * Added HTXTrackerFactory for creating trackers
    * Added overload to Create method on HTXOrderBookFactory support SharedSymbol parameter
    * Fixed rate limiting incorrectly applied to websocket market data connections

* Version 6.2.0 - 21 Oct 2024
    * Added socketClient.SpotApi Order management requests
    * Added restClient.UsdtFuturesApi.ExchangeData.GetInsuranceFundInfoAsync endpoint

* Version 6.1.3 - 14 Oct 2024
    * Updated CryptoExchange.Net to version 8.0.3, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/8.0.3
    * Fixed TypeLoadException during initialization

* Version 6.1.2 - 14 Oct 2024
    * Fixed cancellation token not being passed to subscribe method in Shared client

* Version 6.1.1 - 08 Oct 2024
    * Fixed LastPrice value on SpotTicker Shared implementation

* Version 6.1.0 - 27 Sep 2024
    * Updated CryptoExchange.Net to version 8.0.0, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/8.0.0
    * Added Shared client interfaces implementation for Spot and UsdtFuturesApi Rest and Socket clients
    * Added QuoteQuantity property to HTXOrderUpdate model
    * Updated from parameter type from int? to long? on SpotApi.Account.GetWithdrawalDepositHistoryAsync
    * Updated Status property type from string to SymbolStatus on HTXSymbolConfig model
    * Updated OrderSide property type from string to OrderSide on HTXUsdtMarginSwapOrderUpdate
    * Updated Sourcelink package version
    * Fixed UsdtFuturesApi.Account.SetIsolatedMarginPositionModeAsync, SetCrossmarginPositionModeAsync, GetIsolatedMarginPositionModeAsync and GetCrossMarginPositionMode response deserialization
    * Marked ISpotClient references as deprecated

* Version 6.0.2 - 18 Aug 2024
    * Fix deserialization undocumented canceled-source field value

* Version 6.0.1 - 09 Aug 2024
    * Fixed websocket SpotApi queries (GetXX methods)

* Version 6.0.0 - 08 Aug 2024
    * Renamed library from Huobi.Net to HTX.Net, following the renaming of the exchange
    * Renamed all models and references from Huobi... to HTX...
    * Renamed UsdtMarginSwapApi to UsdtFuturesApi
    * Renamed some endpoints to match standardized endpoint names
    * Split Margin and SubAccount endpoints into separate topics in the rest SpotApi
    * Split SubAccount endpoints into separate topics in the rest FuturesApi
    * Added UsdtFuturesSymbolOrderBook implementation
    * Added client side ratelimiting
    * Added various missing endpoints
    * Added Usdt Futures API account websocket streams
    * Updated from Newtonsoft.Json to System.Text.Json for json handling
    * Updated code xml comments
    * Updated API documentation references
    * Fixed a large number of bugs

* Version 5.7.0 - 07 Aug 2024
    * Updated CryptoExchange.Net to version 7.11.0, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/7.11.0

* Version 5.6.0 - 27 Jul 2024
    * Updated CryptoExchange.Net to version 7.10.0, see https://github.com/JKorf/CryptoExchange.Net/releases/tag/7.10.0

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
