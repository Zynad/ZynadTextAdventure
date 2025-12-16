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
- **Domain**: JSON persistence (`JsonDatabase`) and data models such as `UserAccount`, `SaveSlot`, `WorldLocation`, and `MonsterProfile`.

The API exposes endpoints for registration/login (`/api/auth/register`, `/api/auth/login`), fetching monsters (`/api/monsters`), and saving or loading player progress (`/api/progress/save`, `/api/progress?token=...`). Each authentication response returns a simple session token that can be used when saving or restoring progress.
