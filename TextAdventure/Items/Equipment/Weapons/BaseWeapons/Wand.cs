using TextAdventure.Game;
using TextAdventure.Repos.Weapons.Models;

namespace TextAdventure.Items.Equipment.Weapons.BaseWeapons;
public class Wand : WeaponBase
{
    public Wand()
    {
        WeaponType = WeaponType.Wand;
    }
    public Wand BeginnerWand()
    {
        Name = "Beginner Wand";
        Rarity = Rarity.Uncommon;
        Material = WeaponMaterial.Wood;
        ArmorValue = 0;
        Durability = 40;
        MeleeAttackValue = 2;
        RangedAttackValue = 8;
        IsRanged = true;
        Range = 30;
        Value = 5;
        Weight = 1;
        TwoHanded = false;
        MagicPower = 30;
        return this;
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
