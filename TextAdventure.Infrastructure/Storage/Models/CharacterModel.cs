using Domain.Core;
using Domain.ValueObjects;

namespace TextAdventure.Infrastructure.Storage.Models;

public class CharacterModel
{
    public Guid Id { get; set; }

    public Guid AccountId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string PresetId { get; set; } = string.Empty;

    public string ClassName { get; set; } = string.Empty;

    public int Level { get; set; } = 1;

    public decimal Coins { get; set; } = 20;

    public CharacterStats Stats { get; set; } = CharacterStats.Default();

    public WorldLocationModel Location { get; set; } = new();

    public List<InventoryItem> Inventory { get; set; } = [];

    public List<CharacterQuestState> QuestStates { get; set; } = [];

    public List<Encounter> EncounterLog { get; set; } = [];

    public List<CharacterActionLogEntry> ActionLog { get; set; } = [];
}
