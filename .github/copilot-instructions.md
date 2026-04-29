<!-- SPECKIT START -->
For additional context about technologies to be used, project structure,
shell commands, and other important information, read the current plan
<!-- SPECKIT END -->

## Spec-Driven Development Workflow

This repository follows a **spec-first** approach. Every new feature begins as a specification file before any code is written.

### When a user asks to "plan", "spec out", or "design" a new feature

**Always** follow these steps in order:

1. **Ask clarifying questions** (if the feature description is ambiguous or underspecified). Keep questions concise and targeted — ask up to 5 questions at once, then proceed.
2. **Create or update a spec file** at `.specify/specs/NNNN-PascalCaseFeatureName.md` using the template below.
3. **Use the spec as the source of truth** — all subsequent plans, tasks, and implementation must trace back to the spec.
4. **Acknowledge if a spec already exists** at that path, and offer to update it or create a new version rather than overwriting silently.

### Spec filename conventions

All speckit artifacts are stored under `.specify/` in type-specific subdirectories. Each feature is identified by a unique **`NNNN`** — a 4-digit zero-padded sequential integer (e.g., `0001`, `0042`) that is **never reused**.

| Artifact   | Path                                              |
|------------|---------------------------------------------------|
| Spec       | `.specify/specs/NNNN-PascalCaseName.md`           |
| Plan       | `.specify/plans/NNNN-PascalCaseName.md`           |
| Tasks      | `.specify/tasks/NNNN-PascalCaseName.md`           |
| Research   | `.specify/research/NNNN-PascalCaseName.md`        |
| Data Model | `.specify/data-models/NNNN-PascalCaseName.md`     |
| Quickstart | `.specify/quickstarts/NNNN-PascalCaseName.md`     |
| Contracts  | `.specify/contracts/NNNN-PascalCaseName/`         |

**Naming rules**:
- `NNNN` is determined by incrementing the highest existing number across all specs, git branches, and remote branches. It must be unique across the entire repo history.
- `PascalCaseName` is derived from the feature description with stop words removed and each meaningful word capitalized (e.g., "create a new feature" → `CreateNewFeature`).
- Git branches use `NNNN-kebab-case-name` format (e.g., `0001-create-new-feature`).

**Examples**:
- "Stock price alerts" → `.specify/specs/0001-StockPriceAlerts.md`
- "User authentication" → `.specify/specs/0002-UserAuthentication.md`
- "Portfolio dashboard" → `.specify/specs/0003-PortfolioDashboard.md`

### Spec file template

When creating a spec, always use this structure:

```markdown
# Feature Specification: <Feature Name>

**File**: `.specify/specs/<NNNN-PascalCaseName>.md`
**Created**: <YYYY-MM-DD>
**Status**: Draft | In Review | Approved

## Overview

<One-paragraph description of the feature and its purpose.>

## User Stories

### Story 1 — <Short Title> (Priority: P1)

<Describe the user journey in plain language.>

**Acceptance Criteria**:
1. **Given** <initial state>, **When** <action>, **Then** <expected outcome>
2. **Given** <initial state>, **When** <action>, **Then** <expected outcome>

### Story 2 — <Short Title> (Priority: P2)

...

## Functional Requirements

- **FR-001**: System MUST <specific capability>
- **FR-002**: System MUST <specific capability>

## Out of Scope

- <Explicitly list what is NOT included in this feature>

## Success Criteria

- **SC-001**: <Measurable outcome>
- **SC-002**: <Measurable outcome>

## Assumptions

- <Key assumptions made when writing this spec>

## Open Questions

- <Unresolved questions that need answers before or during implementation>
```

### Behavior by Copilot surface

**GitHub.com Copilot Chat (no direct file writes)**
- Output the full spec as a fenced markdown file block labelled with the target path.
- Follow it with instructions: "Save this as `.specify/specs/<NNNN-PascalCaseName>.md` in the repository, then commit it."
- Example file block opening: ` ```markdown name=.specify/specs/0001-MyFeature.md `

**VS Code Copilot Chat**
- Offer to create the file directly in the workspace using the file creation capability.
- Confirm the filename with the user before writing.

**GitHub Copilot Agent / Coding Agent (automated context)**

Follow these steps **in order** every time a spec is created or updated:

1. **Create a feature branch** named `spec/<kebab-case-feature-name>` (e.g., `spec/stock-price-alerts`).
2. **Create the spec file** at `specs/<kebab-case-feature>.md` on that branch using the template below.
3. **Commit and push** the spec file to the feature branch with a commit message like `docs: add spec for <feature name>`.
4. **Open a Pull Request** (draft is fine) with:
   - Title: `Spec: <Feature Name>`
   - Description summarising the feature and listing the spec's acceptance criteria.
   - Label or note that this PR is awaiting spec review before planning begins.
5. **Request a review** — mention in the PR description: "Please review and approve this spec before planning and implementation proceed."
6. Report the spec file path and the PR URL in your response.

### Keeping plans in sync with specs

- The spec at `specs/<feature>.md` is always the **source of truth**.
- If a plan, task list, or implementation diverges from the spec, update the spec first, then the plan.
- When updating an existing spec, preserve the original content and append a `## Change History` section at the bottom noting what changed and why.

### Speckit slash commands

Use these commands for the full spec-driven workflow:

| Command | Purpose |
|---|---|
| `/speckit.specify` | Create or update a feature spec |
| `/speckit.plan` | Turn a spec into a technical implementation plan |
| `/speckit.tasks` | Break a plan into actionable tasks |
| `/speckit.implement` | Execute implementation tasks |
| `/speckit.constitution` | Review or amend project principles |

See `.specify/memory/constitution.md` for governing project principles.
