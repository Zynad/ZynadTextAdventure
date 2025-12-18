using Domain.Core;

namespace Domain.ValueObjects;

public class WorldState
{
    public List<Town> Towns { get; set; } = [];

    public List<Monster> Monsters { get; set; } = [];

    public List<CharacterPreset> CharacterPresets { get; set; } = [];

    public List<WorldLocationNode> Locations { get; set; } = [];

    public List<DropTable> DropTables { get; set; } = [];
}
