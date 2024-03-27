using ApplicationServices.Items.Equipment.Weapons.BaseWeapons;
using ApplicationServices.Mapping;
using Domain.Entities.Weapons.Models;

namespace ApplicationServices.Items.Equipment.Weapons;
public class WeaponBase : EquipmentBase
{
    public WeaponMaterial Material { get; set; }
    public WeaponType WeaponType { get; set; }
    public int ArmorValue { get; set; }
    public int MeleeAttackValue { get; set; }
    public int RangedAttackValue { get; set; }
    public int MagicAttackValue { get; set; }
    public bool IsRanged { get; set; }
    public bool TwoHanded { get; set; }
    public int Range { get; set; }
    public int MagicPower { get; set; }

    public static implicit operator WeaponBaseEntity(WeaponBase weaponBase)
    {
        return new WeaponBaseEntity
        {
            Id = Guid.NewGuid(),
            Material = EnumMapper.MapToEntity(weaponBase.Material),
            WeaponType = EnumMapper.MapToEntity(weaponBase.WeaponType),
            Rarity = EnumMapper.MapToEntity(weaponBase.Rarity),
            ArmorValue = weaponBase.ArmorValue,
            MeleeAttackValue = weaponBase.MeleeAttackValue,
            RangedAttackValue = weaponBase.RangedAttackValue,
            MagicAttackValue = weaponBase.MagicAttackValue,
            IsRanged = weaponBase.IsRanged,
            TwoHanded = weaponBase.TwoHanded,
            Range = weaponBase.Range,
            MagicPower = weaponBase.MagicPower,
            Durability = weaponBase.Durability,
            Name = weaponBase.Name,
        };
    }

    public static implicit operator WeaponBase(WeaponBaseEntity entity)
    {
        return new WeaponBase
        {
            Material = EnumMapper.MapToModel(entity.Material),
            WeaponType = EnumMapper.MapToModel(entity.WeaponType),
            Rarity = EnumMapper.MapToModel(entity.Rarity),
            ArmorValue = entity.ArmorValue,
            MeleeAttackValue = entity.MeleeAttackValue,
            RangedAttackValue = entity.RangedAttackValue,
            MagicAttackValue = entity.MagicAttackValue,
            IsRanged = entity.IsRanged,
            TwoHanded = entity.TwoHanded,
            Range = entity.Range,
            MagicPower = entity.MagicPower,
            Durability = entity.Durability,
            Name = entity.Name,
        };
    }
}
