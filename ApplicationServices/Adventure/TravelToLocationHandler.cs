using System.Linq;
using ApplicationServices.Adventure.Requests;
using ApplicationServices.Adventure.Results;
using ApplicationServices.Adventure.State;
using ApplicationServices.Authentication;
using ApplicationServices.Contracts.Repositories;
using Domain.Core;
using Domain.Entities.Storage;
using Microsoft.Extensions.Logging;

namespace ApplicationServices.Adventure;

public class TravelToLocationHandler
{
    private readonly GetCurrentUserHandler _getCurrentUserHandler;
    private readonly ICharacterRepository _characterRepository;
    private readonly IWorldRepository _worldRepository;
    private readonly ILogger<TravelToLocationHandler> _logger;

    public TravelToLocationHandler(
        GetCurrentUserHandler getCurrentUserHandler,
        ICharacterRepository characterRepository,
        IWorldRepository worldRepository,
        ILogger<TravelToLocationHandler> logger)
    {
        _getCurrentUserHandler = getCurrentUserHandler;
        _characterRepository = characterRepository;
        _worldRepository = worldRepository;
        _logger = logger;
    }

    public async Task<AdventureResult> HandleAsync(
        string token,
        TravelRequest request,
        CancellationToken cancellationToken = default)
    {
        var userResult = await _getCurrentUserHandler.HandleAsync(token, cancellationToken);
        if (!userResult.Success || userResult.User is null)
        {
            return AdventureResult.Unauthorized(userResult.Error ?? "Unauthorized");
        }

        var character = await _characterRepository.GetByIdAsync(request.CharacterId, cancellationToken);
        if (character is null || character.AccountId != userResult.User.Id)
        {
            return AdventureResult.NotFound("Character not found");
        }

        if (string.IsNullOrWhiteSpace(request.DestinationId))
        {
            return AdventureResult.Validation("DestinationId is required");
        }

        var locations = await _worldRepository.GetLocationsAsync(cancellationToken);
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

        await _characterRepository.UpdateAsync(character, cancellationToken);
        _logger.LogInformation(
            "Character {CharacterId} traveled from {From} to {To}",
            character.Id,
            current.Name,
            target.Name);

        return AdventureResult.FromSuccess(CharacterStateMapper.FromCharacter(character));
    }

    private static WorldLocationNode? FindCurrentLocation(IEnumerable<WorldLocationNode> locations, WorldLocation currentLocation)
    {
        return locations.FirstOrDefault(l =>
            l.Id.Equals(currentLocation.Name, StringComparison.OrdinalIgnoreCase)
            || l.Name.Equals(currentLocation.Name, StringComparison.OrdinalIgnoreCase));
    }
}
