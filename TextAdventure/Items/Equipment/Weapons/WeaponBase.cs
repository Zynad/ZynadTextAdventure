using TextAdventure.Repos.Weapons.Models;

namespace TextAdventure.Items.Equipment.Weapons;
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
            Material = weaponBase.Material,
            WeaponType = weaponBase.WeaponType,
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
            Material = entity.Material,
            WeaponType = entity.WeaponType,
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
