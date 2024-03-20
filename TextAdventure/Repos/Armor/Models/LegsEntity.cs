using TextAdventure.Items.Equipment.Armor;

namespace TextAdventure.Repos.Armor.Models;

public class LegsEntity : ArmorPieceEntity
{
    public static implicit operator Legs(LegsEntity entity)
    {
        return new Legs
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

