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
    public Vocation Vocation { get; set; } = null!;
    public BaseLocation CurrentLocation { get; set; } = null!;

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
}
