namespace TextAdventure.Api.Models;

public class Monster
{
    public Guid Id { get; set; } = Guid.NewGuid();

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public int Level { get; set; }

    public int HitPoints { get; set; }

    public int AttackPower { get; set; }
}
