using ApplicationServices.Admin.Models;
using ApplicationServices.Authentication;
using ApplicationServices.Contracts.Repositories;
using Domain.Core;
using Domain.ValueObjects;

namespace ApplicationServices.Admin;

public class AdminWorldService(
    GetCurrentUserHandler getCurrentUserHandler,
    IWorldRepository worldRepository) : IAdminWorldService
{
    public async Task<AdminOperationResult<IReadOnlyCollection<TownDto>>> GetTownsAsync(
        string token,
        CancellationToken cancellationToken = default)
    {
        var authorization = await AuthorizeAsync(token, cancellationToken);
        if (!authorization.Success)
        {
            return AdminOperationResult<IReadOnlyCollection<TownDto>>.Unauthorized(authorization.Error ?? "Unauthorized");
        }

        var state = await LoadWorldAsync(cancellationToken);
        return AdminOperationResult<IReadOnlyCollection<TownDto>>.FromSuccess(state.Towns.Select(ToDto).ToList());
    }

    public async Task<AdminOperationResult<TownDto>> CreateTownAsync(
        string token,
        TownDto townDto,
        CancellationToken cancellationToken = default)
    {
        var authorization = await AuthorizeAsync(token, cancellationToken);
        if (!authorization.Success)
        {
            return AdminOperationResult<TownDto>.Unauthorized(authorization.Error ?? "Unauthorized");
        }

        if (string.IsNullOrWhiteSpace(townDto.Name))
        {
            return AdminOperationResult<TownDto>.ValidationFailed("Town name is required");
        }

        var state = await LoadWorldAsync(cancellationToken);
        if (state.Towns.Any(t => t.Name.Equals(townDto.Name, StringComparison.OrdinalIgnoreCase)))
        {
            return AdminOperationResult<TownDto>.Conflict("Town already exists");
        }

        var entity = ToEntity(townDto);
        state.Towns.Add(entity);
        await SaveWorldAsync(state, cancellationToken);

        return AdminOperationResult<TownDto>.FromSuccess(ToDto(entity));
    }

    public async Task<AdminOperationResult<TownDto>> UpdateTownAsync(
        string token,
        string townName,
        TownDto townDto,
        CancellationToken cancellationToken = default)
    {
        var authorization = await AuthorizeAsync(token, cancellationToken);
        if (!authorization.Success)
        {
            return AdminOperationResult<TownDto>.Unauthorized(authorization.Error ?? "Unauthorized");
        }

        var state = await LoadWorldAsync(cancellationToken);
        var town = state.Towns.FirstOrDefault(t => t.Name.Equals(townName, StringComparison.OrdinalIgnoreCase));
        if (town is null)
        {
            return AdminOperationResult<TownDto>.NotFound("Town not found");
        }

        if (!town.Name.Equals(townDto.Name, StringComparison.OrdinalIgnoreCase)
            && state.Towns.Any(t => t.Name.Equals(townDto.Name, StringComparison.OrdinalIgnoreCase)))
        {
            return AdminOperationResult<TownDto>.Conflict("Town already exists");
        }

        var updated = ToEntity(townDto);
        var index = state.Towns.IndexOf(town);
        state.Towns[index] = updated;
        await SaveWorldAsync(state, cancellationToken);

        return AdminOperationResult<TownDto>.FromSuccess(ToDto(updated));
    }

    public async Task<AdminOperationResult<bool>> DeleteTownAsync(
        string token,
        string townName,
        CancellationToken cancellationToken = default)
    {
        var authorization = await AuthorizeAsync(token, cancellationToken);
        if (!authorization.Success)
        {
            return AdminOperationResult<bool>.Unauthorized(authorization.Error ?? "Unauthorized");
        }

        var state = await LoadWorldAsync(cancellationToken);
        var removed = state.Towns.RemoveAll(t => t.Name.Equals(townName, StringComparison.OrdinalIgnoreCase)) > 0;
        if (!removed)
        {
            return AdminOperationResult<bool>.NotFound("Town not found");
        }

        await SaveWorldAsync(state, cancellationToken);
        return AdminOperationResult<bool>.FromSuccess(true);
    }

    public async Task<AdminOperationResult<TownNpcDto>> CreateNpcAsync(
        string token,
        string townName,
        TownNpcDto townNpc,
        CancellationToken cancellationToken = default)
    {
        var authorization = await AuthorizeAsync(token, cancellationToken);
        if (!authorization.Success)
        {
            return AdminOperationResult<TownNpcDto>.Unauthorized(authorization.Error ?? "Unauthorized");
        }

        var state = await LoadWorldAsync(cancellationToken);
        var town = state.Towns.FirstOrDefault(t => t.Name.Equals(townName, StringComparison.OrdinalIgnoreCase));
        if (town is null)
        {
            return AdminOperationResult<TownNpcDto>.NotFound("Town not found");
        }

        if (string.IsNullOrWhiteSpace(townNpc.Name))
        {
            return AdminOperationResult<TownNpcDto>.ValidationFailed("NPC name is required");
        }

        var npcId = string.IsNullOrWhiteSpace(townNpc.Id) ? Guid.NewGuid().ToString() : townNpc.Id;
        if (town.Npcs.Any(n => n.Id.Equals(npcId, StringComparison.OrdinalIgnoreCase)))
        {
            return AdminOperationResult<TownNpcDto>.Conflict("NPC already exists");
        }

        var entity = ToEntity(townNpc);
        entity.Id = npcId;
        town.Npcs.Add(entity);
        await SaveWorldAsync(state, cancellationToken);

        return AdminOperationResult<TownNpcDto>.FromSuccess(ToDto(entity));
    }

    public async Task<AdminOperationResult<TownNpcDto>> UpdateNpcAsync(
        string token,
        string townName,
        string npcId,
        TownNpcDto townNpc,
        CancellationToken cancellationToken = default)
    {
        var authorization = await AuthorizeAsync(token, cancellationToken);
        if (!authorization.Success)
        {
            return AdminOperationResult<TownNpcDto>.Unauthorized(authorization.Error ?? "Unauthorized");
        }

        var state = await LoadWorldAsync(cancellationToken);
        var town = state.Towns.FirstOrDefault(t => t.Name.Equals(townName, StringComparison.OrdinalIgnoreCase));
        if (town is null)
        {
            return AdminOperationResult<TownNpcDto>.NotFound("Town not found");
        }

        var npc = town.Npcs.FirstOrDefault(n => n.Id.Equals(npcId, StringComparison.OrdinalIgnoreCase));
        if (npc is null)
        {
            return AdminOperationResult<TownNpcDto>.NotFound("NPC not found");
        }

        var updated = ToEntity(townNpc);
        updated.Id = npcId;
        var index = town.Npcs.IndexOf(npc);
        town.Npcs[index] = updated;
        await SaveWorldAsync(state, cancellationToken);

        return AdminOperationResult<TownNpcDto>.FromSuccess(ToDto(updated));
    }

    public async Task<AdminOperationResult<bool>> DeleteNpcAsync(
        string token,
        string townName,
        string npcId,
        CancellationToken cancellationToken = default)
    {
        var authorization = await AuthorizeAsync(token, cancellationToken);
        if (!authorization.Success)
        {
            return AdminOperationResult<bool>.Unauthorized(authorization.Error ?? "Unauthorized");
        }

        var state = await LoadWorldAsync(cancellationToken);
        var town = state.Towns.FirstOrDefault(t => t.Name.Equals(townName, StringComparison.OrdinalIgnoreCase));
        if (town is null)
        {
            return AdminOperationResult<bool>.NotFound("Town not found");
        }

        var removed = town.Npcs.RemoveAll(n => n.Id.Equals(npcId, StringComparison.OrdinalIgnoreCase)) > 0;
        if (!removed)
        {
            return AdminOperationResult<bool>.NotFound("NPC not found");
        }

        await SaveWorldAsync(state, cancellationToken);
        return AdminOperationResult<bool>.FromSuccess(true);
    }

    public async Task<AdminOperationResult<IReadOnlyCollection<AdminWorldLocationDto>>> GetLocationsAsync(
        string token,
        CancellationToken cancellationToken = default)
    {
        var authorization = await AuthorizeAsync(token, cancellationToken);
        if (!authorization.Success)
        {
            return AdminOperationResult<IReadOnlyCollection<AdminWorldLocationDto>>.Unauthorized(
                authorization.Error ?? "Unauthorized");
        }

        var state = await LoadWorldAsync(cancellationToken);
        return AdminOperationResult<IReadOnlyCollection<AdminWorldLocationDto>>.FromSuccess(
            state.Locations.Select(ToDto).ToList());
    }

    public async Task<AdminOperationResult<AdminWorldLocationDto>> CreateLocationAsync(
        string token,
        AdminWorldLocationDto location,
        CancellationToken cancellationToken = default)
    {
        var authorization = await AuthorizeAsync(token, cancellationToken);
        if (!authorization.Success)
        {
            return AdminOperationResult<AdminWorldLocationDto>.Unauthorized(authorization.Error ?? "Unauthorized");
        }

        if (string.IsNullOrWhiteSpace(location.Name))
        {
            return AdminOperationResult<AdminWorldLocationDto>.ValidationFailed("Location name is required");
        }

        var state = await LoadWorldAsync(cancellationToken);
        var locationId = string.IsNullOrWhiteSpace(location.Id) ? Guid.NewGuid().ToString() : location.Id;
        if (state.Locations.Any(l => l.Id.Equals(locationId, StringComparison.OrdinalIgnoreCase)))
        {
            return AdminOperationResult<AdminWorldLocationDto>.Conflict("Location already exists");
        }

        var entity = ToEntity(location);
        entity.Id = locationId;
        state.Locations.Add(entity);
        await SaveWorldAsync(state, cancellationToken);

        return AdminOperationResult<AdminWorldLocationDto>.FromSuccess(ToDto(entity));
    }

    public async Task<AdminOperationResult<AdminWorldLocationDto>> UpdateLocationAsync(
        string token,
        string locationId,
        AdminWorldLocationDto location,
        CancellationToken cancellationToken = default)
    {
        var authorization = await AuthorizeAsync(token, cancellationToken);
        if (!authorization.Success)
        {
            return AdminOperationResult<AdminWorldLocationDto>.Unauthorized(authorization.Error ?? "Unauthorized");
        }

        var state = await LoadWorldAsync(cancellationToken);
        var existing = state.Locations.FirstOrDefault(l => l.Id.Equals(locationId, StringComparison.OrdinalIgnoreCase));
        if (existing is null)
        {
            return AdminOperationResult<AdminWorldLocationDto>.NotFound("Location not found");
        }

        var updated = ToEntity(location);
        updated.Id = locationId;
        var index = state.Locations.IndexOf(existing);
        state.Locations[index] = updated;
        await SaveWorldAsync(state, cancellationToken);

        return AdminOperationResult<AdminWorldLocationDto>.FromSuccess(ToDto(updated));
    }

    public async Task<AdminOperationResult<bool>> DeleteLocationAsync(
        string token,
        string locationId,
        CancellationToken cancellationToken = default)
    {
        var authorization = await AuthorizeAsync(token, cancellationToken);
        if (!authorization.Success)
        {
            return AdminOperationResult<bool>.Unauthorized(authorization.Error ?? "Unauthorized");
        }

        var state = await LoadWorldAsync(cancellationToken);
        var removed = state.Locations.RemoveAll(l => l.Id.Equals(locationId, StringComparison.OrdinalIgnoreCase)) > 0;
        if (!removed)
        {
            return AdminOperationResult<bool>.NotFound("Location not found");
        }

        await SaveWorldAsync(state, cancellationToken);
        return AdminOperationResult<bool>.FromSuccess(true);
    }

    public async Task<AdminOperationResult<IReadOnlyCollection<DropTableDto>>> GetDropTablesAsync(
        string token,
        CancellationToken cancellationToken = default)
    {
        var authorization = await AuthorizeAsync(token, cancellationToken);
        if (!authorization.Success)
        {
            return AdminOperationResult<IReadOnlyCollection<DropTableDto>>.Unauthorized(authorization.Error ?? "Unauthorized");
        }

        var state = await LoadWorldAsync(cancellationToken);
        return AdminOperationResult<IReadOnlyCollection<DropTableDto>>.FromSuccess(
            state.DropTables.Select(ToDto).ToList());
    }

    public async Task<AdminOperationResult<DropTableDto>> UpsertDropTableAsync(
        string token,
        DropTableDto dropTable,
        CancellationToken cancellationToken = default)
    {
        var authorization = await AuthorizeAsync(token, cancellationToken);
        if (!authorization.Success)
        {
            return AdminOperationResult<DropTableDto>.Unauthorized(authorization.Error ?? "Unauthorized");
        }

        if (string.IsNullOrWhiteSpace(dropTable.Biome))
        {
            return AdminOperationResult<DropTableDto>.ValidationFailed("Biome is required");
        }

        var state = await LoadWorldAsync(cancellationToken);
        var existing = state.DropTables.FirstOrDefault(d => d.Biome.Equals(dropTable.Biome, StringComparison.OrdinalIgnoreCase));
        var entity = ToEntity(dropTable);
        if (existing is null)
        {
            state.DropTables.Add(entity);
        }
        else
        {
            var index = state.DropTables.IndexOf(existing);
            state.DropTables[index] = entity;
        }

        await SaveWorldAsync(state, cancellationToken);
        return AdminOperationResult<DropTableDto>.FromSuccess(dropTable);
    }

    public async Task<AdminOperationResult<bool>> DeleteDropTableAsync(
        string token,
        string biome,
        CancellationToken cancellationToken = default)
    {
        var authorization = await AuthorizeAsync(token, cancellationToken);
        if (!authorization.Success)
        {
            return AdminOperationResult<bool>.Unauthorized(authorization.Error ?? "Unauthorized");
        }

        var state = await LoadWorldAsync(cancellationToken);
        var removed = state.DropTables.RemoveAll(d => d.Biome.Equals(biome, StringComparison.OrdinalIgnoreCase)) > 0;
        if (!removed)
        {
            return AdminOperationResult<bool>.NotFound("Drop table not found");
        }

        await SaveWorldAsync(state, cancellationToken);
        return AdminOperationResult<bool>.FromSuccess(true);
    }

    private static TownDto ToDto(Town town)
    {
        return new TownDto(
            town.Name,
            town.VendorInventory.Select(price => new VendorPriceDto(price.ItemId, price.BuyPrice, price.SellPrice)).ToList(),
            town.Npcs.Select(ToDto).ToList());
    }

    private static TownNpcDto ToDto(TownNpc npc)
    {
        return new TownNpcDto(
            npc.Id,
            npc.Name,
            npc.Role,
            npc.RoleType,
            npc.IsVendor,
            npc.Personality,
            npc.Location,
            npc.QuestsOffered,
            new NpcDialogueDto(
                npc.Dialogue.Greetings,
                npc.Dialogue.QuestOffers,
                npc.Dialogue.Farewells,
                npc.Dialogue.RandomLines,
                npc.Dialogue.TradeOpeners));
    }

    private static AdminWorldLocationDto ToDto(WorldLocationNode location)
    {
        return new AdminWorldLocationDto(
            location.Id,
            location.Name,
            location.Description,
            location.Biome,
            location.ThreatLevel,
            location.AdjacentLocationIds,
            location.TownName);
    }

    private static DropTableDto ToDto(DropTable dropTable)
    {
        return new DropTableDto(dropTable.Biome, dropTable.Drops);
    }

    private static Town ToEntity(TownDto dto)
    {
        return new Town
        {
            Name = dto.Name,
            VendorInventory = dto.VendorInventory
                .Select(price => new VendorPrice { ItemId = price.ItemId, BuyPrice = price.BuyPrice, SellPrice = price.SellPrice })
                .ToList(),
            Npcs = dto.Npcs.Select(ToEntity).ToList()
        };
    }

    private static TownNpc ToEntity(TownNpcDto dto)
    {
        return new TownNpc
        {
            Id = dto.Id,
            Name = dto.Name,
            Role = dto.Role,
            RoleType = dto.RoleType,
            IsVendor = dto.IsVendor,
            Personality = dto.Personality,
            Location = dto.Location,
            QuestsOffered = dto.QuestsOffered.ToList(),
            Dialogue = new NpcDialogueTemplate
            {
                Greetings = dto.Dialogue.Greetings.ToList(),
                QuestOffers = dto.Dialogue.QuestOffers.ToList(),
                Farewells = dto.Dialogue.Farewells.ToList(),
                RandomLines = dto.Dialogue.RandomLines.ToList(),
                TradeOpeners = dto.Dialogue.TradeOpeners.ToList()
            }
        };
    }

    private static WorldLocationNode ToEntity(AdminWorldLocationDto dto)
    {
        return new WorldLocationNode
        {
            Id = dto.Id,
            Name = dto.Name,
            Description = dto.Description,
            Biome = dto.Biome,
            ThreatLevel = dto.ThreatLevel,
            AdjacentLocationIds = dto.AdjacentLocationIds.ToList(),
            TownName = dto.TownName
        };
    }

    private static DropTable ToEntity(DropTableDto dto)
    {
        return new DropTable
        {
            Biome = dto.Biome,
            Drops = dto.Drops.ToList()
        };
    }

    private async Task<(bool Success, string? Error)> AuthorizeAsync(string token, CancellationToken cancellationToken)
    {
        var auth = await getCurrentUserHandler.HandleAsync(token, cancellationToken);
        return (auth.Success, auth.Error);
    }

    private async Task<WorldStateData> LoadWorldAsync(CancellationToken cancellationToken)
    {
        var towns = (await worldRepository.GetTownsAsync(cancellationToken)).ToList();
        var monsters = (await worldRepository.GetMonstersAsync(cancellationToken)).ToList();
        var presets = (await worldRepository.GetCharacterPresetsAsync(cancellationToken)).ToList();
        var locations = (await worldRepository.GetLocationsAsync(cancellationToken)).ToList();
        var dropTables = (await worldRepository.GetDropTablesAsync(cancellationToken)).ToList();
        return new WorldStateData(towns, monsters, presets, locations, dropTables);
    }

    private Task SaveWorldAsync(WorldStateData state, CancellationToken cancellationToken)
    {
        return worldRepository.SaveWorldAsync(
            state.Towns,
            state.Monsters,
            state.CharacterPresets,
            state.Locations,
            state.DropTables,
            cancellationToken);
    }

    private sealed record WorldStateData(
        List<Town> Towns,
        List<Monster> Monsters,
        List<CharacterPreset> CharacterPresets,
        List<WorldLocationNode> Locations,
        List<DropTable> DropTables);
}
