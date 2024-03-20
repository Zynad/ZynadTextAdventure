using TextAdventure.Items.Equipment.Weapons.BaseWeapons;

namespace TextAdventure.Repos.Weapons.Models;
public class AxeEntity : WeaponBaseEntity
{
    public static implicit operator Axe(AxeEntity entity)
    {
        return new Axe
        {
            Name = entity.Name,
            Rarity = entity.Rarity,
            Material = entity.Material,
            WeaponType = entity.WeaponType,
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
