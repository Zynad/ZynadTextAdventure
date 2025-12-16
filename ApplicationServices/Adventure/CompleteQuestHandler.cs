using System.Linq;
using ApplicationServices.Adventure.Requests;
using ApplicationServices.Adventure.Results;
using ApplicationServices.Adventure.State;
using ApplicationServices.Authentication;
using ApplicationServices.Contracts.Repositories;
using Domain.Core;
using Domain.ValueObjects;
using Microsoft.Extensions.Logging;

namespace ApplicationServices.Adventure;

public class CompleteQuestHandler
{
    private readonly GetCurrentUserHandler _getCurrentUserHandler;
    private readonly IQuestRepository _questRepository;
    private readonly ICharacterRepository _characterRepository;
    private readonly IWorldRepository _worldRepository;
    private readonly ILogger<CompleteQuestHandler> _logger;

    public CompleteQuestHandler(
        GetCurrentUserHandler getCurrentUserHandler,
        IQuestRepository questRepository,
        ICharacterRepository characterRepository,
        IWorldRepository worldRepository,
        ILogger<CompleteQuestHandler> logger)
    {
        _getCurrentUserHandler = getCurrentUserHandler;
        _questRepository = questRepository;
        _characterRepository = characterRepository;
        _worldRepository = worldRepository;
        _logger = logger;
    }

    public async Task<AdventureResult> HandleAsync(
        string token,
        string questId,
        QuestActionRequest request,
        CancellationToken cancellationToken = default)
    {
        var userResult = await _getCurrentUserHandler.HandleAsync(token, cancellationToken);
        if (!userResult.Success || userResult.User is null)
        {
            return AdventureResult.Unauthorized(userResult.Error ?? "Unauthorized");
        }

        if (string.IsNullOrWhiteSpace(questId))
        {
            return AdventureResult.Validation("Quest id is required");
        }

        var quest = await _questRepository.GetByIdAsync(questId, cancellationToken);
        if (quest is null)
        {
            return AdventureResult.NotFound("Quest not found");
        }

        var character = await _characterRepository.GetByIdAsync(request.CharacterId, cancellationToken);
        if (character is null || character.AccountId != userResult.User.Id)
        {
            return AdventureResult.NotFound("Character not found");
        }

        var questState = character.QuestStates.FirstOrDefault(q =>
            q.QuestId.Equals(quest.Id, StringComparison.OrdinalIgnoreCase));

        if (questState is null)
        {
            return AdventureResult.Validation("Quest has not been accepted");
        }

        if (questState.Status == QuestProgressStatus.Completed)
        {
            return AdventureResult.Conflict("Quest already completed");
        }

        if (!string.IsNullOrWhiteSpace(quest.CompletionLocationId))
        {
            var locations = await _worldRepository.GetLocationsAsync(cancellationToken);
            var isAtLocation = locations.Any(l =>
                l.Id.Equals(quest.CompletionLocationId, StringComparison.OrdinalIgnoreCase)
                && l.Name.Equals(character.Location.Name, StringComparison.OrdinalIgnoreCase));

            if (!isAtLocation)
            {
                return AdventureResult.Validation("You must be at the quest location to complete it");
            }
        }

        questState.Status = QuestProgressStatus.Completed;
        questState.UpdatedAt = DateTimeOffset.UtcNow;

        if (quest.RewardItems.Any())
        {
            GrantRewards(character, quest.RewardItems);
        }

        await _characterRepository.UpdateAsync(character, cancellationToken);

        _logger.LogInformation(
            "Character {CharacterId} completed quest {QuestId}",
            character.Id,
            quest.Id);

        return AdventureResult.FromSuccess(CharacterStateMapper.FromCharacter(character));
    }

    private static void GrantRewards(Character character, IEnumerable<InventoryItem> rewards)
    {
        foreach (var reward in rewards)
        {
            var existing = character.Inventory.FirstOrDefault(i =>
                i.ItemId.Equals(reward.ItemId, StringComparison.OrdinalIgnoreCase));

            if (existing is null)
            {
                character.Inventory.Add(new InventoryItem { ItemId = reward.ItemId, Quantity = reward.Quantity });
            }
            else
            {
                existing.Quantity += reward.Quantity;
            }
        }
    }
}
