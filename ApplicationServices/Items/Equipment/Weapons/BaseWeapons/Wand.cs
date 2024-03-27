using ApplicationServices.Mapping;
using Domain.Entities.Weapons.Models;

namespace ApplicationServices.Items.Equipment.Weapons.BaseWeapons;
public class Wand : WeaponBase
{
    public Wand()
    {
        WeaponType = WeaponType.Wand;
    }

    public static implicit operator Wand(WandEntity entity)
    {
        return new Wand
        {
            Name = entity.Name,
            Rarity = EnumMapper.MapToModel(entity.Rarity),
            Material = EnumMapper.MapToModel(entity.Material),
            WeaponType = EnumMapper.MapToModel(entity.WeaponType),
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

    public static implicit operator WandEntity(Wand wand)
    {
        return new WandEntity
        {
            Id = Guid.NewGuid(),
            Name = wand.Name,
            Rarity = EnumMapper.MapToEntity(wand.Rarity),
            Material = EnumMapper.MapToEntity(wand.Material),
            WeaponType = EnumMapper.MapToEntity(wand.WeaponType),
            ArmorValue = wand.ArmorValue,
            MeleeAttackValue = wand.MeleeAttackValue,
            RangedAttackValue = wand.RangedAttackValue,
            MagicAttackValue = wand.MagicAttackValue,
            IsRanged = wand.IsRanged,
            TwoHanded = wand.TwoHanded,
            Range = wand.Range,
            MagicPower = wand.MagicPower
        };
    }
}
