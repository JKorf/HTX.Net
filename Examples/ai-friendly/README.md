# AI-Friendly Examples

These examples are optimized for AI coding assistants and quick onboarding. Each file is:

- **Compilable** - drop into a console project with `dotnet add package JKorf.HTX.Net` and it builds.
- **Self-contained** - single file, no external setup, no shared helpers.
- **Heavily commented** - explains why each line is there, not just what it does.
- **Idiomatic** - follows current HTX.Net 8.x patterns.

## Files

| File | What it shows |
|---|---|
| `01-spot-quickstart.cs` | Client setup, public ticker, authenticated account id and balances, place limit order, query/cancel order |
| `02-futures.cs` | USDT futures cross margin: set leverage, place market order, get position rows, close position |
| `03-websocket.cs` | Subscribe to spot ticker, klines, account updates, order updates, with proper teardown |
| `04-multi-exchange.cs` | `CryptoExchange.Net.SharedApis` pattern for exchange-agnostic code |
| `05-error-handling.cs` | `HttpResult` patterns, retry, HTX-specific routing and validation mistakes |

## Running

```bash
dotnet new console -n MyHtxApp
cd MyHtxApp
dotnet add package JKorf.HTX.Net
# Copy the example .cs file content into Program.cs
# Replace API_KEY / API_SECRET placeholders with your own
dotnet run
```
