<!-- SPECKIT START -->
For additional context about technologies to be used, project structure,
shell commands, and other important information, read the current plan
<!-- SPECKIT END -->

## Spec-Driven Development Workflow

This repository follows a **spec-first** approach. Every new feature begins as a specification file before any code is written.

### When a user asks to "plan", "spec out", or "design" a new feature

**Always** follow these steps in order:

1. **Ask clarifying questions** (if the feature description is ambiguous or underspecified). Keep questions concise and targeted — ask up to 5 questions at once, then proceed.
2. **Create or update a spec file** at `specs/<kebab-case-feature-name>.md` using the template below.
3. **Use the spec as the source of truth** — all subsequent plans, tasks, and implementation must trace back to the spec.
4. **Acknowledge if a spec already exists** at that path, and offer to update it or create a new version rather than overwriting silently.

### Spec filename conventions

- Path: `specs/<kebab-case-feature>.md`
- Use lowercase, hyphen-separated words derived from the feature name.
- Examples:
  - "Stock price alerts" → `specs/stock-price-alerts.md`
  - "User authentication" → `specs/user-authentication.md`
  - "Portfolio dashboard" → `specs/portfolio-dashboard.md`
- If the user provides a ticket/issue number prefix, include it: `specs/42-stock-price-alerts.md`

### Spec file template

When creating a spec, always use this structure:

```markdown
# Feature Specification: <Feature Name>

**File**: `specs/<kebab-case-feature>.md`
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
- Follow it with instructions: "Save this as `specs/<kebab-case-feature>.md` in the repository, then commit it."
- Example file block opening: ` ```markdown name=specs/my-feature.md `

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
