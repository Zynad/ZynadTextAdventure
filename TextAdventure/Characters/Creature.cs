public abstract class Creature
{
    public Creature()
    {
        CheckLevel();
    }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Name => $"{FirstName} {LastName}";
    public int Age { get; set; }
    public int HitPoints { get; set; }
    public int ArmorValue { get; set; }
    public int AttackDamage { get; set; }
    public int DefenseValue { get; set; }
    private int xpValue;
    public int XpValue
    {
        get => xpValue;
        set
        {
            xpValue = value;
            CheckLevel();
        }
    }
    public int Level { get; private set; }
    private void CheckLevel()
    {
        int oldLevel = Level;
        Level = XpValue switch
        {
            < 100 => 1,
            >= 100 and < 300 => 2,
            >= 300 and < 600 => 3,
            _ => Level
        };
        // If the level has increased, call LevelUp
        if (Level > oldLevel)
        {
            LevelUp();
        }
    }
    public void AddXp(int xp)
    {
        XpValue += xp;
    }

    private void LevelUp()
    {
        HitPoints += 10;
        AttackDamage += 5;
        DefenseValue += 5;
    }
}