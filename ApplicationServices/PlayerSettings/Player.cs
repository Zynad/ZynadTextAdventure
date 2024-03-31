using ApplicationServices.Characters;
using ApplicationServices.Classes;
using ApplicationServices.Items.Equipment.Armor;
using ApplicationServices.Items.Equipment.Weapons;
using ApplicationServices.Locations;
using ApplicationServices.Mechanics;

namespace ApplicationServices.PlayerSettings;
public class Player : Human
{
    public Dice Dice { get; set; } = new Dice();
    public Vocation Vocation { get; set; }
    public BaseLocation CurrentLocation { get; set; }

    public void SetBaseValues(int hitPoints, int meleeAttackDamage, int defenseValue, int manaPoints, int carryWeigth, int stamina, int rangedAttackDamage, int magicAttackDamage, List<ArmorMaterial> allowedArmors, List<WeaponType> allowedWeaponTypes)
    {
        HitPoints = hitPoints;
        MeleePhysicalAttackDamage = meleeAttackDamage;
        DefenseValue = defenseValue;
        ManaPoints = manaPoints;
        MaxCarryWeigth = carryWeigth;
        Stamina = stamina;
        MagicAttackDamage = magicAttackDamage;
        RangedPhysicalAttackDamage = rangedAttackDamage;
        CanCarryArmorType = allowedArmors ?? [];
        CanCarryWeaponType = allowedWeaponTypes ?? [];
    }

    protected override void LevelUp()
    {
        base.LevelUp();
        switch (Vocation)
        {
            case Knight:
                HitPoints += 20;
                MeleePhysicalAttackDamage += 10;
                DefenseValue += 5;
                ManaPoints += 5;
                MaxCarryWeigth += 10;
                Stamina += 10;
                MagicAttackDamage += 2;
                RangedPhysicalAttackDamage += 5;
                break;
            case Mage:
                HitPoints += 5;
                MeleePhysicalAttackDamage += 5;
                DefenseValue += 2;
                ManaPoints += 30;
                MaxCarryWeigth += 5;
                Stamina += 5;
                MagicAttackDamage += 15;
                RangedPhysicalAttackDamage += 2;
                break;
            // Add more cases as needed
        }
    }
}
