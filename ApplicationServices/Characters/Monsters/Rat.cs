namespace ApplicationServices.Characters.Monsters;
public class Rat : Creature
{
    public Rat(string firstName, string lastName, int age, int hitPoints, int physicalDefense, int magicResistance, int attackDamage, int defenseValue)
    {
        FirstName = firstName;
        LastName = lastName;
        Age = age;
        HitPoints = hitPoints;
        PhysicalDefense = physicalDefense;
        MagicResistance = magicResistance;
        MeleePhysicalAttackDamage = attackDamage;
        DefenseValue = defenseValue;
        WorthXp = 5;
    }
}
