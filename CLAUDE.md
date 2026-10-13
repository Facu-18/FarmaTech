# CLAUDE.md

This file provides guidance to Claude Code (claude.ai/code) when working with code in this repository.

## Overview

FarmaTech is a pharmacy management app (products, sales, suppliers/"droguerías", stock intake, purchase orders, employees). It is a .NET 10 solution: an ASP.NET Core Web API that also hosts a Blazor WebAssembly client. Domain names, identifiers, and user-facing messages are in Spanish — keep that convention when extending existing code.

## Commands

```bash
dotnet build FarmaTech.slnx                     # build everything
dotnet run --project FarmaTech.Server           # run API + hosted Blazor client (http://localhost:5193, https://localhost:7221)

# EF Core migrations (DbContext lives in FarmaTech.BD, startup/config in FarmaTech.Server)
dotnet ef migrations add <Name> --project FarmaTech.BD --startup-project FarmaTech.Server
dotnet ef database update --project FarmaTech.BD --startup-project FarmaTech.Server
```

There are no test projects yet.

Build output goes to `%LOCALAPPDATA%\IBUY\artifacts` on Windows (`Directory.Build.props`), not to per-project `bin/obj`.

Swagger UI is available in Development at `/swagger`, with a Bearer (JWT) security scheme — paste the token without the `Bearer` prefix.

## Configuration

`FarmaTech.Server/appsettings.json` is gitignored (user secrets are also enabled). The server fails fast at startup unless it has:

- `ConnectionStrings:ConnSqlServer` — SQL Server connection string.
- `Jwt:Issuer`, `Jwt:Audience`, `Jwt:Key` (≥ 32 bytes), optional `Jwt:ExpirationMinutes` (default 60).
- Optional `BootstrapAdmin:Usuario` + `BootstrapAdmin:Pin` (4–6 digits), plus optional `Nombre`/`Apellido`. `IdentitySeeder` runs on every startup: it creates the `Encargada` and `Empleada` roles and, if configured, the initial admin user.

## Architecture

Project dependency flow:

```
FarmaTech.Server ──> FarmaTech.Repository ──> FarmaTech.BD
       │                     └──────────────> FarmaTech.Shared
       └──> FarmaTech.Cliente ──> FarmaTech.Servicios ──> FarmaTech.Shared
```

- **FarmaTech.BD** — EF Core data layer. `AppDbContext` extends `IdentityDbContext<Empleada, IdentityRole<int>, int>`. Entities inherit `EntityBase` (int identity `Id`) and use data annotations for validation with Spanish error messages. `Empleada` is the Identity user (`IdentityUser<int>`) and implements `IEntityBase` directly; it is mapped to the `Empleadas` table. Migrations live here.
- **FarmaTech.Repository** — generic `IRepositorio<E>` / `Repositorio<E>` (Select, SelectById, Insert, Update, Delete, Existe) constrained to `IEntityBase`. Entity-specific repositories extend it (e.g. `IEmpleadaRepositorio`). Each repository must be registered explicitly in `FarmaTech.Server/Program.cs`.
- **FarmaTech.Shared** — DTOs exchanged between server and client. Controllers map DTO ↔ entity by hand (no AutoMapper).
- **FarmaTech.Server** — API controllers under `api/<entity>` routes, injecting `IRepositorio<T>`. Auth: ASP.NET Core Identity (`AddIdentityCore`, PIN-style passwords: min length 4, digit required, lockout after 5 failures) + JWT bearer. `POST api/auth/login` takes `Usuario`/`Pin` and returns a token from `TokenService`. Role constants are in `Autenticacion/Roles.cs`; protect endpoints with `[Authorize(Roles = Roles.Encargada)]` (`EmpleadaController` and the write endpoints of `ProductoController` are role-restricted). The server also serves the Blazor client (`UseBlazorFrameworkFiles` + `MapFallbackToFile("index.html")`).
- **FarmaTech.Cliente** — Blazor WebAssembly app. Its `HttpClient` base address is the host, so API calls use relative `api/...` URLs. Auth: `Autenticacion/SesionService` calls `api/auth/login`, keeps the `LoginResponseDTO` in `localStorage` (restored on startup, discarded once expired) and applies the token via `IHttpService.EstablecerToken`; `ProveedorAutenticacion` (`AuthenticationStateProvider`) builds the claims (id, name, role) from it. Pages use `@attribute [Authorize]` (role-restricted ones use `Roles = "Encargada"`) and `App.razor` redirects anonymous users to `/login`. Pages: `/login`, `/` (home), `/productos`, `/productos/nuevo` (Encargada), `/productos/editar/{id}` (Encargada), `/ventas/nueva` (register sale).
- **FarmaTech.Servicios** — client-side HTTP wrapper (`IHttpService`/`HttpService` returning `HttpResp<T>`), which normalizes error bodies (plain text, ProblemDetails, validation `errors`) into a single message. Namespace: `FarmaTech.Servicios.ServiciosHTTP`. Registered in the client as `IHttpService`; call `EstablecerToken(jwt)` after login to send the Bearer header.

## Key endpoints

- `POST api/auth/login` — `Usuario`/`Pin` → JWT.
- `api/producto` — the whole controller requires login; POST/PUT/DELETE require the `Encargada` role (duplicate `Codigo` returns 400). `Producto.Estado` is the catalog status (`Activo`/`Inactivo`); stock is not tracked (the employee decides physical availability and uses the order note when a product is missing). `DELETE api/producto/{id}` is a logical delete (sets `Estado = "Inactivo"`, keeps sale history); reactivate with PUT. Any `Estado` other than `Inactivo` is treated as active (legacy rows). `GET api/producto/{id}` returns `ProductoResponseDTO`.
- `GET api/producto/codigo/{codigo}` — exact lookup by code (404 if missing).
- `GET api/producto/buscar?texto=&incluirInactivos=false` — partial match on name or code (max 50, 400 if empty); inactive products are excluded unless `incluirInactivos=true` (the product list passes true, the sale page false).
- `POST api/venta/registrar` — `[Authorize]`; items `{IdProducto, Cantidad}`; prices, totals, date and employee are set server-side.
- `POST api/nota-pedido/faltante` — `[Authorize]`; creates a "Pendiente" order note for a missing product.

- **UI conventions (Cliente)** — design tokens and `.ft-*` utility classes live in `wwwroot/css/app.css`; page styles go in scoped `.razor.css`; icons are inline SVGs via `Shared/Icono.razor` (no icon fonts or CDNs); `Pages/Login` uses `Layout/LayoutVacio` (no sidebar).
