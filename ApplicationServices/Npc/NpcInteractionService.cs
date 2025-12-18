using System.Linq;
using ApplicationServices.Authentication;
using ApplicationServices.Characters;
using ApplicationServices.Characters.Dto;
using ApplicationServices.Contracts.Repositories;
using ApplicationServices.Contracts.Services;
using ApplicationServices.Npc.Models;
using Domain.Core;
using Domain.ValueObjects;

namespace ApplicationServices.Npc;

public enum NpcActionType
{
    Combat,
    Stealth,
    Pickpocket
}

public enum TradeAction
{
    Buy,
    Sell
}

public class NpcInteractionService(
    GetCurrentUserHandler getCurrentUserHandler,
    IWorldRepository worldRepository,
    ICharacterRepository characterRepository,
    IVendorPricingService vendorPricingService,
    IRandomService randomService)
{
    public async Task<NpcInteractionResult<NpcDialogueResponse>> GetDialogueAsync(
        string token,
        Guid characterId,
        string npcId,
        CancellationToken cancellationToken = default)
    {
        var resolution = await ResolveCharacterAndNpcAsync(token, characterId, npcId, cancellationToken);
        if (!resolution.Success)
        {
            return NpcInteractionResult<NpcDialogueResponse>.Failure(resolution.Error ?? "Unable to load NPC");
        }

        var character = resolution.Character;
        var npc = resolution.Npc;
        var town = resolution.Town;

        var candidateLines = new List<string>();
        candidateLines.AddRange(npc.Dialogue.Greetings);
        candidateLines.AddRange(npc.Dialogue.RandomLines);

        if (npc.IsVendor && npc.Dialogue.TradeOpeners.Count > 0)
        {
            candidateLines.AddRange(npc.Dialogue.TradeOpeners);
        }

        if (npc.RoleType == NpcRoleType.Guard && npc.Dialogue.QuestOffers.Count > 0)
        {
            candidateLines.AddRange(npc.Dialogue.QuestOffers);
        }

        if (candidateLines.Count == 0)
        {
            candidateLines.Add($"Hello there, {character.Name}.");
        }

        var selected = candidateLines[randomService.NextInt(0, candidateLines.Count)];
        var personalized = selected.Replace("{playerName}", character.Name);

        var payload = new NpcDialogueResponse(npc.Id, npc.Name, town.Name, personalized, npc.RoleType.ToString());
        return NpcInteractionResult<NpcDialogueResponse>.FromSuccess(payload, CharacterMapper.ToStateDto(character));
    }

    public async Task<NpcInteractionResult<NpcQuestOfferResponse>> OfferQuestAsync(
        string token,
        Guid characterId,
        string npcId,
        CancellationToken cancellationToken = default)
    {
        var resolution = await ResolveCharacterAndNpcAsync(token, characterId, npcId, cancellationToken);
        if (!resolution.Success)
        {
            return NpcInteractionResult<NpcQuestOfferResponse>.Failure(resolution.Error ?? "Unable to load NPC");
        }

        var character = resolution.Character;
        var npc = resolution.Npc;
        var town = resolution.Town;

        if (npc.QuestsOffered.Count == 0)
        {
            return NpcInteractionResult<NpcQuestOfferResponse>.Failure("This NPC has no quests available.");
        }

        var questId = npc.QuestsOffered[randomService.NextInt(0, npc.QuestsOffered.Count)];
        var prompt = npc.Dialogue.QuestOffers.FirstOrDefault()
                     ?? $"{npc.Name} has a task for you, {character.Name}.";

        var payload = new NpcQuestOfferResponse(npc.Id, npc.Name, questId, prompt.Replace("{playerName}", character.Name));
        return NpcInteractionResult<NpcQuestOfferResponse>.FromSuccess(payload, CharacterMapper.ToStateDto(character));
    }

    public async Task<NpcInteractionResult<NpcTradeResponse>> TradeAsync(
        string token,
        Guid characterId,
        string npcId,
        string itemId,
        int quantity,
        TradeAction action,
        CancellationToken cancellationToken = default)
    {
        var resolution = await ResolveCharacterAndNpcAsync(token, characterId, npcId, cancellationToken);
        if (!resolution.Success)
        {
            return NpcInteractionResult<NpcTradeResponse>.Failure(resolution.Error ?? "Unable to load NPC");
        }

        if (quantity <= 0)
        {
            return NpcInteractionResult<NpcTradeResponse>.Failure("Quantity must be positive.");
        }

        var character = resolution.Character;
        var npc = resolution.Npc;
        var town = resolution.Town;
        if (!npc.IsVendor)
        {
            return NpcInteractionResult<NpcTradeResponse>.Failure("NPC is not configured as a vendor.");
        }

        var price = await vendorPricingService.GetPriceForItemAsync(town.Name, itemId, cancellationToken);
        if (price is null)
        {
            return NpcInteractionResult<NpcTradeResponse>.Failure("Item not available from this vendor.");
        }

        var unitPrice = action == TradeAction.Buy ? price.BuyPrice : price.SellPrice;
        var totalPrice = unitPrice * quantity;
        var rounded = Math.Ceiling(totalPrice);

        if (action == TradeAction.Buy)
        {
            if (character.Coins < (decimal)rounded)
            {
                return NpcInteractionResult<NpcTradeResponse>.Failure("Not enough coins for this purchase.");
            }

            character.Coins -= (decimal)rounded;
            AddInventory(character.Inventory, itemId, quantity);
        }
        else
        {
            var current = character.Inventory.FirstOrDefault(i => i.ItemId.Equals(itemId, StringComparison.OrdinalIgnoreCase));
            if (current is null || current.Quantity < quantity)
            {
                return NpcInteractionResult<NpcTradeResponse>.Failure("Insufficient items to sell.");
            }

            current.Quantity -= quantity;
            character.Coins += (decimal)rounded;
        }

        await characterRepository.UpdateAsync(character, cancellationToken);
        var payload = new NpcTradeResponse(
            npc.Id,
            npc.Name,
            itemId,
            quantity,
            unitPrice,
            (decimal)rounded,
            character.Coins,
            action.ToString());

        return NpcInteractionResult<NpcTradeResponse>.FromSuccess(payload, CharacterMapper.ToStateDto(character));
    }

    public async Task<NpcInteractionResult<NpcActionResponse>> ResolveActionAsync(
        string token,
        Guid characterId,
        string npcId,
        NpcActionType actionType,
        int difficulty,
        CancellationToken cancellationToken = default)
    {
        var resolution = await ResolveCharacterAndNpcAsync(token, characterId, npcId, cancellationToken);
        if (!resolution.Success)
        {
            return NpcInteractionResult<NpcActionResponse>.Failure(resolution.Error ?? "Unable to load NPC");
        }

        var character = resolution.Character;
        var npc = resolution.Npc;

        var roll = randomService.NextInt(1, 21);
        var modifier = actionType switch
        {
            NpcActionType.Combat => character.Stats.Combat,
            NpcActionType.Stealth => character.Stats.Stealth,
            NpcActionType.Pickpocket => character.Stats.Pickpocket,
            _ => 0
        };

        var total = roll + modifier;
        var success = total >= difficulty;

        character.ActionLog.Add(new CharacterActionLogEntry
        {
            Action = actionType.ToString(),
            Target = npc.Name,
            Success = success,
            Roll = total,
            Difficulty = difficulty,
            OccurredAt = DateTimeOffset.UtcNow
        });

        await characterRepository.UpdateAsync(character, cancellationToken);

        var payload = new NpcActionResponse(
            npc.Id,
            npc.Name,
            actionType.ToString(),
            total,
            difficulty,
            modifier,
            success,
            character.Coins);

        return NpcInteractionResult<NpcActionResponse>.FromSuccess(payload, CharacterMapper.ToStateDto(character));
    }

    private async Task<(bool Success, string? Error, Character Character, TownNpc Npc, Town Town)> ResolveCharacterAndNpcAsync(
        string token,
        Guid characterId,
        string npcId,
        CancellationToken cancellationToken)
    {
        var userResult = await getCurrentUserHandler.HandleAsync(token, cancellationToken);
        if (!userResult.Success || userResult.User is null)
        {
            return (false, "Unauthorized", new Character(), new TownNpc(), new Town());
        }

        var character = await characterRepository.GetByIdAsync(characterId, cancellationToken);
        if (character is null || character.AccountId != userResult.User.Id)
        {
            return (false, "Character not found", new Character(), new TownNpc(), new Town());
        }

        var towns = await worldRepository.GetTownsAsync(cancellationToken);
        foreach (var town in towns)
        {
            var npc = town.Npcs.FirstOrDefault(n => n.Id.Equals(npcId, StringComparison.OrdinalIgnoreCase));
            if (npc is not null)
            {
                npc.Location = town.Name;
                return (true, null, character, npc, town);
            }
        }

        return (false, "Npc not found", new Character(), new TownNpc(), new Town());
    }

    private static void AddInventory(ICollection<InventoryItem> items, string itemId, int quantity)
    {
        var existing = items.FirstOrDefault(i => i.ItemId.Equals(itemId, StringComparison.OrdinalIgnoreCase));
        if (existing is null)
        {
            items.Add(new InventoryItem { ItemId = itemId, Quantity = quantity });
        }
        else
        {
            existing.Quantity += quantity;
        }
    }
}
