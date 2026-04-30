---
description: "Actionable task list for feature 0001-remove-weather-sample-code"
---

# Tasks: Remove Weather Sample Code

**Feature**: `0001-remove-weather-sample-code`

**Inputs**:
- Spec: `.specify/specs/0001-remove-weather-sample-code.md`
- Plan: *(not present yet — tasks below assume the default solution layout stated in the constitution: `src/StockAnalyst.Api`, `src/StockAnalyst.Web`, `src/StockAnalyst.Core`, tests in `tests/`)*

**Goal**: Remove all ASP.NET template “weather forecast” sample artifacts from API + Blazor UI, clean up wiring, and keep `dotnet build` / `dotnet test` green.

## Format
`[ID] [P?] [US#] Description`
- **[P]**: can run in parallel (different files / low merge conflict risk)
- **US#**: user story from the spec (US1–US4)

---

## User Story 1 (P1): Remove Weather Endpoints from the API

**Independent Test**: Build and run the API; confirm no `/weatherforecast` (or similar) endpoints exist and repo search shows no remaining weather sample controller/minimal API routes/models.

- [ ] T001 [US1] Identify the API project entrypoint and routing style (controller vs minimal API) in `src/StockAnalyst.Api` (inspect `Program.cs` and controller folder structure).
- [ ] T002 [P] [US1] Delete the weather endpoint implementation in `src/StockAnalyst.Api` (e.g., `Controllers/WeatherForecastController.cs` or any minimal API route mapping for weather).
- [ ] T003 [P] [US1] Delete the weather DTO/model in `src/StockAnalyst.Api` (e.g., `WeatherForecast.cs`) if it exists.
- [ ] T004 [US1] Remove any API registrations or route wiring related to weather (DI registrations, endpoint mapping, OpenAPI/Swagger examples that reference weather types).
- [ ] T005 [US1] Ensure the API still starts cleanly after removal (no missing type references / DI exceptions).

---

## User Story 2 (P1): Remove Weather UI Components from the Web App

**Independent Test**: Run the Blazor app; verify navigation has no weather link and no weather page/route is reachable.

- [ ] T006 [US2] Locate weather-related pages/components/routes in `src/StockAnalyst.Web` (common names: `Weather.razor`, `FetchData.razor`, etc.).
- [ ] T007 [P] [US2] Delete the weather page/component(s) in `src/StockAnalyst.Web`.
- [ ] T008 [US2] Remove navigation entries pointing to weather pages (commonly `NavMenu.razor` or similar in `src/StockAnalyst.Web`).
- [ ] T009 [US2] Remove any layout/homepage content that references weather sample data (cards, links, sample text).

---

## User Story 3 (P1): Delete All Weather Code Artifacts Entirely

**Independent Test**: Repo-wide search finds zero matches (excluding speckit artifacts / changelog/docs) for common sample identifiers.

- [ ] T010 [US3] Repo-wide search for weather sample identifiers and enumerate all remaining references (at minimum: `WeatherForecast`, `weatherforecast`, `FetchData`, template sample references).
- [ ] T011 [P] [US3] Remove weather references from solution/project files if any (e.g., `.csproj` includes, `using` statements, embedded resource references).
- [ ] T012 [US3] Remove any weather-only configuration, options, or service wiring (DI, config sections) that becomes dead code once endpoints/pages are deleted.
- [ ] T013 [US3] Validate that no “Forecast” identifiers remain in sample context (be careful about legitimate stock “forecast” terminology—only remove template/sample artifacts).

---

## User Story 4 (P1): Keep Builds and Tests Green After Removal

**Independent Test**: `dotnet build` and `dotnet test` succeed.

- [ ] T014 [US4] Run `dotnet build` and fix compile errors caused by removed files/types.
- [ ] T015 [US4] Run `dotnet test` and remove/update any tests that were exclusively for weather sample behavior.
- [ ] T016 [US4] If GitHub Actions workflows exist, verify CI passes (adjust build/test steps only if they referenced deleted projects/files).

---

## Definition of Done (from spec)

- [ ] DOD1 No weather endpoints exist (no `/weatherforecast` route, no `WeatherForecast*` controller/routes).
- [ ] DOD2 No weather UI pages/links exist in the Blazor app.
- [ ] DOD3 Repo-wide search for `WeatherForecast`, `weatherforecast`, and weather sample identifiers returns zero matches in source (excluding speckit artifacts).
- [ ] DOD4 `dotnet build` passes.
- [ ] DOD5 `dotnet test` passes.
