---
mode: ask
description: Generate a feature spec file at specs/<kebab-case-feature>.md using the project spec template.
---

You are helping create a new feature specification for the Stock Analyst project.

## Instructions

1. If the feature description is vague or underspecified, ask up to 5 targeted clarifying questions before proceeding.
2. Derive a kebab-case filename from the feature name (e.g., "Stock Price Alerts" → `stock-price-alerts`).
3. Create or propose a spec file at `specs/<kebab-case-feature>.md` using the template below.
4. If a spec already exists at that path, note the conflict and offer to update it or create a versioned file.

## Spec template

Produce the file with this exact structure (fill in all placeholders):

```markdown
# Feature Specification: <Feature Name>

**File**: `specs/<kebab-case-feature>.md`
**Created**: <YYYY-MM-DD>
**Status**: Draft

## Overview

<One-paragraph description of the feature and its purpose in the Stock Analyst application.>

## User Stories

### Story 1 — <Short Title> (Priority: P1)

<Describe the user journey in plain language.>

**Acceptance Criteria**:
1. **Given** <initial state>, **When** <action>, **Then** <expected outcome>
2. **Given** <initial state>, **When** <action>, **Then** <expected outcome>

### Story 2 — <Short Title> (Priority: P2)

<Describe the user journey in plain language.>

**Acceptance Criteria**:
1. **Given** <initial state>, **When** <action>, **Then** <expected outcome>

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

## Output format by surface

**GitHub.com Chat**: Output the completed spec as a fenced markdown block with the filename label (` ```markdown name=specs/<feature>.md `), then instruct the user to save it to that path and commit it.

**VS Code Chat**: Offer to write the file directly to `specs/<kebab-case-feature>.md` in the workspace.

**Agent / Coding Agent**: Create the file at `specs/<kebab-case-feature>.md` directly, then report the file path.

## Implementation checklist

After producing the spec, output a short checklist:

- [ ] Spec saved to `specs/<kebab-case-feature>.md`
- [ ] User stories cover at least one P1 scenario
- [ ] All functional requirements are listed
- [ ] Success criteria are measurable
- [ ] Open questions are noted for follow-up

## Feature to specify

$ARGUMENTS
