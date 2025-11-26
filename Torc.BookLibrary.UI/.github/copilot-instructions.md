# Torc Book Library Agent Guide
- React 19 + TypeScript + Vite UI lives entirely under `Torc.BookLibrary.UI`; backend APIs reside in a sibling `.API` project and are consumed over HTTP only.
- App entry `src/App.tsx` wires `BookSearch` -> `fetchBooks` -> `BookList`; keep additions inside this data flow so loading/error state is centralized in `App`.
- Material UI (core + X DataGrid) provides layout and tables; keep styling in component `sx` props and avoid bespoke CSS unless necessary.

## Architecture & Data Flow
- `src/App.tsx` owns `books`, `loading`, and `error` state, performs initial fetch on mount, and passes `handleSearch` to `BookSearch`; extend this file when new global app state or banners are required.
- `src/components/BookSearch.tsx` limits query params to the chosen search mode; new filters should follow the same selective-append pattern to avoid sending empty params.
- `src/components/BookList.tsx` renders `@mui/x-data-grid` rows with `id: bookId`; any new columns must update both the `Book` type and `columns` definition to keep the grid stable.
- Shared types live in `src/types/Book.ts`; update this file before touching API or UI code so TypeScript catches mismatches.

## API Contract
- `src/api/BookApi.ts` wraps fetches against `http://localhost:8010/api/Books`; adjust `API_BASE_URL` (or better, move to env var) if the backend host changes.
- Query string building uses `URLSearchParams`; only append params when a value exists (ownershipStatus permits `0`, so check for `!== undefined/null`).
- The API currently returns a flat `Book[]`; pagination is client-side only via DataGrid, so if the backend introduces server paging you must extend `BookSearchParams` and store pagination metadata alongside `books`.

## UI Patterns
- Layout relies on `Container` + nested `Box` elements with `flexGrow` to keep the grid filling the viewport; mimic this pattern for new sections to maintain responsive height.
- `BookSearch` uses the new MUI Grid v3 API (`size` prop) for responsive rows; prefer the same API when adding form controls so spacing stays consistent.
- `BookList` calculates height from `useMediaQuery`; reuse this hook when introducing components that need viewport-aware sizing instead of hardcoding pixels.

## Developer Workflows
- Install deps with `npm install`; start locally via `npm run dev` (Vite dev server on 5173) and ensure the backend API is available at port 8010.
- Production builds run `npm run build` which executes `tsc -b` before `vite build`; keep the TypeScript project references (`tsconfig.json`) intact so incremental builds stay fast.
- Lint with `npm run lint`; CI runs the same command plus the Vite build (see README build badge referencing `.github/workflows/nodejs-vite.yml`).

## Coding Conventions & Lint
- TypeScript is strict with additional checks like `noUncheckedSideEffectImports` and `erasableSyntaxOnly` (`tsconfig.app.json`); do not introduce implicit `any` or unused values.
- ESLint config (`eslint.config.js`) extends `@eslint/js` + `typescript-eslint` and enforces React Hooks + React Refresh rules; keep components as functions and ensure hooks stay at the top level.
- Favor data-fetch helpers in `src/api` instead of calling `fetch` directly inside components so error/loading handling remains centralized.
