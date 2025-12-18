namespace Domain.ValueObjects;

public class WorldLocation
{
    public string Name { get; set; } = "Unknown";

    public string Biome { get; set; } = "Unknown";

    public string ThreatLevel { get; set; } = "Calm";

    public static WorldLocation Default()
    {
        return new WorldLocation
        {
            Name = "Traveler's Road",
            Biome = "Grassland",
            ThreatLevel = "Low"
        };
    }
}
