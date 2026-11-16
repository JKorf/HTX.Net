# Operational considerations

## Credentials and account scope

Private REST/socket operations use HTX credentials. The common constructor takes HMAC key and secret without a passphrase; Ed25519 credentials are available on supported modern targets. Keep credentials outside source and grant permissions for the intended API area.

Spot account IDs identify different account types. Select the intended spot or margin account and verify its state; do not submit trades against an arbitrary first account. Balance rows distinguish balance types, so do not sum frozen and tradable fields as though all were available funds.

## Spot, margin, and conditional execution

Spot market buys use quote-currency quantity; limit orders and market sells use base quantity. Apply symbol minimums, increments, and precision. Limit orders can fill immediately if their price crosses the book. Conditional orders have a distinct lifecycle and client identifier; do not treat them as ordinary resting orders.

Margin borrowing, repayment, and transfers change liabilities and collateral. Preserve source/destination account type, symbol, asset, quantity, and margin scope.

## USDT futures versions and position modes

`UsdtFuturesApi` has separate cross and isolated methods. Match leverage, margin mode, contract quantity, `Offset.Open`/`Close`, reduce-only behavior, and position mode to the intended exposure. Leverage and mode changes can persist and affect existing positions. Close-position endpoints can act on exposure that predates the sample order; verify scope before using them.

`UsdtFuturesV5Api` has its own account/position modes, order models, and Shared API capabilities. Do not copy parameters or prerequisites between futures versions. Futures sizes are contracts rather than spot base-asset amounts. Futures client-order IDs used through Shared API must be integer-compatible.

## Nested outcomes and submission recovery

Spot batch placement can return `HttpResult<CallResult<HTXBatchPlaceResult>[]>`; check outer success and every inner result/error. Batch cancellation reports `Successful` and `Failed` entries: inspect failed IDs and errors before reconciling or retrying individual items. Futures batch responses likewise require per-order inspection.

Cancellation can race with fills. Reconcile order/trade state instead of assuming accepted cancellation prevented execution. A timeout or lost response can occur after acceptance of any submission. Query supported order/client identifiers and relevant account/position/transaction state before deciding whether to resubmit. Transient errors do not make account-changing calls safe to repeat; bound public-read retries separately.

## Transfers and withdrawals

Transfers, margin loans, subaccount changes, and withdrawals can move funds or alter permissions. Preserve source/destination, asset, network, address, applicable tag, amount, and fees. Request acceptance is distinct from completed settlement. `HTXEnvironment.Live` is the built-in environment; custom endpoint addresses do not themselves provide simulated trading.
