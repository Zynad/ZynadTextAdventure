using System.Linq;
using ApplicationServices.Adventure.Requests;
using ApplicationServices.Adventure.Results;
using ApplicationServices.Authentication;
using ApplicationServices.Characters;
using ApplicationServices.Contracts.Repositories;
using Domain.Core;
using Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace ApplicationServices.Adventure;

public class TravelToLocationHandler(
    GetCurrentUserHandler getCurrentUserHandler,
    ICharacterRepository characterRepository,
    IWorldRepository worldRepository,
    EncounterGenerator encounterGenerator,
    ILogger<TravelToLocationHandler> logger)
{
    public async Task<AdventureResult> HandleAsync(
        string token,
        TravelRequest request,
        CancellationToken cancellationToken = default)
    {
        var userResult = await getCurrentUserHandler.HandleAsync(token, cancellationToken);
        if (!userResult.Success || userResult.User is null)
        {
            return AdventureResult.Unauthorized(userResult.Error ?? "Unauthorized");
        }

        var character = await characterRepository.GetByIdAsync(request.CharacterId, cancellationToken);
        if (character is null || character.AccountId != userResult.User.Id)
        {
            return AdventureResult.NotFound("Character not found");
        }

        character.EncounterLog ??= [];

        if (string.IsNullOrWhiteSpace(request.DestinationId))
        {
            return AdventureResult.Validation("DestinationId is required");
        }

        var locations = await worldRepository.GetLocationsAsync(cancellationToken);
        var target = locations.FirstOrDefault(l => l.Id.Equals(request.DestinationId, StringComparison.OrdinalIgnoreCase));
        if (target is null)
        {
            return AdventureResult.Validation("Destination does not exist");
        }

        var current = FindCurrentLocation(locations, character.Location);
        if (current is null)
        {
            return AdventureResult.Validation("Current location is not part of the world map");
        }

        var isAdjacent = current.AdjacentLocationIds.Any(id => id.Equals(target.Id, StringComparison.OrdinalIgnoreCase));
        if (!isAdjacent)
        {
            return AdventureResult.Conflict("Destination is not adjacent to the current location");
        }

        character.Location = new WorldLocation
        {
            Name = target.Name,
            Biome = target.Biome,
            ThreatLevel = target.ThreatLevel
        };

        var encounter = await encounterGenerator.GenerateForTravelAsync(character, current, target, cancellationToken);
        if (encounter is not null)
        {
            ApplyEncounter(character, encounter);
        }

        await characterRepository.UpdateAsync(character, cancellationToken);
        logger.LogInformation(
            "Character {CharacterId} traveled from {From} to {To}",
            character.Id,
            current.Name,
            target.Name);

        return AdventureResult.FromSuccess(CharacterMapper.ToStateDto(character));
    }

    private static WorldLocationNode? FindCurrentLocation(IEnumerable<WorldLocationNode> locations, WorldLocation currentLocation)
    {
        return locations.FirstOrDefault(l =>
            l.Id.Equals(currentLocation.Name, StringComparison.OrdinalIgnoreCase)
            || l.Name.Equals(currentLocation.Name, StringComparison.OrdinalIgnoreCase));
    }

    private static void ApplyEncounter(Character character, EncounterResolution resolution)
    {
        foreach (var drop in resolution.Loot)
        {
            var existing = character.Inventory.FirstOrDefault(i =>
                i.ItemId.Equals(drop.ItemId, StringComparison.OrdinalIgnoreCase));

            if (existing is null)
            {
                character.Inventory.Add(new InventoryItem { ItemId = drop.ItemId, Quantity = drop.Quantity });
            }
            else
            {
                existing.Quantity += drop.Quantity;
            }
        }

        character.EncounterLog.Add(resolution.Encounter);
        if (character.EncounterLog.Count > 25)
        {
            character.EncounterLog = character.EncounterLog
                .OrderByDescending(e => e.OccurredAt)
                .Take(25)
                .ToList();
        }
    }
}
