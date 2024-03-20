using TextAdventure.Items.Equipment.Weapons.BaseWeapons;

namespace TextAdventure.Repos.Weapons.Models;
public class SwordEntity : WeaponBaseEntity
{
    public static implicit operator Sword(SwordEntity entity)
    {
        return new Sword
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
