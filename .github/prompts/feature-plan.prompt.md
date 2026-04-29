---
mode: ask
description: Turn an approved feature spec into a step-by-step technical implementation plan.
---

You are helping create a technical implementation plan for a feature in the Stock Analyst project.

## Project stack (do not deviate without a spec amendment)

- **Backend**: .NET 10, C#, ASP.NET Core Web API (`src/StockAnalyst.Api`), Core logic (`src/StockAnalyst.Core`)
- **Frontend**: ASP.NET Core Blazor (`src/StockAnalyst.Web`)
- **Tests**: xUnit (`tests/`), one test project per source project
- **No** JavaScript frameworks or runtimes

## Instructions

1. Locate the feature spec at `specs/<kebab-case-feature>.md`. If it does not exist, stop and ask the user to create the spec first using the `feature-spec` prompt.
2. Read the spec thoroughly before producing the plan.
3. Produce a numbered, step-by-step implementation plan that traces every step back to a spec requirement (FR-xxx or user story).
4. Flag any open questions from the spec that must be resolved before or during implementation.

## Plan template

Produce the plan with this structure:

```markdown
# Implementation Plan: <Feature Name>

**Spec**: `specs/<kebab-case-feature>.md`
**Created**: <YYYY-MM-DD>
**Status**: Draft

## Summary

<Two to three sentences describing what will be built and the high-level approach.>

## Prerequisites

- Approved spec at `specs/<kebab-case-feature>.md`
- All open questions in the spec resolved
- <Any other prerequisite>

## Implementation Steps

### Phase 1: <Phase Name> (e.g., Domain / Core Logic)

- [ ] **Step 1.1** — <Action> *(traces to FR-001, Story 1)*
  - Files: `src/StockAnalyst.Core/<Path>`
  - Notes: <Any relevant notes>
- [ ] **Step 1.2** — <Action> *(traces to FR-002)*
  - Files: `src/StockAnalyst.Core/<Path>`

### Phase 2: <Phase Name> (e.g., API Layer)

- [ ] **Step 2.1** — <Action> *(traces to FR-003)*
  - Files: `src/StockAnalyst.Api/<Path>`
- [ ] **Step 2.2** — <Action>
  - Files: `src/StockAnalyst.Api/<Path>`

### Phase 3: <Phase Name> (e.g., Frontend / Blazor)

- [ ] **Step 3.1** — <Action> *(traces to Story 2)*
  - Files: `src/StockAnalyst.Web/<Path>`

### Phase 4: Tests

- [ ] **Step 4.1** — Write unit tests for <component> *(covers FR-001, FR-002)*
  - Files: `tests/StockAnalyst.Core.Tests/<Path>`
- [ ] **Step 4.2** — Write integration tests for <endpoint>
  - Files: `tests/StockAnalyst.Api.Tests/<Path>`

## Open Questions (from spec)

<List any unresolved open questions from the spec that may affect implementation choices.>

## Out of Scope

<Confirm what the spec explicitly excluded, so it is not accidentally implemented.>
```

## Output format by surface

**GitHub.com Chat**: Output the plan as a fenced markdown block, then instruct the user to save it to `specs/<feature>/plan.md` and commit it.

**VS Code Chat**: Offer to write the plan directly to `specs/<feature>/plan.md` in the workspace.

**Agent / Coding Agent**: Create the plan file at `specs/<feature>/plan.md` directly, then report the file path and a summary of phases.

## Feature or spec to plan

$ARGUMENTS
