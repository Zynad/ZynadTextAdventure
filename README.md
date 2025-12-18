# TextAdventure

## Running the JSON-backed Web API
The `TextAdventure.Api` project is a lightweight ASP.NET Core Web API that persists users, monsters, and save data to a local JSON file instead of SQL Server.

1. Install the .NET 10 SDK (preview builds work if a stable SDK is not yet available in your environment).
2. Restore dependencies and start the API:
   ```bash
   dotnet restore
   dotnet run --project TextAdventure.Api
   ```
3. By default, data is stored in `TextAdventure.Api/Data/database.json`. The file and directory are created automatically if they do not exist, and writes are performed atomically by replacing a temporary file.

### Solution layout
- **TextAdventure.Api**: Controllers and hosting setup only. Business logic is delegated to the application layer.
- **ApplicationServices**: Contracts and services (e.g., `GameDataService`) that orchestrate authentication, monsters, and progress.
- **Domain**: Persistence contracts (for example, `IGameDatabase`) and data models such as `UserAccount`, `SaveSlot`, `WorldLocation`, and `MonsterProfile`.
- **TextAdventure.Infrastructure**: JSON persistence implementations (including `JsonDatabase`), repositories, and infrastructure services.

The API exposes endpoints for registration/login (`/api/auth/register`, `/api/auth/login`), fetching monsters (`/api/monsters`), and saving or loading player progress (`/api/progress/save`, `/api/progress?token=...`). Each authentication response returns a simple session token that can be used when saving or restoring progress.

### Authentication and request headers
- Successful `/api/auth/register` and `/api/auth/login` calls return the session token in the response body and also set an `authToken` HttpOnly cookie scoped to `/` (suitable for SPAs running on `http://localhost:5173` or `http://localhost:3000`).
- Authorized requests should send an `Authorization: Bearer <token>` header if the cookie is not available. Endpoints that require authentication look for either the header or the cookie.
- The `/api/progress` endpoint continues to accept a `token` query string parameter but will also fall back to the header/cookie when the query parameter is not present.

### Observability and schema
- A lightweight health check is available at `/api/status` and returns `{ status: "ok", timestamp: "<utc>" }` when the API is reachable.
- Swagger/OpenAPI docs are generated from controller XML comments. When running locally, navigate to `/swagger` to view request/response schemas and apply a bearer token for authenticated calls.
- To expose documentation in non-development environments (for example, staging), set `ApiDocumentation:Enabled` to `true` (or export `ApiDocumentation__Enabled=true`) before launching the API. Optional `ApiDocumentation:RequireAuthorization` can be toggled to add an authorization requirement around the OpenAPI and Scalar endpoints.
