# Feature: abm-productos

## Objective
Full product ABM (alta, baja, modificación) for the Encargada, and fix the sale screen so every active product can be added to the sale.

## Problem / Why
- `RegistrarVenta.razor` only shows "Agregar" when `Estado == "Disponible"`. The system does not track stock (by design), so availability is decided by the employee, not by a product field. Adding to the order note is a separate employee action when the product is not physically in the pharmacy.
- Only create exists for products; edit and delete are missing in the UI.
- Physical delete would break sales history (`detalle_venta` references the product; FK restrict).

## Decisions
- `Producto.Estado` now means catalog status: `Activo` / `Inactivo` (user clarified that stock is not controlled; previous `Disponible`/`No disponible` assumption dropped).
- Baja = logical: `DELETE api/producto/{id}` sets `Estado = "Inactivo"`; reactivation via edit. Any value other than `Inactivo` is treated as active (legacy rows).
- Sale search excludes inactive products; product list can include them.

## Scope / Constraints
- Branch `feat/frontend-blazor` (continues).
- UI copy in Spanish. Reuse `IHttpService` and Shared DTOs.
- TDD: Strict TDD configured but no test project/runner; verification is `dotnet build` (gap disclosed).

## Tasks
- [x] T1 — Sale page fix: "Agregar" for every result; "Agregar a nota de pedido" as a secondary action on every result; `buscar` excludes inactive by default (`incluirInactivos` query flag). Route: delegated.
- [x] T2 — ABM: shared product form component; edit page `/productos/editar/{id}`; list actions editar / dar de baja / reactivar (Encargada); logical delete on the API; Estado select `Activo`/`Inactivo`. Route: delegated.
- [x] T3 — Docs: update `CLAUDE.md` and the pending-question note about product `estado` in `docs/03-preguntas-al-cliente.md` (now answered: catalog status, stock not tracked). Route: delegated.

## Checks
- `dotnet build FarmaTech.slnx`

## Progress / Evidence
- T1 done — `2746cf2`. Root cause: "Agregar" was gated on `Estado == "Disponible"`. Now every active result can be added; note-de-pedido is a separate action.
- T2 done — `89b205a`. Edit page, shared form, logical delete, reactivate.
- T3 done — `18f7b2e`. CLAUDE.md + docs 03/04/05 wording.
- Parent spot check: `dotnet build FarmaTech.slnx -p:ArtifactsPath=<scratch>` → 0 warnings, 0 errors (running server locks default output).
- Not verified at runtime; no tests (no test project).

## Next step
User: restart server, smoke test ABM and sale.
