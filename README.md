# Stock Analyst

A spec-driven stock analysis application built with .NET 10 (C#) and JavaScript, using the [GitHub Spec Kit](https://github.com/github/spec-kit) methodology.

## Project Structure

```
stock-analyst/
├── src/
│   ├── StockAnalyst.Core/       # C# business logic & domain models
│   └── StockAnalyst.Api/        # ASP.NET Core 10 Web API
├── tests/
│   ├── StockAnalyst.Core.Tests/ # xUnit unit tests for Core
│   └── StockAnalyst.Api.Tests/  # xUnit integration tests for API
├── client/
│   ├── src/                     # JavaScript source
│   └── tests/                   # Vitest tests
├── .specify/                    # Spec Kit configuration & memory
│   └── memory/constitution.md  # Project principles (start here)
└── .github/prompts/             # Copilot slash commands
```

## Getting Started

### Prerequisites
- [.NET 10 SDK](https://dotnet.microsoft.com/)
- [Node.js 20+](https://nodejs.org/)

### Build & Test (.NET)
```bash
dotnet build
dotnet test
```

### Build & Test (JavaScript)
```bash
cd client
npm install
npm test
```

## Spec-Driven Development Workflow

This project uses [GitHub Spec Kit](https://github.com/github/spec-kit). Use these Copilot slash commands in order:

1. `/speckit.constitution` — Review or amend project principles
2. `/speckit.specify` — Describe what you want to build
3. `/speckit.plan` — Define the technical approach
4. `/speckit.tasks` — Break the plan into actionable tasks
5. `/speckit.implement` — Execute implementation

See `.specify/memory/constitution.md` for the project's governing principles.

