---
title: Getting started
nav_order: 2
---

## Creating client
There are 2 clients available to interact with the Huobi API, the `HuobiClient` and `HuobiSocketClient`.

*Create a new rest client*
```csharp
var huobiClient = new HuobiClient(new HuobiClientOptions()
{
	// Set options here for this client
});
```

*Create a new socket client*
```csharp
var huobiSocketClient = new HuobiSocketClient(new HuobiSocketClientOptions()
{
	// Set options here for this client
});
```

Different options are available to set on the clients, see this example
```csharp
var huobiClient = new HuobiClient(new HuobiClientOptions()
{
	ApiCredentials = new ApiCredentials("API-KEY", "API-SECRET"),
	LogLevel = LogLevel.Trace,
	RequestTimeout = TimeSpan.FromSeconds(60)
});
```
Alternatively, options can be provided before creating clients by using `SetDefaultOptions`:
```csharp
HuobiClient.SetDefaultOptions(new HuobiClientOptions{
	// Set options here for all new clients
});
var huobiClient = new HuobiClient();
```
More info on the specific options can be found on the [CryptoExchange.Net wiki](https://jkorf.github.io/CryptoExchange.Net/Options.html)

### Dependency injection
See [CryptoExchange.Net wiki](https://jkorf.github.io/CryptoExchange.Net/Clients.html#dependency-injection)
