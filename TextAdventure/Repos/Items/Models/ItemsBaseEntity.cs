using TextAdventure.Game;

namespace TextAdventure.Repos.Items.Models;
public class ItemsBaseEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public int LevelRequirement { get; set; }
    public Rarity Rarity { get; set; }
    public int Value { get; set; }
    public int Weight { get; set; }
}
