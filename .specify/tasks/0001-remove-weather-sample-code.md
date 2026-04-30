---
description: "Actionable task list for feature 0001-remove-weather-sample-code (with exact paths)"
---

# Tasks: Remove Weather Sample Code

**Feature**: `0001-remove-weather-sample-code`

**Inputs**:
- Spec: `.specify/specs/0001-remove-weather-sample-code.md`
- Plan: `.specify/plans/0001-remove-weather-sample-code.md`

**Goal**: Remove all ASP.NET template “weather forecast” sample artifacts from API + Blazor UI, clean up wiring, and keep `dotnet build` / `dotnet test` green.

## Format
`[ID] [P?] [US#] Description`
- **[P]**: can run in parallel (different files / low merge conflict risk)
- **US#**: user story from the spec (US1–US4)

---

## User Story 1 (P1): Remove Weather Endpoints from the API

**Independent Test**: Build and run the API; confirm no `/weatherforecast` endpoint exists.

- [ ] T001 [US1] Remove minimal API mapping for weather forecast from `src/StockAnalyst.Api/Program.cs` (delete the `app.MapGet("/weatherforecast", ...)` block).
- [ ] T002 [US1] Remove the `WeatherForecast` record/type from `src/StockAnalyst.Api/Program.cs`.
- [ ] T003 [US1] Remove the sample `summaries` array (only used by the weather endpoint) from `src/StockAnalyst.Api/Program.cs`.

---

## User Story 2 (P1): Remove Weather UI Components from the Web App

**Independent Test**: Run the Blazor app; verify no weather navigation link exists and `/weather` is not reachable.

- [ ] T004 [US2] Delete the weather page `src/StockAnalyst.Web/Components/Pages/Weather.razor`.
- [ ] T005 [US2] Remove any navigation entry that links to `/weather` (search and update the Blazor nav component, commonly `src/StockAnalyst.Web/Components/Layout/NavMenu.razor` or similar).

---

## User Story 3 (P1): Delete All Weather Code Artifacts Entirely

**Independent Test**: Repo-wide search finds zero matches (excluding `.specify/` artifacts) for `WeatherForecast`, `weatherforecast`, and weather sample identifiers.

- [ ] T006 [US3] Remove the weather request from the API HTTP scratch file `src/StockAnalyst.Api/StockAnalyst.Api.http` (delete the `GET .../weatherforecast/` request block).
- [ ] T007 [US3] Repo-wide search for `WeatherForecast` and `weatherforecast` and remove any remaining references outside `.specify/`.

---

## User Story 4 (P1): Keep Builds and Tests Green After Removal

**Independent Test**: `dotnet build` and `dotnet test` succeed.

- [ ] T008 [US4] Run `dotnet build` and fix any compile errors caused by removing `Weather.razor` and the API weather endpoint/type.
- [ ] T009 [US4] Run `dotnet test` and remove/update any tests that were exclusively for weather sample behavior (if any exist).
- [ ] T010 [US4] If GitHub Actions workflows exist, verify CI passes (adjust only if build/test steps referenced deleted endpoints/files).

---

## Definition of Done (from spec)

- [ ] DOD1 No weather endpoints exist (no `/weatherforecast` route, no `WeatherForecast` sample types).
- [ ] DOD2 No weather UI pages/links exist in the Blazor app (no `/weather` page).
- [ ] DOD3 Repo-wide search for `WeatherForecast` and `weatherforecast` returns zero matches in source (excluding `.specify/` artifacts).
- [ ] DOD4 `dotnet build` passes.
- [ ] DOD5 `dotnet test` passes.
