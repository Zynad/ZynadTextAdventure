using TextAdventure.Game;

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
        return this;
    }
}
