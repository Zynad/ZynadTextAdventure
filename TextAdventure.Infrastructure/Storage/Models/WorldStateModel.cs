using Domain.Core;
using Domain.ValueObjects;

namespace TextAdventure.Infrastructure.Storage.Models;

public class WorldStateModel
{
    public List<Town> Towns { get; set; } = [];

    public List<Monster> Monsters { get; set; } = [];

    public List<CharacterPresetModel> CharacterPresets { get; set; } = [];

    public List<WorldLocationNodeModel> Locations { get; set; } = [];

    public List<DropTable> DropTables { get; set; } = [];
}
