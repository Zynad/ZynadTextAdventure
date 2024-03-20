using TextAdventure.Items.Equipment.Armor;

namespace TextAdventure.Repos.Armor.Models;

public class BootsEntity : ArmorPieceEntity
{
    public static implicit operator Boots(BootsEntity entity)
    {
        return new Boots
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

