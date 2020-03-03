# ![Icon](https://github.com/JKorf/Huobi.Net/blob/master/Huobi.Net/Icon/icon.png?raw=true) Huobi.Net 

![Build status](https://travis-ci.org/JKorf/Huobi.Net.svg?branch=master)

A .Net wrapper for the Huobi API as described on [Huobi](https://github.com/huobiapi), including all features the API provides using clear and readable objects.

**If you think something is broken, something is missing or have any questions, please open an [Issue](https://github.com/JKorf/Huobi.Net/issues)**

## CryptoExchange.Net
Implementation is build upon the CryptoExchange.Net library, make sure to also check out the documentation on that: [docs](https://github.com/JKorf/CryptoExchange.Net)

Other CryptoExchange.Net implementations:
<table>
<tr>
<td><a href="https://github.com/JKorf/Bittrex.Net"><img src="https://github.com/JKorf/Bittrex.Net/blob/master/Bittrex.Net/Icon/icon.png?raw=true"></a>
<br />
<a href="https://github.com/JKorf/Bittrex.Net">Bittrex</a>
</td>
<td><a href="https://github.com/JKorf/Bitfinex.Net"><img src="https://github.com/JKorf/Bitfinex.Net/blob/master/Bitfinex.Net/Icon/icon.png?raw=true"></a>
<br />
<a href="https://github.com/JKorf/Bitfinex.Net">Bitfinex</a>
</td>
<td><a href="https://github.com/JKorf/Binance.Net"><img src="https://github.com/JKorf/Binance.Net/blob/master/Binance.Net/Icon/icon.png?raw=true"></a>
<br />
<a href="https://github.com/JKorf/Binance.Net">Binance</a>
</td>
<td><a href="https://github.com/JKorf/CoinEx.Net"><img src="https://github.com/JKorf/CoinEx.Net/blob/master/CoinEx.Net/Icon/icon.png?raw=true"></a>
<br />
<a href="https://github.com/JKorf/CoinEx.Net">CoinEx</a>
</td>
<td><a href="https://github.com/JKorf/Kucoin.Net"><img src="https://github.com/JKorf/Kucoin.Net/blob/master/Kucoin.Net/Icon/icon.png?raw=true"></a>
<br />
<a href="https://github.com/JKorf/Kucoin.Net">Kucoin</a>
</td>
<td><a href="https://github.com/JKorf/Kraken.Net"><img src="https://github.com/JKorf/Kraken.Net/blob/master/Kraken.Net/Icon/icon.png?raw=true"></a>
<br />
<a href="https://github.com/JKorf/Kraken.Net">Kraken</a>
</td>
</tr>
</table>
Implementations from third parties:
<table>
<tr>
<td><a href="https://github.com/Zaliro/Switcheo.Net"><img src="https://github.com/Zaliro/Switcheo.Net/blob/master/Resources/switcheo-coin.png?raw=true"></a>
<br />
<a href="https://github.com/Zaliro/Switcheo.Net">Switcheo</a>
</td>
	<td><a href="https://github.com/ridicoulous/LiquidQuoine.Net"><img src="https://github.com/ridicoulous/LiquidQuoine.Net/blob/master/Resources/icon.png?raw=true"></a>
<br />
<a href="https://github.com/ridicoulous/LiquidQuoine.Net">Liquid</a>
</td>
<td><a href="https://github.com/burakoner/OKEx.Net"><img src="https://raw.githubusercontent.com/burakoner/OKEx.Net/master/Okex.Net/Icon/icon.png"></a>
<br />
<a href="https://github.com/burakoner/OKEx.Net">OKEx</a>
</td>
</tr>
</table>


## Donations
Donations are greatly appreciated and a motivation to keep improving.

**Btc**:  12KwZk3r2Y3JZ2uMULcjqqBvXmpDwjhhQS  
**Eth**:  0x069176ca1a4b1d6e0b7901a6bc0dbf3bb0bf5cc2  
**Nano**: xrb_1ocs3hbp561ef76eoctjwg85w5ugr8wgimkj8mfhoyqbx4s1pbc74zggw7gs  


## Installation
![Nuget version](https://img.shields.io/nuget/v/Huobi.net.svg)  ![Nuget downloads](https://img.shields.io/nuget/dt/Huobi.Net.svg)
Available on [Nuget](https://www.nuget.org/packages/Huobi.Net/).
```
pm> Install-Package Huobi.Net
```
To get started with Huobi.Net first you will need to get the library itself. The easiest way to do this is to install the package into your project using  [NuGet](https://www.nuget.org/packages/Huobi.Net/). Using Visual Studio this can be done in two ways.

### Using the package manager
In Visual Studio right click on your solution and select 'Manage NuGet Packages for solution...'. A screen will appear which initially shows the currently installed packages. In the top bit select 'Browse'. This will let you download net package from the NuGet server. In the search box type'Huobi.Net' and hit enter. The Huobi.Net package should come up in the results. After selecting the package you can then on the right hand side select in which projects in your solution the package should install. After you've selected all project you wish to install and use Huobi.Net in hit 'Install' and the package will be downloaded and added to you projects.

### Using the package manager console
In Visual Studio in the top menu select 'Tools' -> 'NuGet Package Manager' -> 'Package Manager Console'. This should open up a command line interface. On top of the interface there is a dropdown menu where you can select the Default Project. This is the project that Huobi.Net will be installed in. After selecting the correct project type  `Install-Package Huobi.Net`  in the command line interface. This should install the latest version of the package in your project.

After doing either of above steps you should now be ready to actually start using Huobi.Net.
## Getting started
After installing it's time to actually use it. To get started you have to add the Huobi.Net namespace: `using Huobi.Net;`.

Huobi.Net provides two clients to interact with the Huobi API. The `HuobiClient` provides all rest API calls. The  `HuobiSocketClient`  provides functions to interact with the websocket provided by the Huobi API. Both clients are disposable and as such can be used in a `using` statement.

## Examples
Examples can be found in the Examples folder.

## Release notes
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
