using ApplicationServices.Mapping;
using Domain.Entities.Armor.Models;

namespace ApplicationServices.Items.Equipment.Armor;
public class Chest : ArmorPiece
{
    public static implicit operator Chest(ChestEntity entity)
    {
        return new Chest
        {
            Name = entity.Name,
            Material = Mapper.MapToModel(entity.Material),
            ArmorValue = entity.ArmorValue,
            Durability = entity.Durability,
            Weight = entity.Weight,
            LevelRequirement = entity.LevelRequirement,
            Rarity = Mapper.MapToModel(entity.Rarity),
            Value = entity.Value
        };
    }
}
