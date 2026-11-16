# Working on HTX.Net

For library development, use this file. For code that consumes the library, read the [HTX.Net skill](.claude/skills/htx-net/SKILL.md). [llms.txt](llms.txt) indexes the remaining documentation.

## Repository layout

- [Client interfaces](HTX.Net/Interfaces/Clients/): public endpoint signatures and XML documentation.
- [Clients](HTX.Net/Clients/): REST, WebSocket, and Shared API implementations, grouped by API and transport.
- [Models](HTX.Net/Objects/Models/), [options](HTX.Net/Objects/Options/), and [enums](HTX.Net/Enums/): wire models and configuration.
- [Unit tests](HTX.Net.UnitTests/): request/response fixtures, subscription validation, and example compilation.
- [AI-friendly examples](Examples/ai-friendly/): complete consumer programs.

## Build and validation

Run commands from the repository root. Use the SDK configured in the [build workflow](.github/workflows/dotnet.yml); targets and dependencies are defined in the [library project](HTX.Net/HTX.Net.csproj).

```sh
dotnet restore HTX.Net.sln
dotnet build HTX.Net.sln --no-restore
dotnet test HTX.Net.UnitTests/HTX.Net.UnitTests.csproj --no-build --filter "FullyQualifiedName!~IntegrationTests"
```

Debug builds select a subset of frameworks. To check every supported target:

```sh
dotnet build HTX.Net/HTX.Net.csproj -p:BuildAllFrameworks=true
```

Run focused tests for the change. For edits to consumer examples:

```sh
dotnet test HTX.Net.UnitTests/HTX.Net.UnitTests.csproj --filter FullyQualifiedName~AiExampleCompileTests
```

That test compiles the examples; it does not execute their exchange operations or validate Markdown snippets. The normal test command above excludes integration fixtures. Native REST and socket integration fixtures have `Run = false`; Shared REST integration can be enabled with `INTEGRATION=1`. Inspect integration fixtures before running the unfiltered CI test command; they can contact the exchange. Do not enable authenticated exchange calls as part of routine validation.

## Implementation conventions

- Use the matching client interface and a neighboring endpoint implementation as the starting point. Preserve endpoint metadata, serialization, signing, rate limits, and cancellation behavior.
- Update public interfaces and XML documentation together with implementations. Add meaningful request/response or subscription coverage when behavior changes.
- Keep Shared API adapters consistent with native endpoint behavior; verify capability support and trading modes against the implemented surface.
- Preserve serialization compatibility and source-generated JSON metadata when changing models.
- Keep changes compatible with every target in the library project; gate framework-specific APIs where required.

## AI documentation ownership

- [SKILL.md](.claude/skills/htx-net/SKILL.md) owns consumer conventions and routes to detailed skill references.
- [API surfaces](.claude/skills/htx-net/references/api-surfaces.md) owns endpoint routing; [usage](.claude/skills/htx-net/references/usage.md) owns snippets.
- Link to those files instead of repeating API guides here or in tool-specific instructions.
- Keep skill links relative. References outside the skill are checkout-only; packaged guidance must remain usable without repository sources.
- The project packs [.claude/skills](.claude/skills/) into `skills/` in NuGet. After changing its layout, inspect package entries and verify referenced resources are included.
