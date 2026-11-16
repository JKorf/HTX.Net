---
name: htx-net
description: Implement or review HTX.Net (formerly Huobi) spot, margin, subaccount, and USDT futures REST workflows, V5 futures, WebSocket subscriptions and order queries, authentication, Shared API integration, and dependency injection in .NET applications.
---
# HTX.Net

Install with `dotnet add package JKorf.HTX.Net`.

- Reuse `HTXRestClient` and `HTXSocketClient`, or use dependency injection. Current roots include `SpotApi`, `UsdtFuturesApi`, and `UsdtFuturesV5Api`.
- Private operations use `HTXCredentials(key, secret)` for HMAC, without a passphrase. Ed25519 credential wrappers are also available on .NET 8 and later targets. Public market data needs no credentials.
- Spot symbols are compact (`ETHUSDT`); USDT futures contract codes use dashes (`ETH-USDT`). Select instruments from the relevant metadata endpoint.
- Spot balances and orders require an account ID. Select the intended account type and verify its state rather than taking the first account returned.
- REST returns `HttpResult<T>` / `HttpResult`, subscriptions return `WebSocketResult<UpdateSubscription>`, and socket requests return `QueryResult<T>` / `QueryResult`. Check `Success` before `Data` and handle `Error`. Batch outcomes need item-level inspection.
- `UsdtFuturesApi` has separate cross/isolated methods with contract quantities, leverage, and open/close intent. V5 uses its own methods/models; do not mix their parameters.
- Keep the concrete socket client and unsubscribe successful subscriptions on shutdown. Order/query results are not subscriptions.
- Use supported roots' `SharedApi` for Shared API V2. Depend on the required capability and inspect exchange parameter rules, especially spot `AccountId` and futures margin settings.

## References

- Read [API surfaces](references/api-surfaces.md) for routing, V5 capabilities, account selection, catalogs, quantities, and prerequisites.
- Read [usage](references/usage.md) for authentication, market/account reads, spot/futures trading, subscription cleanup, Shared API V2, and DI snippets.
- Read [operational considerations](references/safety.md) for account scope, margin modes, batch outcomes, submission recovery, and fund movements.

Checkout-only: [repository instructions](../../../AGENTS.md) and [complete examples](../../../Examples/ai-friendly/).
