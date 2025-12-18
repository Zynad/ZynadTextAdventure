using Domain.ValueObjects;

namespace Domain.Core;

public class Character
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public Guid AccountId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string PresetId { get; set; } = string.Empty;
    public string ClassName { get; set; } = string.Empty;
    public int Level { get; set; } = 1;
    public decimal Coins { get; set; } = 20;
    public CharacterStats Stats { get; set; } = CharacterStats.Default();
    public WorldLocation Location { get; set; } = WorldLocation.Default();
    public List<InventoryItem> Inventory { get; set; } = [];
    public List<CharacterQuestState> QuestStates { get; set; } = [];
    public List<Encounter> EncounterLog { get; set; } = [];
    public List<CharacterActionLogEntry> ActionLog { get; set; } = [];
}
