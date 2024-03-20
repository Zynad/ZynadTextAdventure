using TextAdventure.Items.Equipment.Armor;

namespace TextAdventure.Repos.Armor.Models;

public class ChestEntity : ArmorPieceEntity
{
    public static implicit operator Chest(ChestEntity entity)
    {
        return new Chest
        {
            Name = entity.Name,
            Material = entity.Material,
            ArmorValue = entity.ArmorValue,
            Durability = entity.Durability,
            Weight = entity.Weight,
            LevelRequirement = entity.LevelRequirement,
            Rarity = entity.Rarity,
            Value = entity.Value
        };
    }
}

