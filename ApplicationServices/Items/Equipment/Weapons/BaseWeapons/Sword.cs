using ApplicationServices.Mapping;
using Domain.Entities.Weapons.Models;

namespace ApplicationServices.Items.Equipment.Weapons.BaseWeapons;
public class Sword : WeaponBase
{
    public Sword()
    {
        WeaponType = WeaponType.Sword;
    }

    public static implicit operator Sword(SwordEntity entity)
    {
        return new Sword
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
}
