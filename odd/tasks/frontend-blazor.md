# Feature: frontend-blazor

## Objective
Blazor WebAssembly screens for: login, create product (Encargada), and register sale (chosen use case "Registrar Venta"), consuming the existing API through `FarmaTech.Servicios` (`IHttpService`).

## Why
Registering a sale requires products to exist, and every sale/note endpoint requires a JWT. The client is still the Blazor template.

## Scope / Constraints
- Stay on Blazor (user decision; Next.js discarded).
- Branch `feat/frontend-blazor`, stacked on `feat/registrar-venta`.
- Reuse `IHttpService`/`HttpResp<T>` and Shared DTOs; no duplicated HTTP layer.
- Product creation is restricted to role `Encargada` ("Gestionar productos (ABM)" use case); backend product write endpoints get protected accordingly.
- Product `Estado` values assumed `Disponible` / `No disponible` (not defined in client docs — assumption to confirm).
- UI copy in Spanish (existing project convention); Bootstrap already bundled.
- TDD: Strict TDD configured but no test project/runner exists; verification is `dotnet build` (gap disclosed, no test project created).

## Tasks
- [x] T1 — Auth on the client: session service persisting `LoginResponseDTO` in localStorage, `AuthenticationStateProvider` with role claim, token applied to `IHttpService`, login page `/login`, logout, route protection, nav menu by role. Backend: product POST/PUT/DELETE `[Authorize(Roles = Encargada)]`, GETs `[Authorize]`, duplicate code → 400. Route: delegated (writer trigger).
- [x] T2 — Create product page `/productos/nuevo` (Encargada) with DataAnnotations validation, plus product list `/productos`. Route: delegated.
- [x] T3 — Register sale page `/ventas/nueva`: search (name/code), cart with quantities and current prices, "agregar a nota de pedido" for unavailable products, "no encontrado" message, confirm → total, cancel. Remove template pages (Counter, Weather, sample-data). Route: delegated.

## Acceptance criteria
- `dotnet build FarmaTech.slnx` succeeds, no new warnings.
- Flows match `docs/04-caso-de-uso-registrar-venta.md`.

## Checks
- `dotnet build FarmaTech.slnx`

## Progress / Evidence
- T1 done — `65ff236` (client session + AuthenticationStateProvider, /login, product writes require Encargada, duplicate code → 400). Build 0/0.
- T2 done — `55ac234` (/productos, /productos/nuevo, ProductoDTO validations). Build 0/0.
- T3 done — `edbeb85` (/ventas/nueva, template pages removed, CLAUDE.md). Build 0/0.
- Parent spot check: `dotnet build FarmaTech.slnx` → 0 warnings, 0 errors; session code reviewed.
- Not verified: nothing run in a browser or against a DB; no tests (no test project). Known gap: expired token mid-session is not auto-logged-out (API returns 401 message).

## Next step
User: apply migrations, run the app, smoke test login → create product → register sale.
