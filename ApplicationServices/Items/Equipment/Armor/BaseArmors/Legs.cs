using ApplicationServices.Mapping;
using Domain.Entities.Armor.Models;

namespace ApplicationServices.Items.Equipment.Armor.BaseArmors;
public class Legs : ArmorPiece
{
    public static implicit operator Legs(LegsEntity entity)
    {
        return new Legs
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

    public static implicit operator LegsEntity(Legs model)
    {
        return new LegsEntity
        {
            Id = Guid.NewGuid(),
            Name = model.Name,
            Material = Mapper.MapToEntity(model.Material),
            ArmorValue = model.ArmorValue,
            Durability = model.Durability,
            Weight = model.Weight,
            LevelRequirement = model.LevelRequirement,
            Rarity = Mapper.MapToEntity(model.Rarity),
            Value = model.Value
        };
    }
}
