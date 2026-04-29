# Feature Specification: Remove Weather Sample Code (API + Web)

**File**: `specs/remove-weather-sample-code.md`  
**Created**: 2026-04-29  
**Status**: Draft  

## Overview

This change removes all scaffolded "weather sample" code from the repository, across both the backend API project and the Blazor web project. This includes deleting weather-specific endpoints/routes, UI pages/components, navigation links, DTOs/models, services, and any tests/scripts that exist solely to support weather sample behavior.

After this change:
- No weather endpoints exist in the API.
- No weather pages/links/mentions exist in the web UI.
- The repository continues to build and all tests/scripts pass (with weather-specific tests removed).

## User Stories

### Story 1 — Remove weather endpoints from the API (Priority: P1)

As a developer/user of the API, I do not want any weather sample endpoints exposed, so the API surface reflects only the stock-analyst domain.

**Acceptance Criteria**:
1. **Given** the API project is built and running, **When** I inspect available routes/endpoints, **Then** no weather sample endpoints exist (e.g., `WeatherForecast`, `/weatherforecast`, or equivalent).
2. **Given** the repository, **When** I search for weather sample identifiers in API code, **Then** no weather-specific controllers/minimal-api route mappings/models remain.

### Story 2 — Remove weather UI components from the web app (Priority: P1)

As a user of the web application, I do not want to see any weather-related pages, navigation links, or UI mentions.

**Acceptance Criteria**:
1. **Given** the web project is built and running, **When** I view the application navigation and pages, **Then** there are no weather-related links, pages, labels, or components.
2. **Given** the repository, **When** I search for weather sample identifiers in the web project, **Then** no weather-specific components/pages/routes remain.

### Story 3 — Remove weather-specific code artifacts entirely (Priority: P1)

As a maintainer, I want all weather sample code deleted entirely (not merely disabled), so the codebase stays clean and focused.

**Acceptance Criteria**:
1. **Given** the repository, **When** I search for common weather sample identifiers, **Then** there are zero matches in source code:
   - `WeatherForecast`, `weatherforecast`, `Weather`, `Forecast` (as used in the sample context),
   - and any weather-sample-specific namespaces/types/routes.
2. **Given** the repository, **When** I look at project files and references, **Then** there are no remaining compile-time references to weather sample files/types.

### Story 4 — Keep builds and tests green after removal (Priority: P1)

As a maintainer, I want build scripts and tests to pass after removing weather code, so contributors are not blocked.

**Acceptance Criteria**:
1. **Given** the standard build script(s) used by this repository, **When** they run, **Then** they pass without requiring any weather sample code.
2. **Given** the test suite(s), **When** they run, **Then** they pass; any tests that existed solely for weather sample behavior are removed.
3. **Given** CI workflows (if present), **When** they run, **Then** they pass with weather sample tests removed and any references updated.

## Functional Requirements

- **FR-001**: The system MUST delete weather sample API code, including any controllers/endpoints, route mappings, and DTOs/models that exist solely for the weather sample.
- **FR-002**: The system MUST delete weather sample web UI code, including any pages/components and navigation entries referencing the weather sample.
- **FR-003**: The system MUST remove any DI registrations, configuration, or wiring that exists only for weather sample behavior.
- **FR-004**: The system MUST remove any weather-specific tests and any scripts used solely to test weather sample behavior.
- **FR-005**: The system MUST keep the repository building successfully and all remaining tests passing.

## Non-Functional Requirements

- **NFR-001**: No new languages/runtimes are introduced (must remain within the existing .NET + Blazor approach).
- **NFR-002**: The change should be minimal and focused (no unrelated refactors beyond what is necessary to remove weather sample coupling).

## Out of Scope

- Adding or redesigning stock-analyst domain functionality.
- Introducing new APIs, pages, or features unrelated to removing weather sample code.
- Large-scale refactoring unrelated to eliminating weather sample dependencies.

## Success Criteria

- **SC-001**: `dotnet build` (or the repository's build script) succeeds for all relevant projects.
- **SC-002**: Test execution succeeds; no weather-only tests remain.
- **SC-003**: No weather sample endpoints/routes exist at runtime.
- **SC-004**: No weather sample UI pages/mentions exist at runtime.
- **SC-005**: A repo-wide search shows no remaining weather sample identifiers in source code (excluding this spec and any explicit change-history documentation about the removal).

## Assumptions

- The repository includes default template/sample "weather" code typical of ASP.NET scaffolding.
- Removing weather sample code may require updating navigation/menu definitions, routing configuration, and possibly test harnesses or scripts.

## Open Questions

- None for scope. (Implementation will discover the exact files/paths to delete and update accordingly.)

## Change History

- 2026-04-29: Initial draft created. Scope confirmed: remove weather sample code from both API and web projects, delete entirely, remove all UI mentions/routes, and keep builds/tests/scripts passing.
