using TextAdventure.Equipment.Armor;
using TextAdventure.Equipment.Weapons;

namespace TextAdventure.Characters;
public class Human : Humanoid
{
    public Human()
    {
        
    }
    public Human(string firstName, string lastName, int age, int hitPoints, int attackDamage, int defenseValue)
    {
        // Initialize Creature properties
        FirstName = firstName;
        LastName = lastName;
        Age = age;
        HitPoints = hitPoints;
        ArmorValue = 0;
        AttackDamage = attackDamage;
        DefenseValue = defenseValue;
        // Update ArmorValue based on the new armor
        SetArmorAndAttackValue();
    }
    public Human(string firstName, string lastName, int age, int hitPoints, int attackDamage, int defenseValue, Helmet helmet, Boots boots, Chest chest, Gloves gloves, Legs legs, WeaponBase mainhand, WeaponBase offhand)
    {
        // Initialize Creature properties
        FirstName = firstName;
        LastName = lastName;
        Age = age;
        HitPoints = hitPoints;
        ArmorValue = 0;
        AttackDamage = attackDamage;
        DefenseValue = defenseValue;
        // Initialize Humanoid properties
        Helmet = helmet;
        Boots = boots;
        Chest = chest;
        Gloves = gloves;
        Legs = legs;
        MainHand = mainhand;
        OffHand = offhand;
        // Update ArmorValue based on the new armor
        SetArmorAndAttackValue();
    }
}
