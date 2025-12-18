using ApplicationServices.Admin.Models;
using ApplicationServices.Authentication;
using ApplicationServices.Contracts.Repositories;
using Domain.Core;
using Domain.ValueObjects;

namespace ApplicationServices.Admin;

public class AdminQuestService(
    GetCurrentUserHandler getCurrentUserHandler,
    IQuestRepository questRepository) : IAdminQuestService
{
    public async Task<AdminOperationResult<IReadOnlyCollection<AdminQuestDto>>> GetAllAsync(
        string token,
        CancellationToken cancellationToken = default)
    {
        var authorization = await AuthorizeAsync(token, cancellationToken);
        if (!authorization.Success)
        {
            return AdminOperationResult<IReadOnlyCollection<AdminQuestDto>>.Unauthorized(
                authorization.Error ?? "Unauthorized");
        }

        var quests = await questRepository.GetAllAsync(cancellationToken);
        var payload = quests.Select(ToDto).ToList();
        return AdminOperationResult<IReadOnlyCollection<AdminQuestDto>>.FromSuccess(payload);
    }

    public async Task<AdminOperationResult<AdminQuestDto>> CreateAsync(
        string token,
        AdminQuestDto questDto,
        CancellationToken cancellationToken = default)
    {
        var authorization = await AuthorizeAsync(token, cancellationToken);
        if (!authorization.Success)
        {
            return AdminOperationResult<AdminQuestDto>.Unauthorized(authorization.Error ?? "Unauthorized");
        }

        if (string.IsNullOrWhiteSpace(questDto.Name))
        {
            return AdminOperationResult<AdminQuestDto>.ValidationFailed("Name is required");
        }

        var questId = string.IsNullOrWhiteSpace(questDto.Id) ? Guid.NewGuid().ToString() : questDto.Id;
        var existing = await questRepository.GetByIdAsync(questId, cancellationToken);
        if (existing is not null)
        {
            return AdminOperationResult<AdminQuestDto>.Conflict("Quest already exists");
        }

        var entity = ToEntity(questDto) with { Id = questId };
        await questRepository.AddAsync(entity, cancellationToken);

        return AdminOperationResult<AdminQuestDto>.FromSuccess(ToDto(entity));
    }

    public async Task<AdminOperationResult<AdminQuestDto>> UpdateAsync(
        string token,
        string questId,
        AdminQuestDto questDto,
        CancellationToken cancellationToken = default)
    {
        var authorization = await AuthorizeAsync(token, cancellationToken);
        if (!authorization.Success)
        {
            return AdminOperationResult<AdminQuestDto>.Unauthorized(authorization.Error ?? "Unauthorized");
        }

        var existing = await questRepository.GetByIdAsync(questId, cancellationToken);
        if (existing is null)
        {
            return AdminOperationResult<AdminQuestDto>.NotFound("Quest not found");
        }

        var updated = ToEntity(questDto) with { Id = questId };
        await questRepository.UpdateAsync(updated, cancellationToken);

        return AdminOperationResult<AdminQuestDto>.FromSuccess(ToDto(updated));
    }

    public async Task<AdminOperationResult<bool>> DeleteAsync(
        string token,
        string questId,
        CancellationToken cancellationToken = default)
    {
        var authorization = await AuthorizeAsync(token, cancellationToken);
        if (!authorization.Success)
        {
            return AdminOperationResult<bool>.Unauthorized(authorization.Error ?? "Unauthorized");
        }

        var existing = await questRepository.GetByIdAsync(questId, cancellationToken);
        if (existing is null)
        {
            return AdminOperationResult<bool>.NotFound("Quest not found");
        }

        await questRepository.DeleteAsync(questId, cancellationToken);
        return AdminOperationResult<bool>.FromSuccess(true);
    }

    private static AdminQuestDto ToDto(Quest quest)
    {
        return new AdminQuestDto(
            quest.Id,
            quest.Name,
            quest.Description,
            quest.TownName,
            quest.AcceptLocationId,
            quest.CompletionLocationId,
            quest.PrerequisiteQuestIds,
            quest.ExperienceReward,
            quest.CoinReward,
            quest.RewardItems.Select(item => new QuestRewardItemDto(item.ItemId, item.Quantity)).ToList());
    }

    private static Quest ToEntity(AdminQuestDto dto)
    {
        return new Quest
        {
            Id = dto.Id,
            Name = dto.Name,
            Description = dto.Description,
            TownName = dto.TownName,
            AcceptLocationId = dto.AcceptLocationId,
            CompletionLocationId = dto.CompletionLocationId,
            PrerequisiteQuestIds = dto.PrerequisiteQuestIds.ToList(),
            ExperienceReward = dto.ExperienceReward,
            CoinReward = dto.CoinReward,
            RewardItems = dto.RewardItems
                .Select(item => new InventoryItem { ItemId = item.ItemId, Quantity = item.Quantity })
                .ToList()
        };
    }

    private async Task<(bool Success, string? Error)> AuthorizeAsync(string token, CancellationToken cancellationToken)
    {
        var auth = await getCurrentUserHandler.HandleAsync(token, cancellationToken);
        return (auth.Success, auth.Error);
    }
}
