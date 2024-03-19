using TextAdventure.Game;

namespace TextAdventure.Equipment;
public abstract class EquipmentBase
{
    public string Name { get; set; }
    public int LevelRequirement { get; set; }
    public Rarity Rarity { get; set; }
}
