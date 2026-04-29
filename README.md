# Stock Analyst

A spec-driven stock analysis application built with .NET 10 (C#) and ASP.NET Core Blazor, using the [GitHub Spec Kit](https://github.com/github/spec-kit) methodology.

## Project Structure

```
stock-analyst/
├── src/
│   ├── StockAnalyst.Core/       # C# business logic & domain models
│   ├── StockAnalyst.Api/        # ASP.NET Core 10 Web API
│   └── StockAnalyst.Web/        # ASP.NET Core Blazor frontend
├── tests/
│   ├── StockAnalyst.Core.Tests/ # xUnit unit tests for Core
│   └── StockAnalyst.Api.Tests/  # xUnit integration tests for API
├── .specify/                    # Spec Kit configuration & memory
│   └── memory/constitution.md  # Project principles (start here)
└── .github/prompts/             # Copilot slash commands
```

## Getting Started

### Prerequisites
- [.NET 10 SDK](https://dotnet.microsoft.com/)

### Build & Test (.NET)
```bash
dotnet build
dotnet test
```

## Spec-Driven Development Workflow

This project uses [GitHub Spec Kit](https://github.com/github/spec-kit). All features start as a spec file in `specs/` before any code is written.

Quick start — Copilot slash commands (in order):

1. `/speckit.constitution` — Review or amend project principles
2. `/speckit.specify` — Describe what you want to build (creates `specs/<feature>.md`)
3. `/speckit.plan` — Define the technical approach
4. `/speckit.tasks` — Break the plan into actionable tasks
5. `/speckit.implement` — Execute implementation

You can also use the dedicated prompt files directly:
- `.github/prompts/feature-spec.prompt.md` — generate a spec for any feature
- `.github/prompts/feature-plan.prompt.md` — turn a spec into an implementation plan

See **[SPECS.md](SPECS.md)** for detailed instructions on using specs across GitHub.com Chat, Agent chat, and VS Code.

See `.specify/memory/constitution.md` for the project's governing principles.

