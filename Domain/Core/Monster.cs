namespace Domain.Core;

public class Monster
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = string.Empty;
    public string Biome { get; set; } = string.Empty;
    public int Level { get; set; }
    public int HitPoints { get; set; }
    public int Attack { get; set; }
}
