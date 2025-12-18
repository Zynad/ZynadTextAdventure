using Domain.Core;
using Domain.ValueObjects;

namespace TextAdventure.Infrastructure.Storage.Models;

public class WorldStateModel
{
    public List<Town> Towns { get; set; } = new();

    public List<Monster> Monsters { get; set; } = new();

    public List<CharacterPresetModel> CharacterPresets { get; set; } = new();

    public List<WorldLocationNodeModel> Locations { get; set; } = new();

    public List<DropTable> DropTables { get; set; } = new();
}
