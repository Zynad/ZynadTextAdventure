using ApplicationServices.Mapping;
using Domain.Entities.Weapons.Models;

namespace ApplicationServices.Items.Equipment.Weapons.BaseWeapons;
public class Staff : WeaponBase
{
    public Staff()
    {
        WeaponType = WeaponType.Staff;
    }
    public static implicit operator Staff(StaffEntity entity)
    {
        return new Staff
        {
            Name = entity.Name,
            Rarity = Mapper.MapToModel(entity.Rarity),
            Material = Mapper.MapToModel(entity.Material),
            WeaponType = Mapper.MapToModel(entity.WeaponType),
            ArmorValue = entity.ArmorValue,
            MeleeAttackValue = entity.MeleeAttackValue,
            RangedAttackValue = entity.RangedAttackValue,
            MagicAttackValue = entity.MagicAttackValue,
            IsRanged = entity.IsRanged,
            TwoHanded = entity.TwoHanded,
            Range = entity.Range,
            MagicPower = entity.MagicPower
        };
    }
    public static implicit operator StaffEntity(Staff model)
    {
        return new StaffEntity
        {
            Id = Guid.NewGuid(),
            Name = model.Name,
            Rarity = Mapper.MapToEntity(model.Rarity),
            Material = Mapper.MapToEntity(model.Material),
            WeaponType = Mapper.MapToEntity(model.WeaponType),
            ArmorValue = model.ArmorValue,
            MeleeAttackValue = model.MeleeAttackValue,
            RangedAttackValue = model.RangedAttackValue,
            MagicAttackValue = model.MagicAttackValue,
            IsRanged = model.IsRanged,
            TwoHanded = model.TwoHanded,
            Range = model.Range,
            MagicPower = model.MagicPower
        };
    }
}
