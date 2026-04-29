# Feature Specification: Remove Weather Sample Code

**Feature Branch**: `0001-remove-weather-sample-code`
**Created**: 2026-04-29
**Status**: Draft

## User Scenarios & Testing *(mandatory)*

### User Story 1 - Remove Weather Endpoints from the API (Priority: P1)

As a developer or consumer of the API, I do not want any weather sample endpoints exposed, so the API surface reflects only the stock-analyst domain.

**Why this priority**: Removing sample endpoints is the most fundamental step — shipping a weather API in a stock-analyst product is misleading and potentially confusing for any downstream integration.

**Independent Test**: Can be fully tested by building and running the API and confirming that no `/weatherforecast` or equivalent route responds, and by searching the API source for weather sample identifiers — delivers a clean API surface without touching the UI.

**Acceptance Scenarios**:

1. **Given** the API project is built and running, **When** I inspect available routes/endpoints, **Then** no weather sample endpoints exist (e.g., `WeatherForecast`, `/weatherforecast`, or equivalent).
2. **Given** the repository, **When** I search for weather sample identifiers in API code, **Then** no weather-specific controllers, minimal-API route mappings, or models remain.

---

### User Story 2 - Remove Weather UI Components from the Web App (Priority: P1)

As a user of the web application, I do not want to see any weather-related pages, navigation links, or UI mentions.

**Why this priority**: The UI is user-facing; any remaining weather link or page directly degrades the user experience and misrepresents the application's purpose.

**Independent Test**: Can be fully tested by building and launching the Blazor app, browsing all navigation items, and confirming no weather pages or links are reachable — delivers a clean UI independently of backend work.

**Acceptance Scenarios**:

1. **Given** the web project is built and running, **When** I view the application navigation and all pages, **Then** there are no weather-related links, pages, labels, or components.
2. **Given** the repository, **When** I search for weather sample identifiers in the web project, **Then** no weather-specific Blazor components, pages, or routes remain.

---

### User Story 3 - Delete All Weather Code Artifacts Entirely (Priority: P1)

As a maintainer, I want all weather sample code deleted entirely (not merely disabled), so the codebase stays clean and focused.

**Why this priority**: Commented-out or disabled code still creates noise, confusion, and potential for reintroduction. Complete deletion is necessary for a clean, maintainable codebase.

**Independent Test**: Can be fully tested by running a repo-wide search for common weather sample identifiers after the change — delivers a provably clean codebase.

**Acceptance Scenarios**:

1. **Given** the repository, **When** I search for common weather sample identifiers (`WeatherForecast`, `weatherforecast`, `Weather`, `Forecast` in the sample context), **Then** there are zero matches in source code.
2. **Given** the repository, **When** I look at project files and references, **Then** there are no remaining compile-time references to weather sample files or types.

---

### User Story 4 - Keep Builds and Tests Green After Removal (Priority: P1)

As a maintainer, I want build scripts and tests to continue passing after removing weather code, so contributors are not blocked.

**Why this priority**: A broken build blocks all future development; this criterion validates the other three stories together.

**Independent Test**: Can be fully tested by running `dotnet build` and `dotnet test` against the repository after all weather code is removed — delivers confidence that the clean-up did not break anything.

**Acceptance Scenarios**:

1. **Given** the repository's standard build command (`dotnet build`), **When** it runs after all weather code is removed, **Then** it passes without errors.
2. **Given** the test suite (`dotnet test`), **When** it runs, **Then** all tests pass; any tests that existed solely for weather sample behavior are also removed.
3. **Given** CI workflows (if present), **When** they run, **Then** they pass with weather sample tests removed and any references updated.

---

### Edge Cases

- What if a weather identifier also appears in non-weather code (e.g., a comment or unrelated variable)? Only weather sample artefacts should be removed; any coincidental use of the word must be evaluated individually.
- What if a Blazor route or NavLink references weather but is conditionally rendered? It must still be deleted, not just hidden.
- What if removing a DI registration causes a startup exception? All service wiring for weather-only functionality must be cleaned up as part of this change.

## Requirements *(mandatory)*

### Functional Requirements

- **FR-001**: The system MUST delete weather sample API code, including any controllers/endpoints, route mappings, and DTOs/models that exist solely for the weather sample.
- **FR-002**: The system MUST delete weather sample web UI code, including any Blazor pages/components and navigation entries referencing the weather sample.
- **FR-003**: The system MUST remove any DI registrations, configuration, or service wiring that exists only for weather sample behaviour.
- **FR-004**: The system MUST remove any weather-specific tests and any scripts used solely to test weather sample behaviour.
- **FR-005**: The system MUST keep the repository building successfully (`dotnet build`) and all remaining tests passing (`dotnet test`).

## Success Criteria *(mandatory)*

### Measurable Outcomes

- **SC-001**: `dotnet build` succeeds for all projects in the solution with no errors or warnings attributable to weather removal.
- **SC-002**: `dotnet test` passes with zero failures; no weather-only tests remain in the suite.
- **SC-003**: A repo-wide search for `WeatherForecast`, `weatherforecast`, `Weather`, and `Forecast` (in sample context) returns zero matches in source code (excluding this spec and explicit change-history documentation).
- **SC-004**: The running application exposes no weather endpoints and shows no weather UI elements.

## Assumptions

- The repository includes default template/sample "weather" code typical of ASP.NET scaffolding (controllers, models, Blazor pages, NavLinks).
- Removing weather sample code may require updating navigation/menu definitions, routing configuration, and possibly test harnesses or scripts.
- No non-weather functionality depends on weather sample types; removal will not break unrelated features.
- The project targets `.NET 10` and uses `xUnit` for testing, as defined in the constitution.

## Change History

- **2026-04-29**: Initial draft created in `specs/remove-weather-sample-code.md`.
- **2026-04-29**: Adapted to speckit v1.2.0 format and moved to `.specify/specs/0001-remove-weather-sample-code.md`. Added "Why this priority", "Independent Test", and "Edge Cases" sections. Renamed "Acceptance Criteria" to "Acceptance Scenarios" and "Success Criteria" to "Measurable Outcomes". Removed redundant Non-Functional Requirements section (covered by constitution).
