namespace TextAdventure.Api.Models.State;

public class AdventureState
{
    public string CurrentLocation { get; set; } = "Town";

    public int Health { get; set; } = 100;

    public int Mana { get; set; } = 50;

    public List<string> Inventory { get; set; } = new();

    public List<string> CompletedQuests { get; set; } = new();

    public List<string> DefeatedMonsters { get; set; } = new();
}
