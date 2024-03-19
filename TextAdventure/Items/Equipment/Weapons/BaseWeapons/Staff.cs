using TextAdventure.Game;

namespace TextAdventure.Items.Equipment.Weapons.BaseWeapons;
public class Staff : WeaponBase
{
    public void BeginnerStaff()
    {
        Name = "Beginner Staff";
        LevelRequirement = 1;
        Rarity = Rarity.Uncommon;
        Material = WeaponMaterial.Wood;
        ArmorValue = 3;
        Durability = 30;
        MeleeAttackValue = 7;
        IsRanged = false;
        Range = 0;
        Value = 5;
        Weight = 5;
        TwoHanded = true;
    }
}
