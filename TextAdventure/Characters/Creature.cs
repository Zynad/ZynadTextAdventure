namespace TextAdventure.Characters;
public abstract class Creature
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Name => $"{FirstName} {LastName}";
    public int Age { get; set; }
    public int HitPoints { get; set; }
    public int ArmorValue { get; set; }
    public int AttackDamage { get; set; }
    public int DefenseValue { get; set; }
}
