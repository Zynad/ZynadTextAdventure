using TextAdventure.Items.Equipment.Armor;

namespace TextAdventure.Repos.Armor.Models;

public class GlovesEntity : ArmorPieceEntity
{
    public static implicit operator Gloves(GlovesEntity entity)
    {
        return new Gloves
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

