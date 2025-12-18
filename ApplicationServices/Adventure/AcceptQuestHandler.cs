using System.Linq;
using ApplicationServices.Adventure.Requests;
using ApplicationServices.Adventure.Results;
using ApplicationServices.Authentication;
using ApplicationServices.Characters;
using ApplicationServices.Contracts.Repositories;
using Domain.Core;
using Microsoft.Extensions.Logging;

namespace ApplicationServices.Adventure;

public class AcceptQuestHandler
{
    private readonly GetCurrentUserHandler _getCurrentUserHandler;
    private readonly IQuestRepository _questRepository;
    private readonly ICharacterRepository _characterRepository;
    private readonly IWorldRepository _worldRepository;
    private readonly ILogger<AcceptQuestHandler> _logger;

    public AcceptQuestHandler(
        GetCurrentUserHandler getCurrentUserHandler,
        IQuestRepository questRepository,
        ICharacterRepository characterRepository,
        IWorldRepository worldRepository,
        ILogger<AcceptQuestHandler> logger)
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

        var existingQuestState = character.QuestStates.FirstOrDefault(q =>
            q.QuestId.Equals(quest.Id, StringComparison.OrdinalIgnoreCase));
        if (existingQuestState is not null)
        {
            return AdventureResult.Conflict(existingQuestState.Status == QuestProgressStatus.Completed
                ? "Quest already completed"
                : "Quest already accepted");
        }

        if (quest.PrerequisiteQuestIds.Any())
        {
            var completedQuestIds = character.QuestStates
                .Where(q => q.Status == QuestProgressStatus.Completed)
                .Select(q => q.QuestId)
                .ToHashSet(StringComparer.OrdinalIgnoreCase);

            var missingPrereq = quest.PrerequisiteQuestIds
                .FirstOrDefault(id => !completedQuestIds.Contains(id));

            if (!string.IsNullOrEmpty(missingPrereq))
            {
                return AdventureResult.Validation("Prerequisite quests are not complete");
            }
        }

        if (!string.IsNullOrWhiteSpace(quest.AcceptLocationId))
        {
            var locations = await _worldRepository.GetLocationsAsync(cancellationToken);
            var isAtLocation = locations.Any(l =>
                l.Id.Equals(quest.AcceptLocationId, StringComparison.OrdinalIgnoreCase)
                && l.Name.Equals(character.Location.Name, StringComparison.OrdinalIgnoreCase));

            if (!isAtLocation)
            {
                return AdventureResult.Validation("You must be at the quest location to accept it");
            }
        }

        var questState = new CharacterQuestState
        {
            QuestId = quest.Id,
            Status = QuestProgressStatus.Accepted,
            UpdatedAt = DateTimeOffset.UtcNow
        };

        character.QuestStates.Add(questState);
        await _characterRepository.UpdateAsync(character, cancellationToken);

        _logger.LogInformation(
            "Character {CharacterId} accepted quest {QuestId}",
            character.Id,
            quest.Id);

        return AdventureResult.FromSuccess(CharacterMapper.ToStateDto(character));
    }
}
