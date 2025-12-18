using System.Security.Cryptography;
using System.Text;
using ApplicationServices.Contracts.Requests;
using ApplicationServices.Contracts.Responses;
using Domain.Database;
using Domain.ValueObjects;

namespace ApplicationServices.Services;

public class GameDataService : IGameDataService
{
    private readonly IGameDatabase _database;

    public GameDataService(IGameDatabase database)
    {
        _database = database;
    }

    public async Task<AuthResponse?> RegisterAsync(RegisterRequest request, CancellationToken cancellationToken = default)
    {
        var database = await _database.ReadAsync(cancellationToken);

        if (database.Users.Any(u => u.Username.Equals(request.Username, StringComparison.OrdinalIgnoreCase)))
        {
            return null;
        }

        var user = new UserAccount
        {
            Username = request.Username,
            PasswordHash = HashPassword(request.Password)
        };

        var token = CreateToken();
        user.SessionTokens.Add(token);
        database.Users.Add(user);
        await _database.WriteAsync(database, cancellationToken);

        return new AuthResponse
        {
            UserId = user.Id,
            Token = token
        };
    }

    public async Task<AuthResponse?> LoginAsync(LoginRequest request, CancellationToken cancellationToken = default)
    {
        var database = await _database.ReadAsync(cancellationToken);
        var user = database.Users.FirstOrDefault(u => u.Username.Equals(request.Username, StringComparison.OrdinalIgnoreCase));
        if (user is null)
        {
            return null;
        }

        if (!string.Equals(user.PasswordHash, HashPassword(request.Password), StringComparison.Ordinal))
        {
            return null;
        }

        var token = CreateToken();
        user.SessionTokens.Add(token);
        await _database.WriteAsync(database, cancellationToken);

        return new AuthResponse
        {
            UserId = user.Id,
            Token = token
        };
    }

    public async Task<IReadOnlyCollection<MonsterProfile>> GetMonstersAsync(CancellationToken cancellationToken = default)
    {
        var database = await _database.ReadAsync(cancellationToken);
        if (database.Monsters.Count == 0)
        {
            database.Monsters.AddRange(DatabaseModel.CreateDefaultMonsters());
            await _database.WriteAsync(database, cancellationToken);
        }

        return database.Monsters.AsReadOnly();
    }

    public async Task<ProgressResponse?> GetProgressAsync(string token, CancellationToken cancellationToken = default)
    {
        var database = await _database.ReadAsync(cancellationToken);
        var user = FindUserByToken(database, token);
        if (user is null)
        {
            return null;
        }

        var progress = database.Progress.FirstOrDefault(p => p.UserId == user.Id);
        if (progress is null)
        {
            return null;
        }

        EnsureSaveSlots(progress);

        return new ProgressResponse
        {
            UserId = user.Id,
            Level = progress.Level,
            Experience = progress.Experience,
            AdventureState = progress.AdventureState,
            LastUpdatedUtc = progress.LastUpdatedUtc,
            SaveSlots = progress.SaveSlots.AsReadOnly()
        };
    }

    public async Task<bool> SaveProgressAsync(SaveProgressRequest request, CancellationToken cancellationToken = default)
    {
        var database = await _database.ReadAsync(cancellationToken);
        var user = FindUserByToken(database, request.Token);
        if (user is null)
        {
            return false;
        }

        var progress = database.Progress.FirstOrDefault(p => p.UserId == user.Id);
        if (progress is null)
        {
            progress = new PlayerProgress { UserId = user.Id };
            database.Progress.Add(progress);
        }

        progress.Level = request.Level;
        progress.Experience = request.Experience;
        progress.AdventureState = request.AdventureState;
        progress.LastUpdatedUtc = DateTimeOffset.UtcNow;

        var location = new WorldLocation
        {
            Name = string.IsNullOrWhiteSpace(request.LocationName) ? WorldLocation.Default().Name : request.LocationName,
            Biome = string.IsNullOrWhiteSpace(request.LocationBiome) ? WorldLocation.Default().Biome : request.LocationBiome,
            ThreatLevel = string.IsNullOrWhiteSpace(request.LocationThreatLevel)
                ? WorldLocation.Default().ThreatLevel
                : request.LocationThreatLevel
        };

        var saveSlot = progress.SaveSlots.FirstOrDefault(s => s.Name.Equals(request.SaveSlotName, StringComparison.OrdinalIgnoreCase));
        if (saveSlot is null)
        {
            saveSlot = new SaveSlot { Name = request.SaveSlotName };
            progress.SaveSlots.Add(saveSlot);
        }

        saveSlot.Level = request.Level;
        saveSlot.Experience = request.Experience;
        saveSlot.AdventureState = request.AdventureState;
        saveSlot.LastUpdatedUtc = DateTimeOffset.UtcNow;
        saveSlot.Location = location;

        await _database.WriteAsync(database, cancellationToken);
        return true;
    }

    private static string CreateToken()
    {
        return Convert.ToBase64String(RandomNumberGenerator.GetBytes(32));
    }

    private static string HashPassword(string password)
    {
        var bytes = Encoding.UTF8.GetBytes(password);
        var hashBytes = SHA256.HashData(bytes);
        return Convert.ToBase64String(hashBytes);
    }

    private static UserAccount? FindUserByToken(DatabaseModel database, string token)
    {
        return database.Users.FirstOrDefault(u => u.SessionTokens.Contains(token));
    }

    private static void EnsureSaveSlots(PlayerProgress progress)
    {
        if (progress.SaveSlots.Count == 0)
        {
            progress.SaveSlots.Add(new SaveSlot
            {
                Name = "Slot 1",
                AdventureState = progress.AdventureState,
                Experience = progress.Experience,
                Level = progress.Level,
                LastUpdatedUtc = progress.LastUpdatedUtc,
                Location = WorldLocation.Default()
            });
        }
    }
}
