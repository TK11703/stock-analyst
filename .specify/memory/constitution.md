# Stock Analyst Constitution

## Core Principles

### I. Spec-First (NON-NEGOTIABLE)
All features begin as a specification using `/speckit.specify`. No implementation begins without an approved spec. Specifications are the source of truth — code is their expression. Use `/speckit.plan` to define the technical approach before writing any code.

### II. Test-Driven Development (NON-NEGOTIABLE)
Tests are written from the spec before implementation. Follow Red-Green-Refactor strictly:
- C# (.NET 10): xUnit with tests in `tests/` mirroring `src/` structure.
All tests must pass before any feature branch is merged.

### III. Technology Stack
- **Backend**: .NET 10, C# — ASP.NET Core Web API (`src/StockAnalyst.Api`), Core business logic (`src/StockAnalyst.Core`).
- **Frontend**: ASP.NET Core Blazor — Blazor components in `src/StockAnalyst.Web/`.
- Do not introduce new languages or runtimes without amending this constitution.

### IV. Code Quality
- Keep interfaces small and focused (interface segregation).
- No business logic in API controllers — delegate to services in `StockAnalyst.Core`.
- All public APIs (C#) must have XML documentation.

### V. Simplicity (YAGNI)
Start with the simplest design that satisfies the spec. Avoid over-engineering. Complexity must be justified by a spec requirement.

## Technology Standards

### .NET / C#
- Target: `net10.0`
- Test framework: xUnit
- Dependency injection via `Microsoft.Extensions.DependencyInjection`
- No third-party ORM until a spec requires persistent storage

### Blazor / Frontend
- Framework: ASP.NET Core Blazor (`src/StockAnalyst.Web/`)
- No JavaScript frameworks or runtimes

## Development Workflow

1. Run `/speckit.constitution` to review/amend principles.
2. Run `/speckit.specify` to describe the feature.
3. Run `/speckit.plan` with stack and architecture decisions.
4. Run `/speckit.tasks` to generate actionable tasks.
5. Write tests (failing) before implementation.
6. Implement until all tests pass.
7. Run `/speckit.implement` with AI agent assistance when appropriate.

## Governance
This constitution supersedes all other practices. Amendments require an updated spec entry and team review. All PRs must verify compliance with these principles.

**Version**: 1.0.0 | **Ratified**: 2026-04-28 | **Last Amended**: 2026-04-28

