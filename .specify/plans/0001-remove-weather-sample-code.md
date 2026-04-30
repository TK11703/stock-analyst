# Implementation Plan: Remove Weather Sample Code

**Spec**: `.specify/specs/0001-remove-weather-sample-code.md`  
**Created**: 2026-04-29  
**Status**: Draft

## Goal

Remove all ASP.NET template "weather" sample functionality from both the API and the web app, delete associated artifacts entirely, and keep builds/tests green.

This plan implements:

- User Story 1–4
- FR-001 through FR-005
- SC-001 through SC-004

---

## Milestone 1 — Identify all weather sample artifacts (Story 3, FR-001/2/3/4)

### Steps
1. Perform a repo-wide search for common template identifiers:
   - `WeatherForecast`
   - `weatherforecast`
   - `Weather`
   - `Forecast` (carefully; avoid false positives)
2. Enumerate the hits into buckets:
   - API endpoints/controllers/minimal API route mappings
   - DTOs/models (e.g., `WeatherForecast`)
   - Services/DI registrations/config
   - Web UI pages/components/nav links
   - Tests
   - Scripts/docs

### Deliverables
- A short checklist (in the PR description or tasks doc) of *every* file to delete/update.

### Exit Criteria
- You can point to every location where weather sample behavior is defined or referenced.

---

## Milestone 2 — Remove weather API surface (Story 1, FR-001/FR-003, SC-004)

### Steps
1. Delete weather API endpoint implementation(s):
   - If MVC: remove `WeatherForecastController` (or equivalent)
   - If minimal API: remove `MapGet("/weatherforecast", ...)` (or equivalent)
2. Delete API models/DTOs used only for the sample (e.g., `WeatherForecast`).
3. Remove any DI/service wiring that exists only for weather sample behavior.
4. Ensure there are no remaining route registrations for weather endpoints.

### Verification
- Run the API and confirm no `/weatherforecast` (or similar) route responds.
- Repo-wide search: no weather endpoint identifiers remain (excluding spec/changelog docs if any).

### Exit Criteria
- Story 1 acceptance scenarios satisfied.
- SC-004 (API portion): no weather endpoints exist.

---

## Milestone 3 — Remove weather UI surface (Story 2, FR-002/FR-003, SC-004)

### Steps
1. Delete Blazor components/pages that implement weather UI.
2. Remove navigation/menu entries linking to weather pages (e.g., `NavMenu` / `NavLink` items).
3. Remove any client-side service wiring/config that only supports the weather sample.

### Verification
- Run the web app and confirm:
  - No weather-related pages are reachable
  - No navigation items mention weather
- Repo-wide search in the web project for weather identifiers returns zero matches (excluding spec/changelog docs if any).

### Exit Criteria
- Story 2 acceptance scenarios satisfied.
- SC-004 (UI portion): no weather UI elements exist.

---

## Milestone 4 — Remove residual references + keep build/test green (Story 3 & 4, FR-005, SC-001/2/3)

### Steps
1. Remove any remaining compile-time references:
   - Project file includes (`.csproj`)
   - `using` statements, namespace references
   - Any leftover code comments referencing the sample (optional, but recommended)
2. Remove tests that only validate weather sample behavior.
3. Update CI/build scripts if they reference weather sample endpoints or test data.
4. Run:
   - `dotnet build`
   - `dotnet test`

### Verification
- SC-001: `dotnet build` succeeds for the whole solution.
- SC-002: `dotnet test` passes; no weather-only tests remain.
- SC-003: repo-wide search for `WeatherForecast`, `weatherforecast`, `Weather`, and `Forecast` (sample context) returns zero matches in source code (excluding this plan/spec).

### Exit Criteria
- Story 4 acceptance scenarios satisfied.
- All FRs satisfied.
- All SCs satisfied.

---

## Risk / Edge Case Handling (from Spec "Edge Cases")

- **False positives**: If "weather" appears in unrelated contexts, do not remove it unless it's clearly template/sample related.
- **Conditional UI**: Delete weather pages/links entirely; don't just hide them.
- **Startup/DI failures**: After removal, fix any service registration/config dependencies introduced by template wiring.

---

## Suggested PR Structure

1. Commit 1: "chore: remove weather API sample"
2. Commit 2: "chore: remove weather UI sample"
3. Commit 3: "chore: clean references and keep build green"

(Or squash into one commit if preferred; the important part is traceability to the spec.)

---

## Definition of Done

- All weather sample artifacts deleted (not disabled/commented out).
- No weather endpoints/UI remain.
- `dotnet build` and `dotnet test` pass.
- Repo-wide search confirms removal of weather sample identifiers (excluding `.specify/` docs).
