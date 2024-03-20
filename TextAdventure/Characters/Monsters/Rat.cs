namespace TextAdventure.Characters.Monsters;
public class Rat : Creature
{
    public Rat(string firstName, string lastName, int age, int hitPoints, int armorValue, int attackDamage, int defenseValue)
    {
        FirstName = firstName;
        LastName = lastName;
        Age = age;
        HitPoints = hitPoints;
        ArmorValue = armorValue;
        MeleePhysicalAttackDamage = attackDamage;
        DefenseValue = defenseValue;
        WorthXp = 5;
    }
}
