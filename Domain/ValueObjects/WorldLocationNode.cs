namespace Domain.ValueObjects;

public class WorldLocationNode
{
    public string Id { get; set; } = string.Empty;

    public string Name { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Biome { get; set; } = string.Empty;

    public string ThreatLevel { get; set; } = string.Empty;

    public List<string> AdjacentLocationIds { get; set; } = [];

    public string? TownName { get; set; }
}
