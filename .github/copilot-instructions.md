<!-- SPECKIT START -->
For additional context about technologies to be used, project structure,
shell commands, and other important information, read the current plan
<!-- SPECKIT END -->

## Spec-Driven Development Workflow

This repository follows a **spec-first** approach. Every new feature begins as a specification file before any code is written.

### When a user asks to "plan", "spec out", or "design" a new feature

**Always** follow these steps in order:

1. **Ask clarifying questions** (if the feature description is ambiguous or underspecified). Keep questions concise and targeted — ask up to 5 questions at once, then proceed.
2. **Create or update a spec file** at `.specify/specs/NNNN-kebab-case-name.md` using the template below.
3. **Use the spec as the source of truth** — all subsequent plans, tasks, and implementation must trace back to the spec.
4. **Acknowledge if a spec already exists** at that path, and offer to update it or create a new version rather than overwriting silently.

### Spec filename conventions

All speckit artifacts are stored under `.specify/` in type-specific subdirectories. Each feature is identified by a unique **`NNNN`** — a 4-digit zero-padded sequential integer (e.g., `0001`, `0042`) that is **never reused**.

| Artifact   | Path                                               |
|------------|----------------------------------------------------|
| Spec       | `.specify/specs/NNNN-kebab-case-name.md`           |
| Plan       | `.specify/plans/NNNN-kebab-case-name.md`           |
| Tasks      | `.specify/tasks/NNNN-kebab-case-name.md`           |
| Research   | `.specify/research/NNNN-kebab-case-name.md`        |
| Data Model | `.specify/data-models/NNNN-kebab-case-name.md`     |
| Quickstart | `.specify/quickstarts/NNNN-kebab-case-name.md`     |
| Contracts  | `.specify/contracts/NNNN-kebab-case-name/`         |

**Naming rules**:
- `NNNN` is determined by incrementing the highest existing number across all specs, git branches, and remote branches. It must be unique across the entire repo history.
- `kebab-case-name` uses lowercase, hyphen-separated meaningful words derived from the feature description with stop words removed (e.g., "create a new feature" → `create-new-feature`).
- Git branches use the same `NNNN-kebab-case-name` format — the branch name suffix and file name suffix are always identical.

**Examples**:
- "Stock price alerts" → `.specify/specs/0001-stock-price-alerts.md`
- "User authentication" → `.specify/specs/0002-user-authentication.md`
- "Portfolio dashboard" → `.specify/specs/0003-portfolio-dashboard.md`

### Spec file template

When creating a spec, always use this structure:

```markdown
# Feature Specification: <Feature Name>

**File**: `.specify/specs/<NNNN-kebab-case-name>.md`
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
- Follow it with instructions: "Save this as `.specify/specs/<NNNN-kebab-case-name>.md` in the repository, then commit it."
- Example file block opening: ` ```markdown name=.specify/specs/0001-my-feature.md `

**VS Code Copilot Chat**
- Offer to create the file directly in the workspace using the file creation capability.
- Confirm the filename with the user before writing.

**GitHub Copilot Agent / Coding Agent (automated context)**

Follow these steps **in order** every time a spec is created or updated:

1. **Run `create-new-feature.sh`** (or equivalent) to get the next `NNNN` and the spec file path.
2. **Create the spec file** at `.specify/specs/NNNN-kebab-case-name.md` on a new feature branch using the template above.
3. **Commit and push** the spec file to the feature branch with a commit message like `docs: add spec for <feature name>`.
4. **Open a Pull Request** (draft is fine) with:
   - Title: `Spec: <Feature Name>`
   - Description summarising the feature and listing the spec's acceptance criteria.
   - Label or note that this PR is awaiting spec review before planning begins.
5. **Request a review** — mention in the PR description: "Please review and approve this spec before planning and implementation proceed."
6. Report the spec file path and the PR URL in your response.

---

### When a user invokes `/speckit.plan`

`/speckit.plan` turns an approved spec into a full technical implementation plan. It **must** be invoked with a feature identifier.

**Accepted invocation forms**:
- `/speckit.plan 0001` — numeric prefix only
- `/speckit.plan 0001-remove-weather-sample-code` — full feature name

**Steps (GitHub Copilot Agent / Coding Agent)**:

1. **Extract the feature ID** from the user's input (e.g., `0001` or `0001-remove-weather-sample-code`).
2. **Run `setup-plan.sh`** to resolve paths and scaffold the plan file:
   ```bash
   bash .specify/scripts/bash/setup-plan.sh --json --feature <feature-id>
   ```
   Parse the JSON output — the keys you need are:
   - `FEATURE_SPEC` — absolute path to the spec file (e.g., `.specify/specs/0001-remove-weather-sample-code.md`)
   - `IMPL_PLAN` — absolute path where the plan file should be written (e.g., `.specify/plans/0001-remove-weather-sample-code.md`)
3. **Read the spec** at `FEATURE_SPEC`.
4. **Research the codebase** — explore relevant source files, dependencies, and patterns to understand what needs to change.
5. **Write the plan** to `IMPL_PLAN` (the template is already copied there by `setup-plan.sh`). Fill every section of the template:
   - Replace all `[PLACEHOLDER]` tokens with real values.
   - **Summary**: one paragraph from the spec's functional requirements.
   - **Technical Context**: language/version, dependencies, testing framework, etc., derived from the constitution and codebase.
   - **Constitution Check**: verify each constitution principle applies or is N/A.
   - **Project Structure**: the concrete directory tree for this feature's source files and tests.
   - If research warranted, also create:
     - `.specify/research/NNNN-kebab-case-name.md`
     - `.specify/data-models/NNNN-kebab-case-name.md` (if entities are involved)
     - `.specify/quickstarts/NNNN-kebab-case-name.md` (if a run/test guide is useful)
     - `.specify/contracts/NNNN-kebab-case-name/` (API contracts, if applicable)
6. **Commit and push** all created/modified files with a message like `docs: add plan for <feature-name>`.
7. Report the plan file path and a brief summary of key decisions in your response.

**Error handling**:
- If no feature ID is provided, ask the user: "Which feature would you like to plan? Please provide the feature number (e.g., `0001`) or full name."
- If `setup-plan.sh` exits non-zero (spec not found), report the error and stop.
- Do **not** create a plan if the spec file does not exist.

---

### When a user invokes `/speckit.tasks`

`/speckit.tasks` breaks an approved plan into an ordered, actionable task list.

**Accepted invocation forms**:
- `/speckit.tasks 0001`
- `/speckit.tasks 0001-remove-weather-sample-code`

**Steps (GitHub Copilot Agent / Coding Agent)**:

1. **Extract the feature ID** from the user's input (e.g., `0001` or `0001-remove-weather-sample-code`).
2. **Run `setup-tasks.sh`** to resolve paths and scaffold the tasks file:
   ```bash
   bash .specify/scripts/bash/setup-tasks.sh --json --feature <feature-id>
   ```
   Parse the JSON output — the keys you need are:
   - `FEATURE_SPEC` — absolute path to the spec file (e.g., `.specify/specs/0001-remove-weather-sample-code.md`)
   - `IMPL_PLAN` — absolute path to the plan file (e.g., `.specify/plans/0001-remove-weather-sample-code.md`)
   - `TASKS` — absolute path where the tasks file should be written (e.g., `.specify/tasks/0001-remove-weather-sample-code.md`)
3. **Read the spec and plan** files at `FEATURE_SPEC` and `IMPL_PLAN`. Also read any supporting artifacts (research, data-model, contracts) if they exist.
4. **Write the task list** to `TASKS` (the template is already copied there by `setup-tasks.sh`). Replace all sample tasks with real tasks:
   - Replace all sample tasks with real tasks derived from the spec's user stories and the plan's implementation phases.
   - Group tasks by user story; include phase headers.
   - Mark independent tasks with `[P]`.
   - Include exact file paths in each task description.
5. **Commit and push** the new tasks file with a message like `docs: add tasks for <feature-name>`.
6. **Open a Pull Request** (draft is fine) with:
   - Title: `Tasks: <Feature Name>`
   - Description summarising the feature and listing the generated tasks.
   - Note in the PR description that this is the task breakdown awaiting review before implementation begins.
7. Report the tasks file path, total task count, and PR URL in your response.

**Error handling**:
- If no feature ID is provided, ask the user: "Which feature would you like to generate tasks for? Please provide the feature number (e.g., `0001`) or full name."
- If `setup-tasks.sh` exits non-zero (spec or plan not found), report the error and stop.
- Do **not** create tasks if the plan file does not exist — instruct the user to run `/speckit.plan <id>` first.

---

### When a user invokes `/speckit.implement`

`/speckit.implement` executes the tasks in a tasks file, implementing the feature incrementally.

**Accepted invocation forms**:
- `/speckit.implement 0001`
- `/speckit.implement 0001-remove-weather-sample-code`

**Steps (GitHub Copilot Agent / Coding Agent)**:

1. **Extract the feature ID** from the user's input.
2. **Resolve artifact paths** (same pattern as `/speckit.tasks`):
   - Tasks: `.specify/tasks/<NNNN>-*.md`
   - Plan: `.specify/plans/<NNNN>-*.md`
   - Spec: `.specify/specs/<NNNN>-*.md`
3. **Read the tasks, plan, and spec** files in full before writing any code.
4. **Execute tasks in dependency order** as defined in the tasks file:
   - Tasks marked `[P]` with no shared file dependencies may be done together.
   - For each task: write code, run `dotnet build` and `dotnet test` to verify, commit.
   - Follow the constitution: tests written before implementation, no business logic in controllers, XML docs on public APIs.
   - Mark each completed task with `[x]` in the tasks file and commit the update.
5. **Commit frequently** — after each logical group of tasks — with messages like `feat: <task description>`.
6. When all tasks are complete, report a summary of changes made and confirm all tests pass.

**Error handling**:
- If no feature ID is provided, ask the user which feature to implement.
- If the tasks file is missing, stop and instruct the user to run `/speckit.tasks <id>` first.
- If build or tests fail, stop, report the failure, and wait for guidance before continuing.

---

### Keeping plans in sync with specs

- The spec at `.specify/specs/NNNN-kebab-case-name.md` is always the **source of truth**.
- All related artifacts (plan, tasks, research, etc.) use the **same `NNNN`** prefix so they can be linked.
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
