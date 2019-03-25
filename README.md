# ![Icon](https://github.com/JKorf/Huobi.Net/blob/master/Resources/icon.png?raw=true) Huobi.Net 

![Build status](https://travis-ci.org/JKorf/Huobi.Net.svg?branch=master)

A .Net wrapper for the Huobi API as described on [Huobi](https://github.com/huobiapi), including all features the API provides using clear and readable objects.

**If you think something is broken, something is missing or have any questions, please open an [Issue](https://github.com/JKorf/Huobi.Net/issues)**

---
Also check out my other exchange API wrappers:
<table>
<tr>
<td><a href="https://github.com/JKorf/Bittrex.Net"><img src="https://github.com/JKorf/Bittrex.Net/blob/master/Resources/icon.png?raw=true"></a>
<br />
<a href="https://github.com/JKorf/Bittrex.Net">Bittrex</a>
</td>
<td><a href="https://github.com/JKorf/Bitfinex.Net"><img src="https://github.com/JKorf/Bitfinex.Net/blob/master/Resources/icon.png?raw=true"></a>
<br />
<a href="https://github.com/JKorf/Bitfinex.Net">Bitfinex</a>
</td>
<td><a href="https://github.com/JKorf/Binance.Net"><img src="https://github.com/JKorf/Binance.Net/blob/master/Resources/binance-coin.png?raw=true"></a>
<br />
<a href="https://github.com/JKorf/Binance.Net">Binance</a>
</td>
<td><a href="https://github.com/JKorf/CoinEx.Net"><img src="https://github.com/JKorf/CoinEx.Net/blob/master/Resources/icon.png?raw=true"></a>
<br />
<a href="https://github.com/JKorf/CoinEx.Net">CoinEx</a>
</td>
</table>

And other API wrappers based on CryptoExchange.Net:
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

Most API methods are available in two flavors, sync and async:
````C#
public void NonAsyncMethod()
{
    using(var client = new HuobiClient())
    {
        var result = client.GetMarketList();
    }
}

public async Task AsyncMethod()
{
    using(var client = new HuobiClient())
    {
        var result2 = await client.GetMarketListAsync();
    }
}
````

## Examples
Examples can be found in the Examples folder.


## Response handling
All API requests will respond with an CallResult object. This object contains whether the call was successful, the data returned from the call and an error if the call wasn't successful. As such, one should always check the Success flag when processing a response.
For example:
```C#
using(var client = new HuobiClient())
{
	var result = client.GetMarketTickers();
	if (result.Success)
		Console.WriteLine($"# markets: {result.Data.Ticks.Length}");
	else
		Console.WriteLine($"Error: {result.Error.Message}");
}
```
## Options & Authentication
The default behavior of the clients can be changed by providing options to the constructor, or using the `SetDefaultOptions` before creating a new client. Api credentials can be provided in the options.

## Websockets
The Huobi.Net socket client provides several socket endpoint to which can be subscribed and follow this function structure

```C#
var client = new HuobiSocketClient();

var subscribeResult = client.SubscribeToMarketTradeUpdates("ethusdt", data =>
{
	// handle data
});
```

**Handling socket events**

Subscribing to a socket stream returns a UpdateSubscription object. This object can be used to be notified when a socket is disconnected or reconnected:
````C#
var subscriptionResult = client.SubscribeToMarketTradeUpdates("ethusdt", data =>
{
	Console.WriteLine("Received trades update");
});

if(subscriptionResult.Success){
	sub.Data.Disconnected += () =>
	{
		Console.WriteLine("Socket disconnected");
	};

	sub.Data.Reconnected += (e) =>
	{
		Console.WriteLine("Socket reconnected after " + e);
	};
}
````

**Unsubscribing from socket endpoints:**

Sockets streams can be unsubscribed by using the `client.Unsubscribe` method in combination with the stream subscription received from subscribing:
```C#
var client = new HuobiSocketClient();

var successTrades = client.SubscribeToMarketTradeUpdates("ethusdt", (data) =>
{
	// handle data
});

client.Unsubscribe(successTrades.Data);
```

Additionaly, all sockets can be closed with the `UnsubscribeAll` method. Beware that when a client is disposed the sockets are automatically disposed. This means that if the code is no longer in the using statement the eventhandler won't fire anymore. To prevent this from happening make sure the code doesn't leave the using statement or don't use the socket client in a using statement:
```C#
// Doesn't leave the using block
using(var client = new HuobiSocketClient())
{
	var successTrades = client.SubscribeToMarketTradeUpdates("ethusdt", (data) =>
	{
		// handle data
	});

	Console.ReadLine();
}

// Without using block
var client = new HuobiSocketClient();
client.SubscribeToMarketTradeUpdates("ethusdt", (data) =>
{
	// handle data
});
```

## Release notes
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
