# Feature: registrar-venta

## Objective
Align the code with the final ERD (`Der_FarmaTech.pdf`), implement the "Registrar Venta" use case end to end on the API (including product search by code/name and adding missing products to the order note), prepare the Blazor client to consume it, and produce the updated course deliverables (ERD, use case diagram, client questions, chosen use case, sequence diagram).

## Problem / Why
- Entities diverge from the final ERD: optional FKs in `nota_pedido` (producto, drogueria) and `ingreso_mercaderia` (drogueria) are required in code; roles are `Administrador/Empleada` while the ERD says `Empleada/Encargada`.
- `POST api/venta` trusts client-provided totals and employee id; the use case requires the current system price and server-side totals.
- No product lookup by code or partial name.
- `FarmaTech.Servicios` HTTP services are not wired into the client and still use the `IBUY` namespace.

## Scope / Constraints
- Final ERD is the source of truth; no new columns.
- Use case alternate flow 1 reinterpreted as "product not available" (exists in catalog) because `nota_pedido` can only reference a product id.
- Domain identifiers and user-facing messages stay in Spanish (existing convention).
- TDD: config says Strict TDD enabled, but the solution has no test project/runner. Verification uses `dotnet build` + EF migration generation. Creating a test project is out of scope unless the user asks (gap disclosed).

## Tasks
- [x] T1 — Align entities with ERD: nullable `NotaPedido.IdProducto`, `NotaPedido.IdDrogueria`, `IngresoMercaderia.IdDrogueria`; unique index on `Producto.Codigo`; role `Administrador` → `Encargada`; `Venta.Detalles` navigation; DTO nullability; migration `AjusteDerDefinitivo`. Route: delegated (writer trigger, 2+ non-trivial files).
- [x] T2 — Product search: `GET api/producto/codigo/{codigo}` and `GET api/producto/buscar?texto=` (partial name or code) via `IProductoRepositorio`; response DTO includes `Id`. Route: delegated.
- [x] T3 — Registrar venta: `POST api/venta/registrar` (authorized) with items `{IdProducto, Cantidad}`; server takes current price, computes subtotals/total, employee from JWT, date now, single transaction; `POST api/notapedido/faltante` (authorized). Route: delegated.
- [x] T4 — Client prep: namespace `FarmaTech.Servicios.ServiciosHTTP`, bearer token support, register `IHttpService` in `FarmaTech.Cliente`, imports. Route: delegated.
- [x] T5 — Deliverables in `docs/` (Markdown + Mermaid, Spanish): ERD, use case diagram, client questions, use case "Registrar Venta", sequence diagram. Route: delegated (separate docs writer, disjoint paths).

## Acceptance criteria
- `dotnet build FarmaTech.slnx` succeeds with no new errors.
- Migration generated and reviewed (only nullability/index changes).
- Endpoints documented in `CLAUDE.md`.

## Checks
- `dotnet build FarmaTech.slnx`
- `dotnet ef migrations add AjusteDerDefinitivo --project FarmaTech.BD --startup-project FarmaTech.Server`

## Progress / Evidence
- T1 done — `16493a4`. Migration `20261013134709_AjusteDerDefinitivo`: 3 FK columns nullable, unique `IX_Productos_Codigo`, FKs re-created (delete behavior Cascade → NoAction, consequence of optional FKs). Build 0 errors, warnings 9 → 0.
- T2 done — `32d071a`. `GET api/producto/codigo/{codigo}`, `GET api/producto/buscar?texto=`. Build OK.
- T3 done — `164aa85`. `POST api/venta/registrar`, `POST api/nota-pedido/faltante` (existing route prefix uses a hyphen). Build OK.
- T4 done — `2d4870c` (Class1.cs removals, split by a failed commit) + `48c8d9e`. Build OK.
- T5 done — `docs/` (6 files). Route fixed to `api/nota-pedido`. Mermaid not rendered/validated by a tool (manual syntax review only).
- Parent spot check: `dotnet build FarmaTech.slnx` → 0 errors.
- Not verified: migration not applied to a DB; endpoints not exercised at runtime; no tests (no test project).

## Next step
User: apply migration (`dotnet ef database update ...`), try endpoints in Swagger, decide on open questions (catalog-missing products, product `estado` values).

- [x] T1 · [x] T2 · [x] T3 · [x] T4 · [x] T5
