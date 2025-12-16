namespace Domain.Core;

public class MonsterStatRange
{
    public int Min { get; set; }
    public int Max { get; set; }
}

public class Monster
{
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; } = string.Empty;
    public string Biome { get; set; } = string.Empty;
    public MonsterStatRange LevelRange { get; set; } = new();
    public MonsterStatRange HitPointRange { get; set; } = new();
    public MonsterStatRange AttackRange { get; set; } = new();
    public MonsterStatRange DefenseRange { get; set; } = new();
    public MonsterStatRange CoinDropRange { get; set; } = new();
    public List<string> PreferredThreatLevels { get; set; } = [];
}
