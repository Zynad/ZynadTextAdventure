using ApplicationServices.Mapping;
using Domain.Entities.Weapons.Models;

namespace ApplicationServices.Items.Equipment.Weapons.BaseWeapons;
public class Axe : WeaponBase
{
    public Axe()
    {
        WeaponType = WeaponType.Axe;
    }

    public static implicit operator Axe(AxeEntity entity)
    {
        return new Axe
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
