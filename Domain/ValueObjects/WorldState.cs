using Domain.Core;

namespace Domain.ValueObjects;

public class WorldState
{
    public List<Town> Towns { get; set; } = new();

    public List<Monster> Monsters { get; set; } = new();

    public List<CharacterPreset> CharacterPresets { get; set; } = new();

    public List<WorldLocationNode> Locations { get; set; } = new();

    public List<DropTable> DropTables { get; set; } = new();
}
