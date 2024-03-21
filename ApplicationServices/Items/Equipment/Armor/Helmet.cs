using ApplicationServices.Mapping;
using Domain.Entities.Armor.Models;

namespace ApplicationServices.Items.Equipment.Armor;
public class Helmet : ArmorPiece
{
    public static implicit operator Helmet(HelmetEntity entity)
    {
        return new Helmet
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
