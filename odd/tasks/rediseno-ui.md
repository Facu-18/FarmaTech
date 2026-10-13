# Feature: rediseno-ui

## Objective
Redesign every client view and its styles so the app looks polished and is intuitive at the pharmacy counter, without changing behavior.

## Why
The client still looks like the Blazor template (purple sidebar, plain Bootstrap tables). The user asked for nicer, more intuitive views.

## Design direction (default chosen; user may adjust)
- Pharmacy palette: teal/green primary, light neutral background, white cards with soft shadows, rounded corners; system font stack.
- Design tokens as CSS custom properties in `wwwroot/css/app.css`; component-specific styles in scoped `.razor.css` files. Bootstrap stays for grid/forms/utilities.
- Fixed sidebar (collapsible on mobile) with simple inline-SVG icons, user + role and logout at the bottom.
- Sale screen as a POS: large autofocused search on the left, sticky ticket/cart with a prominent total on the right.
- Login as a centered branded card; Home as role-based quick-action tiles.

## Scope / Constraints
- Views and CSS only: no changes to API calls, routes, validation, authorization or business logic.
- UI copy in Spanish (existing convention). No new packages, no external fonts/CDNs, no icon fonts (inline SVG only).
- Accessible: visible focus, sufficient contrast, labels on inputs, buttons not icon-only without `aria-label`.
- Branch `feat/frontend-blazor`.
- TDD: no test project/runner; verification is build + visual check with screenshots when the app is running.

## Tasks
- [x] T1 — Design system + shell: tokens/base styles in `app.css`, `MainLayout`, `NavMenu`, `NotFound`. Route: delegated.
- [x] T2 — Login and Home. Route: delegated.
- [x] T3 — Products: list (toolbar, search, status badges, row actions, empty state), shared form, create/edit pages. Route: delegated.
- [x] T4 — Register sale POS layout (search, results, ticket, confirmation, note-de-pedido inline flow). Route: delegated.

## Checks
- `dotnet build FarmaTech.slnx` (use `-p:ArtifactsPath=<scratch>` if the server is running)
- Visual check via screenshots after restart.

## Progress / Evidence
- T1 `045e500`, T2 `d28e641`, T3 `6ef6d75`, T4 `4179ddd` — each built 0 warnings/0 errors.
- Parent spot check: build 0/0; diff of .razor shows no URL/HTTP/authorization changes (AuthorizeView blocks only re-indented). @code changes: NavMenu `Iniciales` helper, RegistrarVenta autofocus.
- Not done: quantity stepper (would need logic). Not verified: visual result (no screenshots yet); autofocus may be overridden by FocusOnNavigate.

## Next step
Visual check with screenshots (pending user OK to run the app).
