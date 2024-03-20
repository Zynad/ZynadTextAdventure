using TextAdventure.Items.Equipment.Armor;

namespace TextAdventure.Repos.Armor.Models;

public class HelmetEntity : ArmorPieceEntity
{
    public static implicit operator Helmet(HelmetEntity entity)
    {
        return new Helmet
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

