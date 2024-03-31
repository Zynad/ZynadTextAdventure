using ApplicationServices.Items;
using ApplicationServices.Items.Equipment.Armor;
using ApplicationServices.Items.Equipment.Weapons;

namespace ApplicationServices.Characters;

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
    private int hp;
    public int HitPoints
    {
        get => hp;
        set
        {
            hp = value;
            if (value <= 0)
            {
                OnDeath();
            }
        }
    }
    public int ManaPoints { get; set; }
    public int Stamina { get; set; }
    public Gender Gender { get; set; }
    public int ArmorValue { get; set; }
    public int MeleePhysicalAttackDamage { get; set; }
    public int RangedPhysicalAttackDamage { get; set; }
    public int MagicAttackDamage { get; set; }
    public int DefenseValue { get; set; }
    public List<ArmorMaterial> CanCarryArmorType { get; set; }
    public List<WeaponType> CanCarryWeaponType { get; set; }
    public List<ItemsBase> Inventory { get; set; }
    public int MaxCarryWeigth { get; set; }
    public int WorthXp { get; set; }
    private int xp;
    public int Xp
    {
        get => xp;
        set
        {
            xp = value;
            CheckLevel();
        }
    }
    public int Level { get; private set; }
    private void CheckLevel()
    {
        int oldLevel = Level;
        Level = Xp switch
        {
            < 100 => 1,
            >= 100 and < 300 => 2,
            >= 300 and < 600 => 3,
            > 600 => 4,
            _ => Level
        };
        // If the level has increased, call LevelUp
        for (int i = oldLevel; i < Level; i++)
        {
            LevelUp();
        }
    }
    public void AddXp(int xp)
    {
        Xp += xp;
    }

    protected virtual void LevelUp()
    {
    }

    protected virtual void OnDeath()
    {
    }
}