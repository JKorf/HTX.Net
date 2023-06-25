---
title: Getting started
nav_order: 2
---

## Creating client
There are 2 clients available to interact with the Huobi API, the `HuobiRestClient` and `HuobiSocketClient`. They can be created manually on the fly or be added to the dotnet DI using the `AddHuobi` extension method.

*Create a new rest client*
```csharp
var huobiRestClient = new HuobiRestClient(options =>
{
	// Set options here for this client
});

var huobiSocketClient = new HuobiSocketClient(options =>
{
	// Set options here for this client
});
```

*Using dotnet dependency inject*
```csharp
services.AddHuobi(
	restOptions => {
		// set options for the rest client
	},
	socketClientOptions => {
		// set options for the socket client
	});	
	
// IHuobiRestClient, IHuobiSocketClient and IHuobiOrderBookFactory are now available for injecting
```

Different options are available to set on the clients, see this example
```csharp
var huobiRestClient = new HuobiRestClient(options =>
{
	options.ApiCredentials = new ApiCredentials("API-KEY", "API-SECRET");
	options.RequestTimeout = TimeSpan.FromSeconds(60);
});
```
Alternatively, options can be provided before creating clients by using `SetDefaultOptions` or during the registration in the DI container:  
```csharp
HuobiRestClient.SetDefaultOptions(options => {
	// Set options here for all new clients
});
var huobiRestClient = new HuobiRestClient();
```
More info on the specific options can be found on the [CryptoExchange.Net wiki](https://jkorf.github.io/CryptoExchange.Net/Options.html)

### Dependency injection
See [CryptoExchange.Net documentation](https://jkorf.github.io/CryptoExchange.Net/Dependency%20Injection.html)
