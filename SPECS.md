# Spec-Driven Development Guide

This repository uses a **spec-first** workflow. Every feature begins as a specification file before any code is written.

## Where specs live

All feature specs are stored in the `specs/` directory:

```
specs/
├── <kebab-case-feature>.md        # Flat spec file (simple features)
└── <kebab-case-feature>/          # Folder layout (complex features)
    ├── spec.md                    # Feature specification
    └── plan.md                    # Implementation plan
```

### Filename conventions

- Use lowercase, hyphen-separated words derived from the feature name.
- Examples:
  - "Stock Price Alerts" → `specs/stock-price-alerts.md`
  - "User Authentication" → `specs/user-authentication.md`
  - "Portfolio Dashboard" → `specs/portfolio-dashboard.md`
- If you use issue/ticket numbers, prefix with the number: `specs/42-stock-price-alerts.md`

---

## How to request a spec or plan

### GitHub.com Copilot Chat

Direct file writes are not available in GitHub.com Chat. Copilot will output the spec as a ready-to-commit markdown block.

**To create a spec:**
1. Open Copilot Chat on GitHub.com.
2. Type: `@copilot Plan the <feature name> feature` or `Create a spec for <feature name>`.
3. Copilot will output a fenced file block containing the spec.
4. Copy the output and save it to `specs/<kebab-case-feature>.md` in your local clone, then commit and push.

You can also use the prompt file directly:
> Attach `.github/prompts/feature-spec.prompt.md` or type: `Use the feature-spec prompt to specify <feature name>.`

---

### GitHub.com Copilot Agent Chat (Coding Agent)

The coding agent can write files directly to the repository.

**To create a spec:**
1. Open a Copilot Agent session on GitHub.com.
2. Type: `Create a spec for <feature name>` or `Plan the <feature name> feature`.
3. The agent will create `specs/<kebab-case-feature>.md` directly in the repo.
4. Review the created file, then approve or request changes.

**To generate an implementation plan:**
1. After the spec is approved, type: `Create an implementation plan for <feature name> based on specs/<feature>.md`.
2. The agent will create `specs/<feature>/plan.md` (or output the plan for review).

You can invoke the Speckit commands directly:
- `/speckit.specify` — guided spec creation workflow
- `/speckit.plan` — guided plan creation from an existing spec

---

### VS Code Copilot Chat

In VS Code, Copilot Chat can create files directly in your workspace.

**To create a spec:**
1. Open Copilot Chat in VS Code (`Ctrl+Shift+I` / `Cmd+Shift+I`).
2. Run the prompt: type `@workspace /feature-spec <feature name>` or open `.github/prompts/feature-spec.prompt.md` and click **Run**.
3. Copilot will offer to create the file at `specs/<kebab-case-feature>.md` in your workspace.
4. Confirm the file creation, then commit it.

**To generate a plan:**
1. Open `.github/prompts/feature-plan.prompt.md` and click **Run**, or type `@workspace /feature-plan <feature name>`.
2. Copilot reads the existing spec and produces a step-by-step implementation plan.

You can also use the Speckit slash commands in VS Code chat:
- `/speckit.specify` — interactive spec creation
- `/speckit.plan` — implementation planning from a spec

---

## Expected outputs

| Prompt / Command | Output |
|---|---|
| `feature-spec` prompt or `/speckit.specify` | `specs/<feature>.md` — feature specification |
| `feature-plan` prompt or `/speckit.plan` | `specs/<feature>/plan.md` — implementation plan |
| `/speckit.tasks` | `specs/<feature>/tasks.md` — task breakdown |
| `/speckit.implement` | Code changes implementing the tasks |

---

## Spec lifecycle

```
Draft → In Review → Approved → [implementation] → Done
```

1. **Draft**: Initial spec created by Copilot or a developer. May have open questions.
2. **In Review**: Shared with team or reviewed against project constitution.
3. **Approved**: All open questions resolved. Implementation may begin.
4. **Done**: Feature implemented and tests passing. Spec is archived but not deleted.

Update the **Status** field in the spec file as it moves through the lifecycle.

---

## Updating or superseding specs

- To **update** an existing spec: edit the file directly and append a `## Change History` section at the bottom noting what changed and why.
- To **supersede** a spec (major rework): create a new spec file with a version suffix (e.g., `specs/stock-price-alerts-v2.md`) and add a note at the top of the old file pointing to the new one.
- **Never delete** a spec that has been implemented — it serves as a record of intent.

---

## Full Speckit workflow

For the complete guided workflow using Speckit slash commands:

```
/speckit.constitution  →  Review/amend project principles
/speckit.specify       →  Create feature spec
/speckit.plan          →  Create implementation plan
/speckit.tasks         →  Generate task list
/speckit.implement     →  Execute implementation
```

See `.specify/memory/constitution.md` for the project's governing principles.
