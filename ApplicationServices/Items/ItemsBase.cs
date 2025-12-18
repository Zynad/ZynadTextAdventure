namespace ApplicationServices.Items;
public abstract class ItemsBase
{
    public string Name { get; set; } = string.Empty;
    public int LevelRequirement { get; set; }
    public Rarity Rarity { get; set; }
    public int Value { get; set; }
    public int Weight { get; set; }
}
