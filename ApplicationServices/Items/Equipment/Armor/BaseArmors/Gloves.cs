using ApplicationServices.Mapping;
using Domain.Entities.Armor.Models;

namespace ApplicationServices.Items.Equipment.Armor.BaseArmors;
public class Gloves : ArmorPiece
{
    public static implicit operator Gloves(GlovesEntity entity)
    {
        return new Gloves
        {
            Name = entity.Name,
            Material = EnumMapper.MapToModel(entity.Material),
            ArmorValue = entity.ArmorValue,
            Durability = entity.Durability,
            Weight = entity.Weight,
            LevelRequirement = entity.LevelRequirement,
            Rarity = EnumMapper.MapToModel(entity.Rarity),
            Value = entity.Value
        };
    }

    public static implicit operator GlovesEntity(Gloves model)
    {
        return new GlovesEntity
        {
            Id = Guid.NewGuid(),
            Name = model.Name,
            Material = EnumMapper.MapToEntity(model.Material),
            ArmorValue = model.ArmorValue,
            Durability = model.Durability,
            Weight = model.Weight,
            LevelRequirement = model.LevelRequirement,
            Rarity = EnumMapper.MapToEntity(model.Rarity),
            Value = model.Value
        };
    }
}
