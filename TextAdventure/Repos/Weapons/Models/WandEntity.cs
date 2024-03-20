using TextAdventure.Items.Equipment.Weapons.BaseWeapons;

namespace TextAdventure.Repos.Weapons.Models;
public class WandEntity : WeaponsBaseEntity
{
    public static implicit operator Wand(WandEntity entity)
    {
        return new Wand
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
