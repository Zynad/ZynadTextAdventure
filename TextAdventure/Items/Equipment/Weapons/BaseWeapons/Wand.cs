using TextAdventure.Repos.Weapons.Models;

namespace TextAdventure.Items.Equipment.Weapons.BaseWeapons;
public class Wand : WeaponBase
{
    public Wand()
    {
        WeaponType = WeaponType.Wand;
    }

    public static implicit operator WandEntity(Wand wand)
    {
        return new WandEntity
        {
            Id = Guid.NewGuid(),
            Name = wand.Name,
            Rarity = wand.Rarity,
            Material = wand.Material,
            WeaponType = wand.WeaponType,
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
